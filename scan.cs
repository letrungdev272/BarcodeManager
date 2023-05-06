using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barcode_Tiger
{
    public partial class scan : UserControl
    {
        bool Train_Status = false;
        byte[] NG_Beep = new byte[7] { 0x05, 0xE6, 0x04, 0x00, 0x13, 0xfe, 0xfe }; //NG BEEP
        byte[] Test_Beep = new byte[7] { 0x05, 0xe6, 0x04, 0x00, 0x18, 0xfe, 0xf9 }; //TEST BEEP
        byte[] Led_Off = new byte[7] { 0x05, 0xE7, 0x04, 0x00, 0x2D, 0xFe, 0xe3 }; //LED OFF
        byte[] Green_Led = new byte[7] { 0x05, 0xE8, 0x04, 0x00, 0x2F, 0xFE, 0xE0 }; //GREEN LED ON
        byte[] Red_Led = new byte[7] { 0x05, 0xE7, 0x04, 0x00, 0x2A, 0xFE, 0xE6 }; //RED LED ON
        byte[] Scan_Enable = new byte[] { 0x04, 0xe9, 0x04, 0x00, 0xff, 0x0f }; //SCAN_ENABLE
        byte[] Scan_Disable = new byte[] { 0x04, 0xea, 0x04, 0x00, 0xff, 0x0e }; //SCAN_DISABLE
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=barcode_data;Integrated Security=True");
        int Time_Delay, i, _BarCode_Train_status, _COM_Port_Status;
        private string _ComPort;

        public int COM_Port_Status
        {
            get { return _COM_Port_Status; }
            set { _COM_Port_Status = value; }
        }
        public int BarCode_Train_status
        {
            get { return _BarCode_Train_status; }
            set { _BarCode_Train_status = value; }
        }


        public scan()
        {
            InitializeComponent();
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string get_code = sp.ReadExisting();
            string traincode;

            if (get_code == "NR")
            {
                btn_Train_Line_1_Click(null, null);
            }
            else if (Train_Status == true && get_code != "NR")
            {
                Properties.Settings.Default.Barcode_Train1 = get_code;
                Properties.Settings.Default.Save();
                lblTrain1.Text = get_code;
                btn_Train_Line_1_Click(null, null);
                return;
            }
            else if (Train_Status == false)
            {
                lblRead1.Text = get_code;

                traincode = Properties.Settings.Default.Barcode_Train1;
                if (traincode == get_code)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into dbo.OK_Code values (1,'LINE 1',@get_code,GETDATE());", con);
                    cmd.Parameters.AddWithValue("@get_code", get_code);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Task Task_OK = new Task(() =>
                    {
                        serialPort1.Write(Green_Led, 0, Green_Led.Length);
                        status1.FillColor = Color.LimeGreen;
                        status1.Text = "OK";
                        Thread.Sleep(Time_Delay);
                        serialPort1.Write(Led_Off, 0, Led_Off.Length);
                        status1.FillColor = Color.Gray;
                        status1.Text = "...";
                    });
                    Task_OK.Start();
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into dbo.NG_Code values (1,'LINE 1',@get_code,GETDATE());", con);
                    cmd.Parameters.AddWithValue("@get_code", get_code);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    serialPort1.Write(NG_Beep, 0, NG_Beep.Length);
                    Task Task_NG = new Task(() =>
                    {
                        serialPort1.Write(Red_Led, 0, Red_Led.Length);
                        status1.FillColor = Color.Red;
                        status1.Text = "NG";
                        Thread.Sleep(Time_Delay);
                        serialPort1.Write(Led_Off, 0, Led_Off.Length);
                        status1.FillColor = Color.Gray;
                        status1.Text = "...";
                    });
                    Task_NG.Start();
                }
            }
        }

        private void scan_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            timer1.Enabled = true;
            timer1.Start();

            Time_Delay = Properties.Settings.Default.LED_Timer_on;
        }

        private void btn_Train_Line_1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Train_Status == true)
                {
                    Train_Status = false;
                    btn_Train_Line_1.FillColor = Color.Silver;
                    status1.FillColor = Color.Silver;
                    status1.Text = "OK/NG";
                    i = 1;
                }
                else
                { //active
                    Train_Status = true;
                    btn_Train_Line_1.FillColor = Color.DodgerBlue;
                    status1.FillColor = Color.DodgerBlue;
                    status1.Text = "TRAIN";
                    i = 0;
                    Task Task_led = new Task(
                        () =>
                        {
                            while (i == 0)
                            {
                                serialPort1.Write(Red_Led, 0, Red_Led.Length);
                                Thread.Sleep(150);
                                serialPort1.Write(Green_Led, 0, Green_Led.Length);
                                Thread.Sleep(150);
                                if (i != 0) break;
                            }
                        });
                    Task_led.Start();

                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool Check_port()
        {
            string[] ports = SerialPort.GetPortNames();
            bool stt = false;
            for (int i = 0; i < ports.Length; i++)
            {
                if (ports[i] == comboBox1.Text)
                {
                    stt = true;
                    break;
                }
                else
                {
                    stt = false;
                }
            }
            return stt;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            bool check = Check_port();
            if (check == false || serialPort1.IsOpen == false)
            {
                lblStatus1.Text = "DISCONNECTED";
                lblStatus1.ForeColor = Color.Red;
                try
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.Open();
                }
                catch (Exception) { }
            }
            else if (check == true || serialPort1.IsOpen == true)
            {
                lblStatus1.Text = "CONNECTED";
                lblStatus1.ForeColor = Color.Lime;
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);
        }
    }
}
