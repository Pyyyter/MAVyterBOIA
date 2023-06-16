using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SimpleExample
{
    public partial class GroundStation : Form
    {
        
        // Store the previous mouse position for dragging
        private Point previousMousePosition;

        readonly FormChart formGraficos;
        readonly FormDados formDados;
        readonly FormMapa formMapa;
        readonly FormConfigurações formConfigurações;
        readonly FormPixhawk formPixhawk;
        readonly FormTeste formTeste;

        //Mavlink parser responsible for parsing and deparsing mavlink packets
        private readonly Mavlink.MavlinkParser mavlinkParser = new Mavlink.MavlinkParser();
        // locking to prevent thread collisions on serial port
        private readonly object serialLock = new object();
        private byte SysIDLocal { get;  set; } = 0xFF;
        private byte CompIDLocal { get; set; } = (byte)Mavlink.MAV_COMPONENT.MAV_COMP_ID_MISSIONPLANNER;

        private byte VehicleSysID { get; set; } = 0x01;
        private byte VehicleCompID { get; set; } = (byte)Mavlink.MAV_COMPONENT.MAV_COMP_ID_ONBOARD_COMPUTER;


        public GroundStation()
        {
            InitializeComponent();
            SetupRoundDraggableForm();

            formGraficos = new FormChart() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true, FormBorderStyle = FormBorderStyle.None };
            formDados = new FormDados() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true, FormBorderStyle = FormBorderStyle.None };
            formMapa = new FormMapa() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true, FormBorderStyle = FormBorderStyle.None };
            formConfigurações = new FormConfigurações() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true, FormBorderStyle = FormBorderStyle.None };
            formPixhawk = new FormPixhawk(this) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true, FormBorderStyle = FormBorderStyle.None };
            formTeste = new FormTeste(this) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true, FormBorderStyle = FormBorderStyle.None };
        }

        #region Form Rounding and Dragging

        // This function will allow the form to be rounded and draggable instead of the default sharp rectangle shape that Winforms provides.
        private void SetupRoundDraggableForm()
        {
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            MouseDown += Form_MouseDown_Drag;
            MouseMove += Form_MouseMove_Drag;
        }

       
        // This is the function that will allow the form to be rounded
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
         (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
          );

        // Allows form to be dragged.
        protected override void WndProc(ref Message m)
        {
            // Constants for form rounding and dragging
            const int WM_NCHITTEST = 0x84;
            const int HT_CAPTION = 0x02;

            // Override WndProc to enable dragging
            if (m.Msg == WM_NCHITTEST)
            {
                base.WndProc(ref m);
                if (m.Result.ToInt32() == HT_CAPTION)
                    m.Result = (IntPtr)1;
                return;
            }
            base.WndProc(ref m);
        }

        private void Form_MouseDown_Drag(object sender, MouseEventArgs e)
        {
            // Store the current mouse position
            previousMousePosition = new Point(e.X, e.Y);
        }

        private void Form_MouseMove_Drag(object sender, MouseEventArgs e)
        {
            // Move the form when dragging
            if (e.Button == MouseButtons.Left)
            {
                Left += e.X - previousMousePosition.X;
                Top += e.Y - previousMousePosition.Y;
            }
        }
        #endregion // Form Rounding and Dragging

        #region Form Initialization Defaults

        private void GroundStation_Load(object sender, EventArgs e)
        {
           
                //SetSerialPortDefaults("COM7", 57600);
                LoadForms();

        }

        // Ensure all forms are loaded and ready to receive data.
        private void LoadForms()
        {
            List<Form> forms = new List<Form>() { formConfigurações, formDados, formMapa, formPixhawk, formTeste };
            foreach (var form in forms)
            {
                panelFormLoader.Controls.Add(form); // All forms must be added to the container
                form.Show(); // Forms must be shown at least once to be initialized
            }
            panelFormLoader.Controls.Clear(); // Clear the forms and select the default form to be shown next
            panelFormLoader.Controls.Add(formMapa); // Select default form to be shown here
            panelFormLoader.Dock = DockStyle.Fill;  // Fill the whole panel size with the form
            panelFormLoader.Show(); // Show the form
        }

        private void SetSerialPortDefaults(string portName, int baudRate)
        {
           
            comboBoxBaudRate.SelectedItem = baudRate.ToString();
            String[] portNames = SerialPort.GetPortNames();   
            comboBoxSerialPort.DataSource = portNames;
            if (portNames.Length == 0) return;
            foreach (var item in SerialPort.GetPortNames())
            {
                // Sets default value
                if (item == portName) comboBoxSerialPort.SelectedItem = item;
            }
            buttonConnect.PerformClick();
        }

        #endregion


        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                // if the port is open close it
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                    buttonConnect.Text = "Abrir";
                    return;
                }

                // set the comport options
                serialPort1.PortName = comboBoxSerialPort.SelectedItem.ToString();
                serialPort1.BaudRate = int.Parse(comboBoxBaudRate.SelectedItem.ToString());

                // open the comport
                serialPort1.Open();
                buttonConnect.Text = "Fechar";

                // set timeout to 2 seconds
                serialPort1.ReadTimeout = 2000;
                serialPort1.DiscardInBuffer();
                
                BackgroundWorker workerSerialPort = new BackgroundWorker();
                workerSerialPort.DoWork += workerSerialPort_ReadData;
                workerSerialPort.RunWorkerAsync();

            }
            catch (Exception exception)
            {
                MessageBox.Show($"{exception.Message}\n" +
                    $"Verifique se a porta existe ou já está sendo usada por outro programa.");
            }
          
        }

        private void workerSerialPort_ReadData(object sender, DoWorkEventArgs e)
        {
            while (serialPort1.IsOpen)
            {
                try
                {
                    Mavlink.MavlinkMessage message;
                    lock (serialLock)
                    {
                        message = mavlinkParser.ReadPacket(serialPort1.BaseStream);
                        if (message == null || message.Payload == null)
                            continue;
                    }
                               
                    ProcessMessage(message);
                }
                catch (Exception serialException)
                {
                     Console.WriteLine($"{serialException.Message} at {System.DateTime.Now}");
                }

                System.Threading.Thread.Sleep(10);
            }
        }

        void ProcessMessage(Mavlink.MavlinkMessage message)
        {
            Console.WriteLine($"{message.MsgTypename} + {serialPort1.BytesToRead}");
            switch (message.MsgID)
            {

                case (byte)Mavlink.MAVLINK_MSG_ID.HEARTBEAT:
                    {
                        var payload = (Mavlink.mavlink_heartbeat_t)message.Payload;
                        Console.WriteLine($"Heartbeat: {payload.autopilot} {payload.base_mode} {payload.custom_mode} {payload.mavlink_version}");
                        
                        // Send heartbeat back
                        Mavlink.mavlink_heartbeat_t heartbeat = new Mavlink.mavlink_heartbeat_t() { autopilot = 3, base_mode = 0, custom_mode = 0, mavlink_version = 3, system_status = 4 };
                        byte[] heartbeatBuffer = mavlinkParser.GenerateMAVLinkPacket10(Mavlink.MAVLINK_MSG_ID.HEARTBEAT, heartbeat);
                        lock (serialLock) 
                        {
                            serialPort1.Write(heartbeatBuffer, 0, heartbeatBuffer.Length);
                        }
                        // request data stream
                        //Mavlink.mavlink_request_data_stream_t requestedStream = new Mavlink.mavlink_request_data_stream_t() { req_message_rate = 1, req_stream_id = 0, start_stop = 1, target_component = 0, target_system = 1 };
                        //byte[] requestedStreamBuffer = mavlinkParser.GenerateMAVLinkPacket10(Mavlink.MAVLINK_MSG_ID.REQUEST_DATA_STREAM, requestedStream);
                        //lock (serialLock)
                        //{
                        //    serialPort1.Write(requestedStreamBuffer, 0, requestedStreamBuffer.Length);
                        //}
                        
                        break;
                    }
               
                case (byte)Mavlink.MAVLINK_MSG_ID.CONTROL_SYSTEM:
                    {
                        var payload = (Mavlink.mavlink_control_system_t)message.Payload;
                        String leftPumpState = DecodePumpMask(payload.pump_mask, 1);
                        String rightPumpState = DecodePumpMask(payload.pump_mask, 0);
                        Label labelControlData = FormExchange.GetControl<Label>(nameof(formDados), nameof(labelControlData));
                        
                        labelControlData.BeginInvoke(
                            (Action)(() => labelControlData.Text = $"Sinal Pot:{payload.potentiometer_signal:F2}V\n" +
                            $"Sinal Encoder:{payload.dac_output:F2}V\n" +
                            $"Bomba Esquerda:{leftPumpState}\n" +
                            $"Bomba direita: {rightPumpState}")
                            );           
                        
                        break;
                    }
                case (byte)Mavlink.MAVLINK_MSG_ID.INSTRUMENTATION:
                    {
                        Random random = new Random();
                        var payload = (Mavlink.mavlink_instrumentation_t)message.Payload;
                        Label labelInstrumentationData = FormExchange.GetControl<Label>(formDados.Name, "labelInstrumentationData");
                        labelInstrumentationData.BeginInvoke(new Action(() => labelInstrumentationData.Text = $"Corrente do motor: {payload.current_zero:F2}A\n" +
                        $"Corrente do MPPT: {payload.current_one:F2}A\n" +
                        $"Corrente auxiliar: {payload.current_two:F2}A\n" +
                        $"Tensão do sistema: {payload.battery_voltage:F2}V\n" +
                        $"Temperatura do MPPT: {random.Next(30, 50)}°C\n" +
                        $"Temperatura do Motor: {random.Next(40, 60)}°C"));                    
                        break;
                    }
                case (byte)Mavlink.MAVLINK_MSG_ID.TEMPERATURES:
                    {

                        break;
                    }

                case (byte)Mavlink.MAVLINK_MSG_ID.GPS_GPRMC_SENTENCE:
                    {
                        var payload = (Mavlink.mavlink_gps_gprmc_sentence_t)message.Payload;
                        try
                        {
                            String sentence = Encoding.ASCII.GetString(payload.gprmc_sentence, 8, payload.gprmc_sentence.Length - 8);
                            GPRMCData gpsData = GPRMCParser.Parse(sentence);
                            formMapa.UpdateLocation(gpsData.Latitude, gpsData.Longitude);
                            
                            Console.WriteLine($"GPS received: {gpsData.Latitude}/{gpsData.Longitude}");

                        }
                        catch (Exception exception)
                        {
                            Debug.WriteLine($"{DateTime.Now} --> {exception.Message}");
                        }
                        break;
                    }
                case (byte)Mavlink.MAVLINK_MSG_ID.GPS_LAT_LNG:
                    {
                        break;
                    }

                case (byte)Mavlink.MAVLINK_MSG_ID.NAMED_VALUE_INT:
                    {
                        //var payload = (Mavlink.mavlink_named_value_int_t)message.Payload;
                        //Insert some UI logic here to use this message for testing new variables quickly. Otherwise leave empty.
                        break;
                    }

                default:
                    break;
            }
        }




        private String DecodePumpMask(byte mask, byte index)
        {
            if (Convert.ToBoolean((1 << index) & mask))
            {
                return "Ativa";
            }
            else
            {
                return "Desligada";
            }
        }

        private void WriteBufferConsole(byte[] buffer, string logMessage, bool UseHexMode = false)
        {
            if (logMessage != String.Empty)
                Console.WriteLine(logMessage);

            if (UseHexMode)
            {
                foreach (var item in buffer)
                {
                    Console.WriteLine($"0x{item.ToString("X2")}");
                }
            }
            else
            {
                foreach (var item in buffer)
                {
                    Console.WriteLine($"{item}");
                }
            }
            Console.WriteLine();
        }
   
        private void comboBoxSerialPort_Click(object sender, EventArgs e)
        {
            comboBoxSerialPort.DataSource = SerialPort.GetPortNames();      
        }

        
        private void SetFormLoaderSmall()
        {
            panelFormLoader.Size = new System.Drawing.Size(910, 480);
            panelFormLoader.Dock = DockStyle.Bottom;
        }

        private void buttonGraficos_Click(object sender, EventArgs e)
        {
            button_GenericClickHandler(sender, e);
            panelFormLoader.Controls.Clear();
            panelFormLoader.Dock = DockStyle.Fill;
            panelFormLoader.Controls.Add(formGraficos);
            formGraficos.Show();
            labelTitleSelection.Text = "Gráficos";

        }

        private void buttonDados_Click(object sender, EventArgs e)
        {
            button_GenericClickHandler(sender, e);
            panelFormLoader.Controls.Clear();
            SetFormLoaderSmall();
            panelFormLoader.Controls.Add(formDados);
            formDados.Show();
            Label labelInstrumentationData;
            FormExchange.GetControl<Label>(nameof(formDados), nameof(labelInstrumentationData)).Text = 
                $"Corrente do motor: 5A\n" +
                $"Corrente do MPPT: 10A\n" +
                $"Corrente auxiliar: 2A\n" +
                $"Tensão do sistema: 48V\n" +
                $"Temperatura do MPPT: 40°C\n" +
                $"Temperatura do Motor: 50°C";
            labelTitleSelection.Text = "Dados";
                    
        }

        private void buttonMapa_Click(object sender, EventArgs e)
        {
            button_GenericClickHandler(sender, e);
            panelFormLoader.Controls.Clear();
            panelFormLoader.Dock = DockStyle.Fill;
            panelFormLoader.Controls.Add(formMapa);
            formMapa.Show();
            labelTitleSelection.Text = "Mapa";

        }

        private void buttonConfigurações_Click(object sender, EventArgs e)
        {
            button_GenericClickHandler(sender, e);
            panelFormLoader.Controls.Clear();
            SetFormLoaderSmall();
            panelFormLoader.Controls.Add(formConfigurações);
            labelTitleSelection.Text = "Configurações";
            formConfigurações.Show();

        }


        private void button_GenericClickHandler(object sender, EventArgs e)
        {
            Button button = (Button)sender; // Get the button that was clicked
            panelNav.Height = button.Height; //Position the blue highlight panel
            panelNav.Top = button.Top; //Position the blue highlight panel
            panelNav.Left = button.Left; //Position the blue highlight panel
            panelNav.BringToFront(); // Puts it on top of the other controls
            button.BackColor = Color.FromArgb(46, 51, 73); // Change the button back color.
        }
    
        private void button_Leave(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackColor = Color.FromArgb(24, 30, 54);       
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            // Close application
            System.Windows.Forms.Application.Exit();
        }

        private void comboBox_RemoveBlueHighlight_DropdownClosed(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            comboBox.BeginInvoke(new Action(() => { this.Focus(); }));

        }

        private void buttonLogPacket_Click(object sender, EventArgs e)
        {
            // Send and log a mavlink heartbeat message to the console
            byte[] buffer = mavlinkParser.GenerateMAVLinkPacket10(Mavlink.MAVLINK_MSG_ID.HEARTBEAT,
                new Mavlink.mavlink_heartbeat_t()
                {
                    custom_mode = (uint)Mavlink.MAV_MODE.MANUAL_DISARMED,
                    type = (byte)Mavlink.MAV_TYPE.GCS,
                    autopilot = (byte)Mavlink.MAV_AUTOPILOT.INVALID,
                    base_mode = (byte)Mavlink.MAV_MODE_FLAG.SAFETY_ARMED,
                    system_status = (byte)Mavlink.MAV_STATE.STANDBY,
                    mavlink_version = 1
                }, this.SysIDLocal, this.CompIDLocal);
            serialPort1.Write(buffer, 0, buffer.Length);
            WriteBufferConsole(buffer, "", true);

        }

        private void comboBoxSerialPort_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBoxLogPacket_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxBaudRate_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void labelTitleSelection_Click(object sender, EventArgs e)
        {

        }

        private void barMeter1_Click(object sender, EventArgs e)
        {

        }

        private void buttonPixhawk_Click(object sender, EventArgs e)
        {
            button_GenericClickHandler(sender, e);
            panelFormLoader.Controls.Clear();
            SetFormLoaderSmall();
            panelFormLoader.Controls.Add(formPixhawk);
            formMapa.Show();
            labelTitleSelection.Text = "";
        }

        private void buttonTeste_Click(object sender, EventArgs e)
        {
            button_GenericClickHandler(sender, e);
            panelFormLoader.Controls.Clear();
            SetFormLoaderSmall();
            panelFormLoader.Controls.Add(formTeste);
            formMapa.Show();
            labelTitleSelection.Text = "";
        }
    }
}
