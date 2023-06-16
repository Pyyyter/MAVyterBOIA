using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleExample
{
    public partial class FormPixhawk : Form
    {
        private readonly GroundStation groundStation;
        private readonly object serialLock = new object();
        private readonly Mavlink.MavlinkParser mavlinkParserPixhawk = new Mavlink.MavlinkParser();
        
        public FormPixhawk(GroundStation groundStation)
        {
            InitializeComponent();
            this.groundStation = groundStation;

            // Get enum values from MAV_CMD enum
            foreach (var item in Enum.GetValues(typeof(Mavlink.MAV_CMD)))
            {
                comboBoxCommandSelection.Items.Add(item.ToString());
            }
        }

        
        private void buttonEnviar_Click(object sender, EventArgs e)
        {
            // Get selected command
            Mavlink.MAV_CMD selectedCommandEnum = (Mavlink.MAV_CMD)Enum.Parse(typeof(Mavlink.MAV_CMD), comboBoxCommandSelection.SelectedItem.ToString());
            
            // Parse 7 parameters from the command selection textbox
            string[] parameterInputs = textBoxCommandParameters.Texts.Split(',').Select(x => x.Trim()).ToArray();
            string[] parameters = new string[7];
            for (int i = 0; i < parameters.Length; i++)
            {
                if (i < parameterInputs.Length)
                {
                    parameters[i] = parameterInputs[i];
                }
                else
                {
                    parameters[i] = "0";
                }
            }

            Mavlink.mavlink_command_long_t command = new Mavlink.mavlink_command_long_t() {
                param1 = float.Parse(parameters[0]),
                param2 = float.Parse(parameters[1]),
                param3 = float.Parse(parameters[2]),
                param4 = float.Parse(parameters[3]),
                param5 = float.Parse(parameters[4]),
                param6 = float.Parse(parameters[5]),
                param7 = float.Parse(parameters[6]),
                command = (ushort)selectedCommandEnum,
                target_system = 0,
                target_component = 0,
                confirmation = 0
            };


            byte[] messageBuffer = mavlinkParserPixhawk.GenerateMAVLinkPacket10((Mavlink.MAVLINK_MSG_ID)selectedCommandEnum, command);
            // Send the message to the serial port
            lock (serialLock)
            {
                groundStation.serialPort1.Write(messageBuffer, 0, messageBuffer.Length);
                //foreach (byte b in messageBuffer)
                //{
                //    
                //    Console.WriteLine(b.ToString("X2"));
                //}

            }
                
        }      
    } 
}
