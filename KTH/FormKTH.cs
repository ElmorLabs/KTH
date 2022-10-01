using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KTH {
    public partial class FormKTH : Form {

        private const string EVC2_PNPID = "USB\\VID_0483&PID_5740&MI_01";

        // UART interface commands
        private const byte UART_CMD_WELCOME = 0x00; // 14 bytes "ElmorLabs KTH"
        private const byte UART_CMD_DID = 0x01; // 2 bytes Device ID = = 0xEE04 KTH, 0xEE0D KTH-USB
        private const byte UART_CMD_UID = 0x02; // 12 bytes Unique ID
        private const byte UART_CMD_FWVER = 0x03; // 2 bytes Firmware version

        private const byte UART_CMD_READ_TC1 = 0x10; // 2 bytes Thermocouple 1 Temperature int16 0.1*C
        private const byte UART_CMD_READ_TC2 = 0x11; // 2 bytes Thermocouple 2 Temperature
        private const byte UART_CMD_READ_VDDA = 0x12; // 4 bytes Analog supply int32 1µV
        private const byte UART_CMD_READ_TH1 = 0x14; // 2 bytes Thermistor 1 Raw ADC value uint16
        private const byte UART_CMD_READ_TH2 = 0x15; // 2 bytes Thermistor 2 Raw ADC value

        private const byte UART_CMD_READ_CFG_AOF = 0x20; // 1 byte Auto-off uint8 3.75min
        private const byte UART_CMD_READ_CFG_UNIT = 0x21; // 1 byte Display Temperature Unit 0 => Celsius, 1 => Farenheit, 2 => Kelvin
        private const byte UART_CMD_READ_CFG_SPE = 0x22; // 1 byte Speed (# averages)  0 => 256, 1 => 128, 2 => 64, 3 => 32, 4 => 16, 3 => 8, 2 => 4, 1 => 2, 0 => 1
        private const byte UART_CMD_READ_CAL_PROFILE = 0x23; // 1 byte Currently active calibration profile 0 = Factory, 1 = User
        private const byte UART_CMD_READ_CAL_TC1 = 0x24; // 6 bytes TC1 calibration data [15:0] Tc ADC null-value uint16 [31:16] Tc gain uint16 x*0.125 [47:32] Th ADC offset int16
        private const byte UART_CMD_READ_CAL_TC2 = 0x25; // 6 bytes TC2 calibration data

        private const byte UART_CMD_WRITE_CFG_AOF = 0x30; // 1 byte Auto-off uint8 3.75min
        private const byte UART_CMD_WRITE_CFG_UNIT = 0x31; // 1 byte Display Temperature Unit 0 => Celsius, 1 => Farenheit, 2 => Kelvin
        private const byte UART_CMD_WRITE_CFG_SPE = 0x32; // 1 byte Speed (# averages)  0 => 256, 1 => 128, 2 => 64, 3 => 32, 4 => 16, 3 => 8, 2 => 4, 1 => 2, 0 => 1
        private const byte UART_CMD_WRITE_CAL_PROFILE = 0x33; // 1 byte Currently active calibration profile 0 = Factory, 1 = User
        private const byte UART_CMD_WRITE_CAL_TC1 = 0x34; // 6 bytes TC1 calibration data [15:0] Tc ADC null-value uint16 [31:16] Tc gain uint16 x*0.125 [47:32] Th ADC offset int16
        private const byte UART_CMD_WRITE_CAL_TC2 = 0x35; // 6 bytes TC2 calibration data

        private const byte UART_CMD_DISPLAY_UPD_OFF = 0x40;
        private const byte UART_CMD_DISPLAY_UPD_ON = 0x41;
        private const byte UART_CMD_CAL_OFFSET = 0x42;
        private const byte UART_CMD_CAL_GAIN = 0x43; // Requires LUT index 2 bytes uint16
        private const byte UART_CMD_STORE_CFG = 0x44; // Returns 1 byte after completion

        private const byte UART_CMD_SPI_FLASH_ERASE = 0x50;
        private const byte UART_CMD_SPI_FLASH_PROGRAM = 0x51;

        private const byte UART_CMD_RESET = 0xFD; // Reset device
        private const byte UART_CMD_BOOTLOADER = 0xFE; // Enter bootloader

        public static byte[] KTH_SendCmd(SerialPort serial_port, byte[] tx_buffer, int rx_len) {

            if(serial_port == null) {
                return null;
            }

            try {
                //serial_port.DiscardInBuffer();
                serial_port.Write(tx_buffer, 0, tx_buffer.Length);
            } catch(Exception ex) {
                return null;
            }

            int received = 0;
            int timeout = rx_len * 2;
            byte[] rx_buffer = new byte[rx_len];
            while(received < rx_len && --timeout > 0) {
                try {
                    received += serial_port.Read(rx_buffer, received, 1);
                } catch(Exception ex) {
                    return null;
                }
            }

            if(received != rx_len) {
                return null;
            }

            return rx_buffer;
        }

        List<MonitorGraph> graphList;
        ThreadStart thread_start;
        Thread task_thread;

        List<string> serial_ports;

        SerialPort serial_port = null;
        Mutex serial_port_mutex = new Mutex();

        DataLogger data_logger;

        public FormKTH() {

            InitializeComponent();

            // Update title
            this.Text = "KTH Thermometer " + Application.ProductVersion;

            // Populate options

            comboBoxAof.Items.Add("Disabled");

            comboBoxDisplayUpdate.Items.Add("Off");
            comboBoxDisplayUpdate.Items.Add("On");
            comboBoxDisplayUpdate.SelectedIndex = 1;

            comboBoxSpeed.Items.Add("0 (256 avg)");
            comboBoxSpeed.Items.Add("1 (128 avg)");
            comboBoxSpeed.Items.Add("2 (64 avg)");
            comboBoxSpeed.Items.Add("3 (32 avg)");
            comboBoxSpeed.Items.Add("4 (16 avg)");
            comboBoxSpeed.Items.Add("5 (8 avg)");
            comboBoxSpeed.Items.Add("6 (4 avg)");
            comboBoxSpeed.Items.Add("7 (2 avg)");
            comboBoxSpeed.Items.Add("8 (1 avg)");

            comboBoxUnit.Items.Add("Celsius");
            comboBoxUnit.Items.Add("Farenheit");
            comboBoxUnit.Items.Add("Kelvin");

            comboBoxCalProfile.Items.Add("Factory");
            comboBoxCalProfile.Items.Add("User");

            // Add graphs
            graphList = new List<MonitorGraph>();

            //MonitorGraph monitor_graph = new MonitorGraph(0, "TC1", "°C", "F1", 0, 10, panelMonitoring.Width, 150, true, "");
            MonitorGraph monitor_graph = new MonitorGraph("TC1", 1, "°C", panelMonitoring.Width, 150);
            monitor_graph.Location = new Point(0, 10);
            //monitor_graph.MouseEnter += Monitor_graph_MouseEnter;
            //monitor_graph.MouseLeave += Monitor_graph_MouseLeave;
            //monitor_graph.MouseMove += Monitor_graph_MouseMove;
            graphList.Add(monitor_graph);
            panelMonitoring.Controls.Add(monitor_graph);

            //monitor_graph = new MonitorGraph(1, "TC2", "°C", "F1", 0, 10 + (150 + 10), panelMonitoring.Width, 150, true, "");
            monitor_graph = new MonitorGraph("TC2", 1, "°C", panelMonitoring.Width, 150);
            monitor_graph.Location = new Point(0, 10 + (150 + 10));
            //monitor_graph.MouseEnter += Monitor_graph_MouseEnter;
            //monitor_graph.MouseLeave += Monitor_graph_MouseLeave;
            //monitor_graph.MouseMove += Monitor_graph_MouseMove;
            graphList.Add(monitor_graph);
            panelMonitoring.Controls.Add(monitor_graph);

            data_logger = new DataLogger();

            //UpdateConfigValues();
            //UpdateCalValues();

            thread_start = new ThreadStart(update_task);

            UpdateSerialPorts();

        }

        private void StartMonitoring() {
            run_task = true;
            task_thread = new Thread(thread_start);
            task_thread.IsBackground = true;
            task_thread.Start();
        }

        private void StopMonitoring() {
            run_task = false;
            task_thread.Join(500);
        }

        private void UpdateSerialPorts() {
            serial_ports = SerialPort.GetPortNames().ToList();

            comboBoxPorts.Items.Clear();

            // https://stackoverflow.com/questions/2837985/getting-serial-port-information
            using(var searcher = new ManagementObjectSearcher("SELECT * FROM WIN32_SerialPort")) {
                foreach(string port in serial_ports) {
                    bool found = false;
                    foreach(ManagementObject queryObj in searcher.Get()) {
                        if(queryObj["DeviceID"].ToString().Equals(port)) {
                            string pnp_dev_id = queryObj["PNPDeviceID"].ToString();
                            if(pnp_dev_id.StartsWith(EVC2_PNPID)) {
                                comboBoxPorts.Items.Add(port + ": EVC2 Serial Port");
                                comboBoxPorts.SelectedIndex = comboBoxPorts.Items.Count - 1;
                            } else {
                                comboBoxPorts.Items.Add(port + ": " + queryObj["Description"].ToString());
                            }
                            found = true;
                        }
                    }
                    if(!found) {
                        comboBoxPorts.Items.Add(port + ": Unknown Serial Port");
                    }
                }
            }
        }

        private void UpdateConfigValues() {

            if(!serial_port_mutex.WaitOne(1000)) {
                MessageBox.Show("Couldn't get serial port mutex");
                return;
            }

            // UID
            byte[] rx_buffer = KTH_SendCmd(serial_port, new byte[] { UART_CMD_UID }, 12);
            if(rx_buffer != null) {
                labelUidValue.Text = "";
                foreach(byte val in rx_buffer) {
                    labelUidValue.Text += val.ToString("X2");
                }
            }

            // FW Version
            rx_buffer = KTH_SendCmd(serial_port, new byte[] { UART_CMD_FWVER }, 2);
            if(rx_buffer != null) {
                labelFwVerValue.Text = (rx_buffer[1] << 8 | rx_buffer[0]).ToString("X4");
            }

            // Auto-off
            rx_buffer = KTH_SendCmd(serial_port, new byte[] { UART_CMD_READ_CFG_AOF }, 1);
            if(rx_buffer != null) {
                comboBoxAof.Text = (rx_buffer[0] * 3.75f).ToString();
            }

            // Unit
            rx_buffer = KTH_SendCmd(serial_port, new byte[] { UART_CMD_READ_CFG_UNIT }, 1);
            if(rx_buffer != null) {
                if(rx_buffer[0] < comboBoxUnit.Items.Count) {
                    comboBoxUnit.SelectedIndex = rx_buffer[0];
                }
            }

            // Speed
            rx_buffer = KTH_SendCmd(serial_port, new byte[] { UART_CMD_READ_CFG_SPE }, 1);
            if(rx_buffer != null) {
                if(rx_buffer[0] < comboBoxSpeed.Items.Count) {
                    comboBoxSpeed.SelectedIndex = rx_buffer[0];
                }
            }

            serial_port_mutex.ReleaseMutex();

        }
        private void UpdateCalValues() {

            if(!serial_port_mutex.WaitOne(1000)) {
                MessageBox.Show("Couldn't get serial port mutex");
                return;
            }

            // Cal profile
            byte[] rx_buffer = KTH_SendCmd(serial_port, new byte[] { UART_CMD_READ_CAL_PROFILE }, 1);
            if(rx_buffer != null) {
                if(rx_buffer[0] > 1) {
                    MessageBox.Show("Bad cal profile value");
                    serial_port_mutex.ReleaseMutex();
                    return;
                }
                comboBoxCalProfile.SelectedIndex = rx_buffer[0];
            }
            Thread.Sleep(1);

            // Cal data TC1
            rx_buffer = KTH_SendCmd(serial_port, new byte[] { UART_CMD_READ_CAL_TC1 }, 6);
            if(rx_buffer != null) {
                textBoxCalTc1Zero.Text = ((UInt16)(rx_buffer[1] << 8 | rx_buffer[0])).ToString();
                textBoxCalTc1Gain.Text = ((UInt16)(rx_buffer[3] << 8 | rx_buffer[2])).ToString();
                textBoxCalTc1ThOffset.Text = ((Int16)(rx_buffer[5] << 8 | rx_buffer[4])).ToString();
            }
            Thread.Sleep(1);

            // Cal data TC2
            rx_buffer = KTH_SendCmd(serial_port, new byte[] { UART_CMD_READ_CAL_TC2 }, 6);
            if(rx_buffer != null) {
                textBoxCalTc2Zero.Text = ((UInt16)(rx_buffer[1] << 8 | rx_buffer[0])).ToString();
                textBoxCalTc2Gain.Text = ((UInt16)(rx_buffer[3] << 8 | rx_buffer[2])).ToString();
                textBoxCalTc2ThOffset.Text = ((Int16)(rx_buffer[5] << 8 | rx_buffer[4])).ToString();
            }
            Thread.Sleep(1);

            serial_port_mutex.ReleaseMutex();

        }

        bool track = false;
        private void Monitor_graph_MouseMove(object sender, MouseEventArgs e) {
            if(track) {
                MonitorGraph monitor_graph = (MonitorGraph)sender;
                //monitor_graph.SetTrackX(e.X);
            }
        }

        private void Monitor_graph_MouseLeave(object sender, EventArgs e) {
            if(track) {
                MonitorGraph monitor_graph = (MonitorGraph)sender;
                //monitor_graph.SetTrackX(-1);
                track = false;
            }
        }

        private void Monitor_graph_MouseEnter(object sender, EventArgs e) {
            track = true;
        }

        static bool run_task;
        private void update_task() {
            float[] temp = new float[2];

            while(run_task) {

                byte[] tx_buffer = new byte[1];
                for(int i = 0; i < 2; i++) {
                    try {
                        tx_buffer[0] = (byte)(0x10 + i);
                        int rx_len = 2;
                        byte[] rx_buffer = null;
                        if(serial_port_mutex.WaitOne(1000)) {
                            rx_buffer = KTH_SendCmd(serial_port, tx_buffer, rx_len);
                            serial_port_mutex.ReleaseMutex();
                        }
                        if(rx_buffer != null) {
                            Int16 value = (Int16)(rx_buffer[1] << 8 | rx_buffer[0]);
                            temp[i] = value / 10.0f;
                            //graphList[i].SetTrackX(-1);
                            graphList[i].Invoke((MethodInvoker)delegate {
                                graphList[i].addValue(temp[i]);
                            });
                        }
                    } catch(Exception ex) {

                    }
                }

                if(csv_logging) {
                    if(tc1_log_id != -1) {
                        data_logger.UpdateValue(tc1_log_id, Math.Round(temp[0]*10)/10);
                    }
                    if(tc2_log_id != -1) {
                        data_logger.UpdateValue(tc2_log_id, Math.Round(temp[1] * 10) / 10);
                    }
                    data_logger.WriteLine();
                }

                if(WriteToFileName.Length > 0) {
                    try {
                        string text = "";
                        if(checkBoxTc1.Checked) {
                            text += temp[0].ToString("F1") + "°C " + (temp[0]*9/5f + 32).ToString("F1") + "°F" + Environment.NewLine;
                        }
                        if(checkBoxTc2.Checked) {
                            text += temp[1].ToString("F1") + "°C " + (temp[1] * 9 / 5f + 32).ToString("F1") + "°F" + Environment.NewLine;
                        }
                        System.IO.File.WriteAllText(WriteToFileName, text);
                    } catch(Exception ex) {

                    }
                }
            }
        }


        private void FormKTH_FormClosing(object sender, FormClosingEventArgs e) {
            run_task = false;
        }

        private void buttonReset_Click(object sender, EventArgs e) {
            if(!serial_port_mutex.WaitOne(1000)) {
                MessageBox.Show("Couldn't get serial port mutex");
                return;
            }
            KTH_SendCmd(serial_port, new byte[] { UART_CMD_RESET }, 0);
            serial_port_mutex.ReleaseMutex();
        }

        private void buttonBootloader_Click(object sender, EventArgs e) {
            if(!serial_port_mutex.WaitOne(1000)) {
                MessageBox.Show("Couldn't get serial port mutex");
                return;
            }
            KTH_SendCmd(serial_port, new byte[] { UART_CMD_BOOTLOADER }, 0);
            serial_port_mutex.ReleaseMutex();
        }

        private void buttonStorecfg_Click(object sender, EventArgs e) {
            if(!serial_port_mutex.WaitOne(1000)) {
                MessageBox.Show("Couldn't get serial port mutex");
                return;
            }
            byte[] rx_buffer = KTH_SendCmd(serial_port, new byte[] { UART_CMD_STORE_CFG }, 1);
            if(rx_buffer == null || rx_buffer[0] != 0x01) {
                MessageBox.Show("Error");
            }
            serial_port_mutex.ReleaseMutex();
        }

        private void buttonApply_Click(object sender, EventArgs e) {

            if(!serial_port_mutex.WaitOne(1000)) {
                MessageBox.Show("Couldn't get serial port mutex");
                return;
            }

            // Auto-off
            byte[] tx_buffer = new byte[1];
            int auto_off_value = 0;
            if(comboBoxAof.SelectedIndex != 0 && !int.TryParse(comboBoxAof.Text, out auto_off_value)) {
                serial_port_mutex.ReleaseMutex();
                MessageBox.Show("Error parsing auto-off value");
                return;
            }
            tx_buffer[0] = (byte)(auto_off_value / 3.75);

            KTH_SendCmd(serial_port, new byte[] { UART_CMD_WRITE_CFG_AOF }, 0);
            Thread.Sleep(1);
            KTH_SendCmd(serial_port, tx_buffer, 0);
            Thread.Sleep(1);

            // Speed
            tx_buffer[0] = (byte)comboBoxSpeed.SelectedIndex;
            if(tx_buffer[0] > 8) {
                tx_buffer[0] = 8;
            }

            KTH_SendCmd(serial_port, new byte[] { UART_CMD_WRITE_CFG_SPE }, 0);
            Thread.Sleep(1);
            KTH_SendCmd(serial_port, tx_buffer, 0);
            Thread.Sleep(1);

            // Display update
            if(comboBoxDisplayUpdate.SelectedIndex == 0) {
                tx_buffer[0] = UART_CMD_DISPLAY_UPD_OFF;
            } else {
                tx_buffer[0] = UART_CMD_DISPLAY_UPD_ON;
            }

            KTH_SendCmd(serial_port, tx_buffer, 0);
            Thread.Sleep(1);

            // Unit
            tx_buffer[0] = (byte)comboBoxUnit.SelectedIndex;
            if(tx_buffer[0] > 2) {
                tx_buffer[0] = 2;
            }
            
            KTH_SendCmd(serial_port, new byte[] { UART_CMD_WRITE_CFG_UNIT }, 0);
            Thread.Sleep(1);
            byte[] rx_buffer = KTH_SendCmd(serial_port, tx_buffer, 1);
            if(rx_buffer == null || rx_buffer[0] != 0x01) {
                serial_port_mutex.ReleaseMutex();
                MessageBox.Show("Error setting temp unit");
                return;
            }
            Thread.Sleep(1);

            serial_port_mutex.ReleaseMutex();

            UpdateConfigValues();
        }

        private void buttonChangeCalProfile_Click(object sender, EventArgs e) {
            if(!serial_port_mutex.WaitOne(1000)) {
                MessageBox.Show("Couldn't get serial port mutex");
                return;
            }
            byte[] tx_buffer = new byte[1];

            if(comboBoxCalProfile.SelectedIndex == 0) {
                tx_buffer[0] = 0;
            } else {
                tx_buffer[0] = 1;
            }

            KTH_SendCmd(serial_port, new byte[] { UART_CMD_WRITE_CAL_PROFILE }, 0);
            Thread.Sleep(1);
            KTH_SendCmd(serial_port, tx_buffer, 0);
            Thread.Sleep(1);

            serial_port_mutex.ReleaseMutex();

            UpdateCalValues();
        }

        private void buttonCalOffset_Click(object sender, EventArgs e) {

            if(MessageBox.Show("This will calibrate both TC1 and TC2 simultaneously. For the offset calibration to work, the +/- inputs has to be shorted before continuing. The calibration takes about 15 seconds.", "Calibration", MessageBoxButtons.OKCancel) == DialogResult.Cancel) {
                return;
            }

            if(!serial_port_mutex.WaitOne(1000)) {
                MessageBox.Show("Couldn't get serial port mutex");
                return;
            }

            byte[] tx_buffer = new byte[1];

            if(comboBoxCalProfile.SelectedIndex == 0) {
                tx_buffer[0] = 0;
            } else {
                tx_buffer[0] = 1;
            }

            // Set cal profile
            KTH_SendCmd(serial_port, new byte[] { UART_CMD_WRITE_CAL_PROFILE }, 0);
            Thread.Sleep(1);
            KTH_SendCmd(serial_port, tx_buffer, 0);
            Thread.Sleep(1);

            serial_port.ReadTimeout = 25000;
            byte[] rx_buffer = KTH_SendCmd(serial_port, new byte[] { UART_CMD_CAL_OFFSET }, 1);
            if(rx_buffer == null || rx_buffer[0] != 0x01) {
                MessageBox.Show("Error while calibrating");
            } else {
                MessageBox.Show("Calibration completed.");
            }

            serial_port_mutex.ReleaseMutex();

            UpdateCalValues();
        }

        private void buttonCalGain_Click(object sender, EventArgs e) {
            MessageBox.Show("Not yet implemented");
        }

        private void buttonApplyCal_Click(object sender, EventArgs e) {


            byte cal_profile = 1;
            if(comboBoxCalProfile.SelectedIndex == 0) {
                cal_profile = 0;
            }

            // Parse TC1

            UInt16 tc1_zero;
            UInt16 tc1_gain;
            Int16 tc1_thoffset;

            if(!UInt16.TryParse(textBoxCalTc1Zero.Text, out tc1_zero)) {
                MessageBox.Show("Error parsing TC1 Zero-bias");
                return;
            }
            if(!UInt16.TryParse(textBoxCalTc1Gain.Text, out tc1_gain)) {
                MessageBox.Show("Error parsing TC1 Gain");
                return;
            }
            if(!Int16.TryParse(textBoxCalTc1ThOffset.Text, out tc1_thoffset)) {
                MessageBox.Show("Error parsing TC1 ThOffset");
                return;
            }

            byte[] tc1_data = new byte[6];
            tc1_data[0] = (byte)tc1_zero;
            tc1_data[1] = (byte)(tc1_zero >> 8);
            tc1_data[2] = (byte)tc1_gain;
            tc1_data[3] = (byte)(tc1_gain >> 8);
            tc1_data[4] = (byte)tc1_thoffset;
            tc1_data[5] = (byte)(tc1_thoffset >> 8);

            // Parse TC2
            UInt16 tc2_zero;
            UInt16 tc2_gain;
            Int16 tc2_thoffset;

            if(!UInt16.TryParse(textBoxCalTc2Zero.Text, out tc2_zero)) {
                MessageBox.Show("Error parsing TC2 Zero-bias");
                return;
            }
            if(!UInt16.TryParse(textBoxCalTc2Gain.Text, out tc2_gain)) {
                MessageBox.Show("Error parsing TC2 Gain");
                return;
            }
            if(!Int16.TryParse(textBoxCalTc2ThOffset.Text, out tc2_thoffset)) {
                MessageBox.Show("Error parsing TC2 ThOffset");
                return;
            }

            if(!serial_port_mutex.WaitOne(1000)) {
                MessageBox.Show("Couldn't get serial port mutex");
                return;
            }

            byte[] tc2_data = new byte[6];
            tc2_data[0] = (byte)tc2_zero;
            tc2_data[1] = (byte)(tc2_zero >> 8);
            tc2_data[2] = (byte)tc2_gain;
            tc2_data[3] = (byte)(tc2_gain >> 8);
            tc2_data[4] = (byte)tc2_thoffset;
            tc2_data[5] = (byte)(tc2_thoffset >> 8);

            byte[] tx_buffer = new byte[1];

            // Set cal profile
            KTH_SendCmd(serial_port, new byte[] { UART_CMD_WRITE_CAL_PROFILE }, 0);
            Thread.Sleep(1);
            tx_buffer[0] = cal_profile;
            KTH_SendCmd(serial_port, tx_buffer, 0);
            Thread.Sleep(1);

            // Send TC1 data
            KTH_SendCmd(serial_port, new byte[] { UART_CMD_WRITE_CAL_TC1 }, 0);
            Thread.Sleep(1);
            for(int i = 0; i < tc1_data.Length; i++) {
                tx_buffer[0] = tc1_data[i];
                KTH_SendCmd(serial_port, tx_buffer, 0);
                Thread.Sleep(1);
            }

            // Send TC2 data
            KTH_SendCmd(serial_port, new byte[] { UART_CMD_WRITE_CAL_TC2 }, 0);
            Thread.Sleep(1);
            for(int i = 0; i < tc2_data.Length; i++) {
                tx_buffer[0] = tc2_data[i];
                KTH_SendCmd(serial_port, tx_buffer, 0);
                Thread.Sleep(1);
            }

            serial_port_mutex.ReleaseMutex();

            UpdateCalValues();
        }

        private void buttonOpenPort_Click(object sender, EventArgs e) {
            if(serial_port == null || !serial_port.IsOpen) {
                if(comboBoxPorts.SelectedIndex >= serial_ports.Count) {
                    return;
                }

                try {
                    serial_port = new SerialPort(serial_ports[comboBoxPorts.SelectedIndex]);

                    serial_port.BaudRate = 9600;
                    serial_port.Parity = Parity.None;
                    serial_port.StopBits = StopBits.One;
                    serial_port.DataBits = 8;

                    serial_port.Handshake = Handshake.None;
                    serial_port.ReadTimeout = 1000;
                    serial_port.WriteTimeout = 1000;

                    serial_port.WriteBufferSize = 512;
                    serial_port.ReadBufferSize = 512;

                } catch(Exception ex) {
                    MessageBox.Show("Error initializing serial port: " + ex.Message);
                    serial_port = null;
                    return;
                }

                try {
                    serial_port.Open();
                } catch(Exception ex) {
                    MessageBox.Show("Error opening port: " + ex.Message);
                    return;
                }

                // Check communication
                if(!serial_port_mutex.WaitOne(1000)) {
                    MessageBox.Show("Couldn't get serial port mutex");
                    return;
                }

                byte[] rx_buffer = FormKTH.KTH_SendCmd(serial_port, new byte[] { UART_CMD_DID }, 2);

                serial_port_mutex.ReleaseMutex();

                if(rx_buffer == null || (rx_buffer[0] != 0x0D || rx_buffer[0] != 0x04) || rx_buffer[1] != 0xEE) {
                    MessageBox.Show("Error communicating with KTH"); 
                    try {
                        serial_port.Close();
                        serial_port.Dispose();
                        serial_port = null;
                    } catch(Exception ex) { }

                    return;
                }

                buttonOpenPort.Text = "Close";

                UpdateConfigValues();
                UpdateCalValues();
                StartMonitoring();

            } else {

                StopMonitoring();

                // Close serial port
                try {
                    serial_port.Close();
                } catch(Exception ex) {
                    MessageBox.Show("Error closing port: " + ex.Message);
                }

                try {
                    serial_port.Dispose();
                    serial_port = null;
                } catch(Exception ex) {

                }
                buttonOpenPort.Text = "Open";
            }
        }

        private void buttonRefreshPorts_Click(object sender, EventArgs e) {
            UpdateSerialPorts();
        }

        bool csv_logging = false;
        int tc1_log_id = -1;
        int tc2_log_id = -1;
        
        private void buttonLog_Click(object sender, EventArgs e) {
            if(!csv_logging) {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "csv files (*.csv)|*.csv";
                sfd.RestoreDirectory = true;
                if(sfd.ShowDialog() == DialogResult.OK) {
                    // Open and clear file
                    try {

                        // Try to set file path
                        if(!data_logger.SetFilePath(sfd.FileName, false)) {
                            DialogResult dr = MessageBox.Show($"Overwrite {sfd.FileName}?", "File already exists", MessageBoxButtons.OKCancel);
                            if(dr != DialogResult.OK) {
                                // Cancel
                                return;
                            } else {
                                // Try again with overwrite
                                if(!data_logger.SetFilePath(sfd.FileName, true)) {
                                    MessageBox.Show("Error setting file path");
                                    return;
                                }
                            }
                        }

                        if(checkBoxTc1.Checked) {
                            tc1_log_id = data_logger.AddLogItem("TC1", "°C");
                        } else {
                            tc1_log_id = -1;
                        }
                        if(checkBoxTc2.Checked) {
                            tc2_log_id = data_logger.AddLogItem("TC2", "°C");
                        } else {
                            tc2_log_id = -1;
                        }
                        data_logger.WriteHeader();
                        csv_logging = true;
                        buttonLog.Text = "Stop logging";
                    } catch(Exception ex) {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            } else {
                if(tc1_log_id != -1) {
                    data_logger.RemoveLogItem(tc1_log_id);
                    tc1_log_id = -1;
                }
                if(tc2_log_id != -1) {
                    data_logger.RemoveLogItem(tc2_log_id);
                    tc2_log_id = -1;
                }

                csv_logging = false;
                buttonLog.Text = "Log to CSV";
            }
        }

        private string WriteToFileName = "";

        private void buttonWriteToFile_Click(object sender, EventArgs e) {
            if(WriteToFileName.Length < 1) {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "txt files (*.txt)|*.txt";
                sfd.RestoreDirectory = true;
                if(sfd.ShowDialog() == DialogResult.OK) {
                    // Open and clear file
                    try {
                        System.IO.File.WriteAllText(sfd.FileName, "");
                        WriteToFileName = sfd.FileName;
                        buttonWriteToFile.Text = "Stop writing file";
                    } catch(Exception ex) {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            } else {
                WriteToFileName = "";
                buttonWriteToFile.Text = "Write to file";

            }
        }

        private void checkBoxTc2_CheckedChanged(object sender, EventArgs e) {

        }

        private void checkBoxTc1_CheckedChanged(object sender, EventArgs e) {

        }

        private void WriteSpiFlash() {

            // Check communication
            if(!serial_port_mutex.WaitOne(1000)) {
                MessageBox.Show("Couldn't get serial port mutex");
                return;
            }

            // Erase chip
            byte[] rx_buffer = KTH_SendCmd(serial_port, new byte[] { UART_CMD_SPI_FLASH_ERASE }, 1);

            if(rx_buffer == null || rx_buffer[0] != 1) {
                MessageBox.Show("Error erasing flash");
                return;
            }

            // Create data
            List<byte> temp_list = new List<byte>();
            for(int tc_volt = -6600; tc_volt < 15000; tc_volt++) {
                double Vcomp = tc_volt / 1000.0;
                double Th = 0;
                if(Vcomp < 0) {
                    Th = (2.5173462 * Math.Pow(10, 1)) * Vcomp +
                    (-1.1662878 * Math.Pow(10, 0)) * Math.Pow(Vcomp, 2) +
                    (-1.0833638 * Math.Pow(10, 0)) * Math.Pow(Vcomp, 3) +
                    (-8.9773540 * Math.Pow(10, -1)) * Math.Pow(Vcomp, 4) +
                    (-3.7342377 * Math.Pow(10, -1)) * Math.Pow(Vcomp, 5) +
                    (-8.6632643 * Math.Pow(10, -2)) * Math.Pow(Vcomp, 6) +
                    (-1.0450598 * Math.Pow(10, -2)) * Math.Pow(Vcomp, 7) +
                    (-5.1920577 * Math.Pow(10, -4)) * Math.Pow(Vcomp, 8);
                } else if((Vcomp >= 0) && (Vcomp < 20.644)) {
                    Th = (2.5083550 * Math.Pow(10, 1)) * Vcomp +
                    (7.8601060 * Math.Pow(10, -2)) * Math.Pow(Vcomp, 2) +
                    (-2.5031310 * Math.Pow(10, -1)) * Math.Pow(Vcomp, 3) +
                    (8.3152700 * Math.Pow(10, -2)) * Math.Pow(Vcomp, 4) +
                    (-1.2280340 * Math.Pow(10, -2)) * Math.Pow(Vcomp, 5) +
                    (9.8040360 * Math.Pow(10, -4)) * Math.Pow(Vcomp, 6) +
                    (-4.4130300 * Math.Pow(10, -5)) * Math.Pow(Vcomp, 7) +
                    (1.0577340 * Math.Pow(10, -6)) * Math.Pow(Vcomp, 8) +
                    (-1.0527550 * Math.Pow(10, -8)) * Math.Pow(Vcomp, 9);
                } else if((Vcomp >= 20.644) && (Vcomp <= 54.886)) {
                    Th = -1.3180580 * Math.Pow(10, 2) +
                    (4.8302220 * Math.Pow(10, 1)) * Vcomp +
                    (-1.6460310 * Math.Pow(10, 0)) * Math.Pow(Vcomp, 2) +
                    (5.4647310 * Math.Pow(10, -2)) * Math.Pow(Vcomp, 3) +
                    (-9.6507150 * Math.Pow(10, -4)) * Math.Pow(Vcomp, 4) +
                    (8.8021930 * Math.Pow(10, -6)) * Math.Pow(Vcomp, 5) +
                    (-3.1108100 * Math.Pow(10, -8)) * Math.Pow(Vcomp, 6);
                }

                int temp = (int)(Math.Round(Th, 1)*10);
                temp_list.Add((byte)temp);
                temp_list.Add((byte)(temp>>8));
            }


            for(int page = 0; page < (255 + temp_list.Count) / 256; page++) {
                int retry = 9;

                buttonUpdateLut.Text = ($"Program Page #{page}... "); 
                buttonUpdateLut.Refresh();

                while(retry >= 0) {

                    int tx_len = 256;
                    const int sleep = 1;

                    byte[] tx_buffer = new byte[1];
                    tx_buffer[0] = 0x51;
                    rx_buffer = KTH_SendCmd(serial_port, tx_buffer, 1);

                    if(rx_buffer != null && rx_buffer[0] == 1) {

                        /*tx_buffer[0] = 0;
                        KTH_SendCmd(serial_port, tx_buffer, 0);
                        Thread.Sleep(sleep);
                        tx_buffer[0] = (byte)page;
                        KTH_SendCmd(serial_port, tx_buffer, 0);
                        Thread.Sleep(sleep);
                        tx_buffer[0] = (byte)(page >> 8);
                        KTH_SendCmd(serial_port, tx_buffer, 0);
                        Thread.Sleep(sleep);*/

                        if(temp_list.Count - 256 * page < 256) {
                            tx_len = temp_list.Count - 256 * page;
                        }

                        /*for(int i = 0; i < 256; i++) {
                            if(i < tx_len) {
                                tx_buffer[0] = temp_list[page * 256 + i];
                            } else {
                                tx_buffer[0] = 0xFF;
                            }
                            serial_port.Write(tx_buffer, 0, 1);
                            Thread.Sleep(sleep);
                        }

                        int rx_len = 0;
                        try {
                            rx_len = serial_port.Read(rx_buffer, 0, 1);
                            if(rx_len != 1) {
                                rx_buffer = null;
                            }
                        } catch {
                            rx_buffer = null;
                        }*/

                        tx_buffer = new byte[4 + 256];
                        tx_buffer[0] = 0x51;
                        tx_buffer[1] = 0;
                        tx_buffer[2] = (byte)page;
                        tx_buffer[3] = (byte)(page >> 8);

                        Array.Copy(temp_list.ToArray(), page * 256, tx_buffer, 4, tx_len);
                        //rx_buffer = KTH_SendCmd(serial_port, tx_buffer, 1);
                        //Thread.Sleep(1);

                        serial_port.Write(tx_buffer, 0, tx_buffer.Length);

                        rx_buffer = new byte[1];
                        int read = 0;
                        int rx_retry = 3;
                        while(read != 1 && rx_retry-- > 0) {
                            try {
                                read = serial_port.Read(rx_buffer, 0, 1);
                            } catch {
                                rx_buffer = null;
                            }
                        }
                    }

                    if(rx_buffer != null && rx_buffer[0] == 1) {
                        buttonUpdateLut.Text += "OK";
                        retry = -2;
                    } else {
                        buttonUpdateLut.Text += "R";
                        retry--;
                        /*for(int i = 0; i < tx_buffer.Length; i++) {
                            tx_buffer[i] = 0xFF;
                        }
                        serial_port.Write(tx_buffer, 0, tx_buffer.Length);*/
                        Thread.Sleep(100);
                        serial_port.DiscardInBuffer();
                    }
                    buttonUpdateLut.Refresh();
                }

                if(retry == -1) {
                    MessageBox.Show($"Error page #{page}");
                    break;
                }
            }


            buttonUpdateLut.Text = "Update LUT";

            serial_port_mutex.ReleaseMutex();
        }

        private void buttonUpdateLut_Click(object sender, EventArgs e) {
            WriteSpiFlash();
        }
    }
}
