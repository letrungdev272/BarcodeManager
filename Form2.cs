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
    public partial class Form2 : Form
    {
        #region Khai báo biến
        bool Train_Status_1 = false;
        bool Train_Status_2 = false;
        bool Train_Status_3 = false;
        bool Train_Status_4 = false;
        bool Train_Status_5 = false;
        bool Train_Status_6 = false;
        int Time_Delay;
        int i, j, k, a, b, c;
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=barcode_data;Integrated Security=True");
        SqlConnection con1 = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=barcode_data;Integrated Security=True");
        SqlConnection con2 = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=barcode_data;Integrated Security=True");
        SqlConnection con3 = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=barcode_data;Integrated Security=True");
        SqlConnection con4 = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=barcode_data;Integrated Security=True");
        SqlConnection con5 = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=barcode_data;Integrated Security=True");
        SqlConnection con6 = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=barcode_data;Integrated Security=True");
        SqlConnection connect = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=barcode_data;Integrated Security=True");

        //byte[] OK_Beep = new byte[7] { 0x05, 0xE6, 0x04, 0x00, 0x00, 0xfe, 0xfe }; //OK BEEP

        byte[] NG_Beep = new byte[7] { 0x05, 0xE6, 0x04, 0x00, 0x13, 0xfe, 0xfe }; //NG BEEP
        byte[] OK_Beep = new byte[7] { 0x05, 0xE6, 0x04, 0x00, 0x00, 0xff, 0x11 }; //OK BEEP

        byte[] Test_Beep = new byte[7] { 0x05, 0xe6, 0x04, 0x00, 0x18, 0xfe, 0xf9 }; //TEST BEEP
        byte[] Led_Off = new byte[7] { 0x05, 0xE7, 0x04, 0x00, 0x2D, 0xFe, 0xe3 }; //LED OFF
        byte[] Green_Led = new byte[7] { 0x05, 0xE8, 0x04, 0x00, 0x2F, 0xFE, 0xE0 }; //GREEN LED ON
        byte[] Red_Led = new byte[7] { 0x05, 0xE7, 0x04, 0x00, 0x2A, 0xFE, 0xE6 }; //RED LED ON
        byte[] Scan_Enable = new byte[] { 0x04, 0xe9, 0x04, 0x00, 0xff, 0x0f }; //SCAN_ENABLE
        byte[] Scan_Disable = new byte[] { 0x04, 0xea, 0x04, 0x00, 0xff, 0x0e }; //SCAN_DISABLE

        #endregion

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            metroTabControl1.SelectedIndex = 0;
            CheckForIllegalCrossThreadCalls = false;
            txb_timedelay.Text = Properties.Settings.Default.LED_Timer_on.ToString();
            Time_Delay = Convert.ToInt32(txb_timedelay.Text);
            lblTrain1.Text = Properties.Settings.Default.Barcode_Train1;
            lblTrain2.Text = Properties.Settings.Default.Barcode_Train1;
            lblTrain3.Text = Properties.Settings.Default.Barcode_Train2;
            lblTrain4.Text = Properties.Settings.Default.Barcode_Train2;
            lblTrain5.Text = Properties.Settings.Default.Barcode_Train3;
            lblTrain6.Text = Properties.Settings.Default.Barcode_Train3;

            cbbox_1.Text = Properties.Settings.Default.COM_Scanner1;
            cbbox_2.Text = Properties.Settings.Default.COM_Scanner2;
            cbbox_3.Text = Properties.Settings.Default.COM_Scanner3;
            cbbox_4.Text = Properties.Settings.Default.COM_Scanner4;
            cbbox_5.Text = Properties.Settings.Default.COM_Scanner5;
            cbbox_6.Text = Properties.Settings.Default.COM_Scanner6;

            timer1.Enabled = true;
            timer1.Start();
            panel15.BringToFront();
         
        }

        private void button1delay_Click(object sender, EventArgs e)
        {

            Time_Delay = Convert.ToInt32(txb_timedelay.Text);
            Properties.Settings.Default.LED_Timer_on = Time_Delay;
            Properties.Settings.Default.Save();
            //Form2_Load(null, EventArgs.Empty);
        }
        private bool Check_port_1()
        {
            string[] ports = SerialPort.GetPortNames();
            bool stt = false;
            for (int i = 0; i < ports.Length; i++)
            {
                if (ports[i] == cbbox_1.Text)
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
        private bool Check_port_2()
        {
            string[] ports = SerialPort.GetPortNames();
            bool stt = false;
            for (int i = 0; i < ports.Length; i++)
            {
                if (ports[i] == cbbox_2.Text)
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
        private bool Check_port_3()
        {
            string[] ports = SerialPort.GetPortNames();
            bool stt = false;
            for (int i = 0; i < ports.Length; i++)
            {
                if (ports[i] == cbbox_3.Text)
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
        private bool Check_port_4()
        {
            string[] ports = SerialPort.GetPortNames();
            bool stt = false;
            for (int i = 0; i < ports.Length; i++)
            {
                if (ports[i] == cbbox_4.Text)
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
        private bool Check_port_5()
        {
            string[] ports = SerialPort.GetPortNames();
            bool stt = false;
            for (int i = 0; i < ports.Length; i++)
            {
                if (ports[i] == cbbox_5.Text)
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
        private bool Check_port_6()
        {
            string[] ports = SerialPort.GetPortNames();
            bool stt = false;
            for (int i = 0; i < ports.Length; i++)
            {
                if (ports[i] == cbbox_6.Text)
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

        #region Database 
        void demOK()
        {
            string from_day = fromdaypicker.Value.ToString("dd-MM-yyyy");
            string to_day = todaypicker.Value.ToString("dd-MM-yyy");
            string from_time = fromtimepicker.Value.ToString("HH:mm:ss");
            string to_time = totimepicker.Value.ToString("HH:mm:ss");
            con.Open();
            SqlCommand cmd = new SqlCommand("select count (OK_Code) from OK_Code where  Time >= CONVERT(datetime,N'" + from_day + " " + from_time + "',103) and Time <= CONVERT(datetime,N'" + to_day + " " + to_time + "',103);", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                total_OK.Text = reader.GetValue(0).ToString();
            }
            con.Close();
        }
        void demNG()
        {
            string from_day = fromdaypicker.Value.ToString("dd-MM-yyyy");
            string to_day = todaypicker.Value.ToString("dd-MM-yyy");
            string from_time = fromtimepicker.Value.ToString("HH:mm:ss");
            string to_time = totimepicker.Value.ToString("HH:mm:ss");
            con.Open();
            SqlCommand cmd = new SqlCommand("select count (NG_Code) from NG_Code where Time >= CONVERT(datetime,N'" + from_day + " " + from_time + "',103) and Time <= CONVERT(datetime,N'" + to_day + " " + to_time + "',103);", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                total_NG.Text = reader.GetValue(0).ToString();
            }
            con.Close();
        }
        private void search_button_click(object sender, EventArgs e)
        {
            try
            {
                string from_day = fromdaypicker.Value.ToString("dd-MM-yyyy");
                string to_day = todaypicker.Value.ToString("dd-MM-yyyy");
                string from_time = fromtimepicker.Value.ToString("HH:mm:ss");
                string to_time = totimepicker.Value.ToString("HH:mm:ss");
                if (cbb_line.Text == "ALL")
                {
                    connect.Open();
                    SqlCommand cmd = new SqlCommand("select * from NG_Code where  Time >= CONVERT(datetime,N'" + from_day + " " + from_time + "',103) and Time <= CONVERT(datetime,N'" + to_day + " " + to_time + "',103) order by time desc;", connect);
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable table = new DataTable();
                    table.Load(reader);
                    bunifuDataGridView1.DataSource = table;
                    connect.Close();
                    demOK();
                    demNG();
                }
                else
                {
                    connect.Open();
                    SqlCommand cmd = new SqlCommand("select * from NG_Code where Line = '" + cbb_line.Text + "' and Time >= CONVERT(datetime,N'" + from_day + " " + from_time + "',103) and Time <= CONVERT(datetime,N'" + to_day + " " + to_time + "',103) order by time desc;", connect);
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable table = new DataTable();
                    table.Load(reader);
                    bunifuDataGridView1.DataSource = table;
                    connect.Close();

                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("select count (OK_Code) from OK_Code where Line = '" + cbb_line.Text + "' and Time >= CONVERT(datetime,N'" + from_day + " " + from_time + "',103) and Time <= CONVERT(datetime,N'" + to_day + " " + to_time + "',103);", con);
                    SqlDataReader reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        total_OK.Text = reader2.GetValue(0).ToString();
                    }
                    con.Close();
                    con.Open();
                    SqlCommand cmd3 = new SqlCommand("select count (NG_Code) from NG_Code where Line = '" + cbb_line.Text + "' and Time >= CONVERT(datetime,N'" + from_day + " " + from_time + "',103) and Time <= CONVERT(datetime,N'" + to_day + " " + to_time + "',103);", con);
                    SqlDataReader reader3 = cmd3.ExecuteReader();
                    while (reader3.Read())
                    {
                        total_NG.Text = reader3.GetValue(0).ToString();
                    }
                    con.Close();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region TRAIN BUTTON
        private void btn_Train_Line_1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Train_Status_1 == true)
                {
                    Train_Status_1 = false;
                    btn_Train_Line_1.FillColor = Color.Silver;
                    status1.FillColor = Color.Silver;
                    status1.Text = "OK/NG";
                    i = 1;
                }
                else
                { //active
                    Train_Status_1 = true;
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
        private void btn_Train_Line_2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Train_Status_2 == true)
                {
                    Train_Status_2 = false;
                    btn_Train_Line_2.FillColor = Color.Silver;
                    status2.FillColor = Color.Silver;
                    status2.Text = "OK/NG";
                    j = 1;
                }
                else
                { //active
                    Train_Status_2 = true;
                    btn_Train_Line_2.FillColor = Color.DodgerBlue;
                    status2.FillColor = Color.DodgerBlue;
                    status2.Text = "TRAIN";
                    j = 0;
                    Task Task_led = new Task(
                        () =>
                        {
                            while (j == 0)
                            {
                                serialPort2.Write(Red_Led, 0, Red_Led.Length);
                                Thread.Sleep(150);
                                serialPort2.Write(Green_Led, 0, Green_Led.Length);
                                Thread.Sleep(150);
                                if (j != 0) break;
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
        private void btn_Train_Line_3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Train_Status_3 == true)
                {
                    Train_Status_3 = false;
                    btn_Train_Line_3.FillColor = Color.Silver;
                    status3.FillColor = Color.Silver;
                    status3.Text = "OK/NG";
                    k = 1;
                }
                else
                { //active
                    Train_Status_3 = true;
                    btn_Train_Line_3.FillColor = Color.DodgerBlue;
                    status3.FillColor = Color.DodgerBlue;
                    status3.Text = "TRAIN";
                    k = 0;
                    Task Task_led = new Task(
                        () =>
                        {
                            while (k == 0)
                            {
                                serialPort3.Write(Red_Led, 0, Red_Led.Length);
                                Thread.Sleep(150);
                                serialPort3.Write(Green_Led, 0, Green_Led.Length);
                                Thread.Sleep(150);
                                if (k != 0) break;
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
        private void btn_Train_Line_4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Train_Status_4 == true)
                {
                    Train_Status_4 = false;
                    btn_Train_Line_4.FillColor = Color.Silver;
                    status4.FillColor = Color.Silver;
                    status4.Text = "OK/NG";
                    a = 1;
                }
                else
                { //active
                    Train_Status_4 = true;
                    btn_Train_Line_4.FillColor = Color.DodgerBlue;
                    status4.FillColor = Color.DodgerBlue;
                    status4.Text = "TRAIN";
                    a = 0;
                    Task Task_led = new Task(
                        () =>
                        {
                            while (a == 0)
                            {
                                serialPort4.Write(Red_Led, 0, Red_Led.Length);
                                Thread.Sleep(150);
                                serialPort4.Write(Green_Led, 0, Green_Led.Length);
                                Thread.Sleep(150);
                                if (a != 0) break;
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
        private void btn_Train_Line_5_Click(object sender, EventArgs e)
        {
            try
            {
                if (Train_Status_5 == true)
                {
                    Train_Status_5 = false;
                    btn_Train_Line_5.FillColor = Color.Silver;
                    status5.FillColor = Color.Silver;
                    status5.Text = "OK/NG";
                    b = 1;
                }
                else
                { //active
                    Train_Status_5 = true;
                    btn_Train_Line_5.FillColor = Color.DodgerBlue;
                    status5.FillColor = Color.DodgerBlue;
                    status5.Text = "TRAIN";
                    b = 0;
                    Task Task_led = new Task(
                        () =>
                        {
                            while (b == 0)
                            {
                                serialPort5.Write(Red_Led, 0, Red_Led.Length);
                                Thread.Sleep(150);
                                serialPort5.Write(Green_Led, 0, Green_Led.Length);
                                Thread.Sleep(150);
                                if (b != 0) break;
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
        private void btn_Train_Line_6_Click(object sender, EventArgs e)
        {
            try
            {
                if (Train_Status_6 == true)
                {
                    Train_Status_6 = false;
                    btn_Train_Line_6.FillColor = Color.Silver;
                    status6.FillColor = Color.Silver;
                    status6.Text = "OK/NG";
                    c = 1;
                }
                else
                { //active
                    Train_Status_6 = true;
                    btn_Train_Line_6.FillColor = Color.DodgerBlue;
                    status6.FillColor = Color.DodgerBlue;
                    status6.Text = "TRAIN";
                    c = 0;
                    Task Task_led = new Task(
                        () =>
                        {
                            while (c == 0)
                            {
                                serialPort6.Write(Red_Led, 0, Red_Led.Length);
                                Thread.Sleep(150);
                                serialPort6.Write(Green_Led, 0, Green_Led.Length);
                                Thread.Sleep(150);
                                if (c != 0) break;
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
        #endregion

        #region Serial Port
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

            SerialPort sp = (SerialPort)sender;
            string get_code = sp.ReadExisting();
            string traincode;

            if (get_code == "NR")
            {
                btn_Train_Line_1_Click(null, null);
            }
            else if (Train_Status_1 == true && get_code != "NR")
            {
                Properties.Settings.Default.Barcode_Train1 = get_code;
                Properties.Settings.Default.Save();
                lblTrain1.Text = get_code;
                lblTrain2.Text = get_code;
                btn_Train_Line_1_Click(null, null);
                return;
            }
            else if (Train_Status_1 == false)
            {
                lblRead1.Text = get_code;

                traincode = Properties.Settings.Default.Barcode_Train1;
                if (traincode == get_code)
                {
                    con1.Open();
                    SqlCommand cmd = new SqlCommand("insert into dbo.OK_Code values (1,'LINE 1',@get_code,GETDATE());", con1);
                    cmd.Parameters.AddWithValue("@get_code", get_code);
                    cmd.ExecuteNonQuery();
                    con1.Close();
                    Task Task_OK = new Task(() =>
                    {
                        //serialPort1.Write(Green_Led, 0, Green_Led.Length);
                        status1.FillColor = Color.LimeGreen;
                        status1.Text = "OK";
                        Thread.Sleep(Time_Delay);
                        //serialPort1.Write(Led_Off, 0, Led_Off.Length);
                        status1.FillColor = Color.Gray;
                        status1.Text = "...";
                    });
                    Task_OK.Start();
                }
                else
                {
                    con1.Open();
                    SqlCommand cmd = new SqlCommand("insert into dbo.NG_Code values (1,'LINE 1',@get_code,GETDATE());", con1);
                    cmd.Parameters.AddWithValue("@get_code", get_code);
                    cmd.ExecuteNonQuery();
                    con1.Close();
                    serialPort1.Write(NG_Beep, 0, NG_Beep.Length);
                    Task Task_NG = new Task(() =>
                    {
                        //serialPort1.Write(Red_Led, 0, Red_Led.Length);
                        status1.FillColor = Color.Red;
                        status1.Text = "NG";
                        Thread.Sleep(Time_Delay);
                        //serialPort1.Write(Led_Off, 0, Led_Off.Length);
                        status1.FillColor = Color.Gray;
                        status1.Text = "...";
                    });
                    Task_NG.Start();
                }
            }
        }

        private void serialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string get_code = sp.ReadExisting();
            string traincode;

            if (get_code == "NR")
            {
                btn_Train_Line_2_Click(null, null);
            }
            else if (Train_Status_2 == true && get_code != "NR")
            {
                Properties.Settings.Default.Barcode_Train1 = get_code;
                Properties.Settings.Default.Save();
                lblTrain1.Text = get_code;
                lblTrain2.Text = get_code;
                btn_Train_Line_2_Click(null, null);
                return;
            }
            else if (Train_Status_2 == false)
            {
                lblRead2.Text = get_code;

                traincode = Properties.Settings.Default.Barcode_Train1;
                if (traincode == get_code)
                {
                    con2.Open();
                    SqlCommand cmd = new SqlCommand("insert into dbo.OK_Code values (2,'LINE 1',@get_code,GETDATE());", con2);
                    cmd.Parameters.AddWithValue("@get_code", get_code);
                    cmd.ExecuteNonQuery();
                    con2.Close();
                    Task Task_OK = new Task(() =>
                    {
                        serialPort2.Write(Green_Led, 0, Green_Led.Length);
                        status2.FillColor = Color.LimeGreen;
                        status2.Text = "OK";
                        Thread.Sleep(Time_Delay);
                        serialPort2.Write(Led_Off, 0, Led_Off.Length);
                        status2.FillColor = Color.Gray;
                        status2.Text = "...";

                    });
                    Task_OK.Start();
                }
                else
                {
                    con2.Open();
                    SqlCommand cmd = new SqlCommand("insert into dbo.NG_Code values (2,'LINE 1',@get_code,GETDATE());", con2);
                    cmd.Parameters.AddWithValue("@get_code", get_code);
                    cmd.ExecuteNonQuery();
                    con2.Close();
                    byte[] NG_beep = new byte[7] { 0x05, 0xE6, 0x04, 0x00, 0x13, 0xfe, 0xfe }; //NG BEEP
                    serialPort2.Write(NG_beep, 0, NG_beep.Length);
                    i = 0;
                    Task Task_NG = new Task(() =>
                    {
                        serialPort2.Write(Red_Led, 0, Red_Led.Length);
                        status2.FillColor = Color.Red;
                        status2.Text = "NG";
                        Thread.Sleep(Time_Delay);
                        serialPort2.Write(Led_Off, 0, Led_Off.Length);
                        status2.FillColor = Color.Gray;
                        status2.Text = "...";
                        Thread.Sleep(Time_Delay);

                    });
                    Task_NG.Start();
                }
            }
        }

        private void serialPort3_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string get_code = sp.ReadExisting();
            string traincode;

            if (get_code == "NR")
            {
                btn_Train_Line_3_Click(null, null);
            }
            else if (Train_Status_3 == true && get_code != "NR")
            {
                Properties.Settings.Default.Barcode_Train2 = get_code;
                Properties.Settings.Default.Save();
                lblTrain3.Text = get_code;
                lblTrain4.Text = get_code;
                btn_Train_Line_3_Click(null, null);
                return;
            }
            else if (Train_Status_3 == false)
            {
                lblRead3.Text = get_code;

                traincode = Properties.Settings.Default.Barcode_Train2;
                if (traincode == get_code)
                {
                    con3.Open();
                    SqlCommand cmd = new SqlCommand("insert into dbo.OK_Code values (3,'LINE 2',@get_code,GETDATE());", con3);
                    cmd.Parameters.AddWithValue("@get_code", get_code);
                    cmd.ExecuteNonQuery();
                    con3.Close();
                    Task Task_OK = new Task(() =>
                    {
                        serialPort3.Write(Green_Led, 0, Green_Led.Length);
                        status3.FillColor = Color.LimeGreen;
                        status3.Text = "OK";
                        Thread.Sleep(Time_Delay);
                        serialPort3.Write(Led_Off, 0, Led_Off.Length);
                        status3.FillColor = Color.Gray;
                        status3.Text = "...";
                    });
                    Task_OK.Start();
                }
                else
                {
                    con3.Open();
                    SqlCommand cmd = new SqlCommand("insert into dbo.NG_Code values (3,'LINE 2',@get_code,GETDATE());", con3);
                    cmd.Parameters.AddWithValue("@get_code", get_code);
                    cmd.ExecuteNonQuery();
                    con3.Close();
                    serialPort3.Write(NG_Beep, 0, NG_Beep.Length);
                    Task Task_NG = new Task(() =>
                    {
                        serialPort3.Write(Red_Led, 0, Red_Led.Length);
                        status3.FillColor = Color.Red;
                        status3.Text = "NG";
                        Thread.Sleep(Time_Delay);
                        serialPort3.Write(Led_Off, 0, Led_Off.Length);
                        status3.FillColor = Color.Gray;
                        status3.Text = "...";
                    });
                    Task_NG.Start();
                }
            }
        }

        private void serialPort4_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string get_code = sp.ReadExisting();
            string traincode;

            if (get_code == "NR")
            {
                btn_Train_Line_4_Click(null, null);
            }
            else if (Train_Status_4 == true && get_code != "NR")
            {
                Properties.Settings.Default.Barcode_Train2 = get_code;
                Properties.Settings.Default.Save();
                lblTrain3.Text = get_code;
                lblTrain4.Text = get_code;
                btn_Train_Line_4_Click(null, null);
                return;
            }
            else if (Train_Status_4 == false)
            {
                lblRead4.Text = get_code;

                traincode = Properties.Settings.Default.Barcode_Train2;
                if (traincode == get_code)
                {
                    con4.Open();
                    SqlCommand cmd = new SqlCommand("insert into dbo.OK_Code values (4,'LINE 2',@get_code,GETDATE());", con4);
                    cmd.Parameters.AddWithValue("@get_code", get_code);
                    cmd.ExecuteNonQuery();
                    con4.Close();
                    Task Task_OK = new Task(() =>
                    {
                        serialPort4.Write(Green_Led, 0, Green_Led.Length);
                        status4.FillColor = Color.LimeGreen;
                        status4.Text = "OK";
                        Thread.Sleep(Time_Delay);
                        serialPort4.Write(Led_Off, 0, Led_Off.Length);
                        status4.FillColor = Color.Gray;
                        status4.Text = "...";

                    });
                    Task_OK.Start();
                }
                else
                {
                    con4.Open();
                    SqlCommand cmd = new SqlCommand("insert into dbo.NG_Code values (4,'LINE 2',@get_code,GETDATE());", con4);
                    cmd.Parameters.AddWithValue("@get_code", get_code);
                    cmd.ExecuteNonQuery();
                    con4.Close();
                    serialPort4.Write(NG_Beep, 0, NG_Beep.Length);
                    Task Task_NG = new Task(() =>
                    {
                        serialPort4.Write(Red_Led, 0, Red_Led.Length);
                        status4.FillColor = Color.Red;
                        status4.Text = "NG";
                        Thread.Sleep(Time_Delay);
                        serialPort4.Write(Led_Off, 0, Led_Off.Length);
                        status4.FillColor = Color.Gray;
                        status4.Text = "...";
                    });
                    Task_NG.Start();
                }
            }
        }

        private void serialPort5_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string get_code = sp.ReadExisting();
            string traincode;

            if (get_code == "NR")
            {
                btn_Train_Line_5_Click(null, null);
            }
            else if (Train_Status_5 == true && get_code != "NR")
            {
                Properties.Settings.Default.Barcode_Train3 = get_code;
                Properties.Settings.Default.Save();
                lblTrain5.Text = get_code;
                lblTrain6.Text = get_code;
                btn_Train_Line_5_Click(null, null);
                return;
            }
            else if (Train_Status_5 == false)
            {
                lblRead5.Text = get_code;

                traincode = Properties.Settings.Default.Barcode_Train3;
                if (traincode == get_code)
                {
                    con5.Open();
                    SqlCommand cmd = new SqlCommand("insert into dbo.OK_Code values (5,'LINE 3',@get_code,GETDATE());", con5);
                    cmd.Parameters.AddWithValue("@get_code", get_code);
                    cmd.ExecuteNonQuery();
                    con5.Close();
                    Task Task_OK = new Task(() =>
                    {
                        serialPort5.Write(Green_Led, 0, Green_Led.Length);
                        status5.FillColor = Color.LimeGreen;
                        status5.Text = "OK";
                        Thread.Sleep(Time_Delay);
                        serialPort5.Write(Led_Off, 0, Led_Off.Length);
                        status5.FillColor = Color.Gray;
                        status5.Text = "...";

                    });
                    Task_OK.Start();
                }
                else
                {
                    con5.Open();
                    SqlCommand cmd = new SqlCommand("insert into dbo.NG_Code values (5,'LINE 3',@get_code,GETDATE());", con5);
                    cmd.Parameters.AddWithValue("@get_code", get_code);
                    cmd.ExecuteNonQuery();
                    con5.Close();
                    serialPort5.Write(NG_Beep, 0, NG_Beep.Length);
                    Task Task_NG = new Task(() =>
                    {
                        serialPort5.Write(Red_Led, 0, Red_Led.Length);
                        status5.FillColor = Color.Red;
                        status5.Text = "NG";
                        Thread.Sleep(Time_Delay);
                        serialPort5.Write(Led_Off, 0, Led_Off.Length);
                        status5.FillColor = Color.Gray;
                        status5.Text = "...";
                    });
                    Task_NG.Start();
                    //Task_NG.Wait();
                }
            }
        }

        private void serialPort6_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string get_code = sp.ReadExisting();
            string traincode;

            if (get_code == "NR")
            {
                btn_Train_Line_6_Click(null, null);
            }
            else if (Train_Status_6 == true && get_code != "NR")
            {
                Properties.Settings.Default.Barcode_Train3 = get_code;
                Properties.Settings.Default.Save();
                lblTrain5.Text = get_code;
                lblTrain6.Text = get_code;
                btn_Train_Line_6_Click(null, null);
                return;
            }
            else if (Train_Status_6 == false)
            {
                lblRead6.Text = get_code;

                traincode = Properties.Settings.Default.Barcode_Train3;
                if (traincode == get_code)
                {
                    con6.Open();
                    SqlCommand cmd = new SqlCommand("insert into dbo.OK_Code values (6,'LINE 3',@get_code,GETDATE());", con6);
                    cmd.Parameters.AddWithValue("@get_code", get_code);
                    cmd.ExecuteNonQuery();
                    con6.Close();
                    Task Task_OK = new Task(() =>
                    {
                        serialPort6.Write(Green_Led, 0, Green_Led.Length);
                        status6.FillColor = Color.LimeGreen;
                        status6.Text = "OK";
                        Thread.Sleep(Time_Delay);
                        serialPort6.Write(Led_Off, 0, Led_Off.Length);
                        status6.FillColor = Color.Gray;
                        status6.Text = "...";

                    });
                    Task_OK.Start();
                }
                else
                {
                    con6.Open();
                    SqlCommand cmd = new SqlCommand("insert into dbo.NG_Code values (6,'LINE 3',@get_code,GETDATE());", con6);
                    cmd.Parameters.AddWithValue("@get_code", get_code);
                    cmd.ExecuteNonQuery();
                    con6.Close();
                    serialPort6.Write(NG_Beep, 0, NG_Beep.Length);
                    Task Task_NG = new Task(() =>
                    {
                        serialPort6.Write(Red_Led, 0, Red_Led.Length);
                        status6.FillColor = Color.Red;
                        status6.Text = "NG";
                        Thread.Sleep(Time_Delay);
                        serialPort6.Write(Led_Off, 0, Led_Off.Length);
                        status6.FillColor = Color.Gray;
                        status6.Text = "...";
                    });
                    Task_NG.Start();
                }
            }
        }
        #endregion

        #region Combobox show port
        private void cbbox_1_Click(object sender, EventArgs e)
        {
            cbbox_1.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            cbbox_1.Items.AddRange(ports);
        }

        private void cbbox_2_Click(object sender, EventArgs e)
        {
            cbbox_2.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            cbbox_2.Items.AddRange(ports);
        }

        private void cbbox_3_Click(object sender, EventArgs e)
        {
            cbbox_3.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            cbbox_3.Items.AddRange(ports);
        }

        private void cbbox_4_Click(object sender, EventArgs e)
        {
            cbbox_4.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            cbbox_4.Items.AddRange(ports);
        }

        private void cbbox_5_Click(object sender, EventArgs e)
        {
            cbbox_5.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            cbbox_5.Items.AddRange(ports);
        }

        private void cbbox_6_Click(object sender, EventArgs e)
        {
            cbbox_6.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            cbbox_6.Items.AddRange(ports);
        }
        #endregion

        #region Open port
        private void openport_1_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = cbbox_1.Text;
                serialPort1.Open();
                openport_1.FillColor = Color.Lime;
                close_port_1.FillColor = Color.Gray;
                Properties.Settings.Default.COM_Scanner1 = cbbox_1.Text;
                Properties.Settings.Default.Save();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openport_2_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort2.PortName = cbbox_2.Text;
                serialPort2.Open();
                openport_2.FillColor = Color.Lime;
                close_port_2.FillColor = Color.Gray;
                Properties.Settings.Default.COM_Scanner2 = cbbox_2.Text;
                Properties.Settings.Default.Save();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openport_3_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort3.PortName = cbbox_3.Text;
                serialPort3.Open();
                openport_3.FillColor = Color.Lime;
                close_port_3.FillColor = Color.Gray;
                Properties.Settings.Default.COM_Scanner3 = cbbox_3.Text;
                Properties.Settings.Default.Save();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openport_4_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort4.PortName = cbbox_4.Text;
                serialPort4.Open();
                openport_4.FillColor = Color.Lime;
                close_port_4.FillColor = Color.Gray;
                Properties.Settings.Default.COM_Scanner4 = cbbox_4.Text;
                Properties.Settings.Default.Save();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void openport_5_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort5.PortName = cbbox_5.Text;
                serialPort5.Open();
                openport_5.FillColor = Color.Lime;
                close_port_5.FillColor = Color.Gray;
                Properties.Settings.Default.COM_Scanner5 = cbbox_5.Text;
                Properties.Settings.Default.Save();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openport_6_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort6.PortName = cbbox_6.Text;
                serialPort6.Open();
                openport_6.FillColor = Color.Lime;
                close_port_6.FillColor = Color.Gray;
                Properties.Settings.Default.COM_Scanner6 = cbbox_6.Text;
                Properties.Settings.Default.Save();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Close port
        private void close_port_1_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            openport_1.FillColor = Color.Gray;
            close_port_1.FillColor = Color.Red;
        }
        private void close_port_2_Click(object sender, EventArgs e)
        {
            serialPort2.Close();
            openport_2.FillColor = Color.Gray;
            close_port_2.FillColor = Color.Red;
        }
        private void close_port_3_Click(object sender, EventArgs e)
        {
            serialPort3.Close();
            openport_3.FillColor = Color.Gray;
            close_port_3.FillColor = Color.Red;
        }
        private void close_port_4_Click(object sender, EventArgs e)
        {
            serialPort4.Close();
            openport_4.FillColor = Color.Gray;
            close_port_4.FillColor = Color.Red;
        }
        private void close_port_5_Click(object sender, EventArgs e)
        {
            serialPort5.Close();
            openport_5.FillColor = Color.Gray;
            close_port_5.FillColor = Color.Red;
        }
        private void close_port_6_Click(object sender, EventArgs e)
        {
            serialPort6.Close();
            openport_6.FillColor = Color.Gray;
            close_port_6.FillColor = Color.Red;
        }
        #endregion

        #region No use



        private void DATA_Click(object sender, EventArgs e)
        {

        }

        private void todaypicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void fromdaypicker_ValueChanged(object sender, EventArgs e)
        {

        }
        private void tabPage3_Click(object sender, EventArgs e)
        {

        }
        private void status2_Click(object sender, EventArgs e)
        {

        }
        private void fromtimepicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void totimepicker_ValueChanged(object sender, EventArgs e)
        {

        }
        private void timer2_Tick(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Test port
        private void Test_com_1_Click(object sender, EventArgs e)
        {
            Task Task_Test = new Task(() =>
            {
                try
                {
                    for (int test = 0; test < 5; test++)
                    {
                        serialPort1.Write(Green_Led, 0, Green_Led.Length);
                        Thread.Sleep(200);
                        serialPort1.Write(Led_Off, 0, Led_Off.Length);
                        Thread.Sleep(200);
                        serialPort1.Write(Green_Led, 0, Green_Led.Length);
                    }
                    serialPort1.Write(Test_Beep, 0, Test_Beep.Length);
                    //MessageBox.Show(cbbox_1.Text + " Test OK", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
            Task_Test.Start();
        }
        private void Test_com_2_Click(object sender, EventArgs e)
        {
            Task Task_Test = new Task(() =>
            {
                try
                {
                    for (int test = 0; test < 5; test++)
                    {
                        serialPort2.Write(Green_Led, 0, Green_Led.Length);
                        Thread.Sleep(200);
                        serialPort2.Write(Led_Off, 0, Led_Off.Length);
                        Thread.Sleep(200);
                        serialPort2.Write(Green_Led, 0, Green_Led.Length);
                    }
                    serialPort2.Write(Test_Beep, 0, Test_Beep.Length);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
            Task_Test.Start();
        }
        private void Test_com_3_Click(object sender, EventArgs e)
        {
            Task Task_Test = new Task(() =>
            {
                try
                {
                    for (int test = 0; test < 5; test++)
                    {
                        serialPort3.Write(Green_Led, 0, Green_Led.Length);
                        Thread.Sleep(200);
                        serialPort3.Write(Led_Off, 0, Led_Off.Length);
                        Thread.Sleep(200);
                        serialPort3.Write(Green_Led, 0, Green_Led.Length);
                    }
                    serialPort3.Write(Test_Beep, 0, Test_Beep.Length);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
            Task_Test.Start();
        }
        private void Test_com_4_Click(object sender, EventArgs e)
        {
            Task Task_Test = new Task(() =>
            {
                try
                {
                    for (int test = 0; test < 5; test++)
                    {
                        serialPort4.Write(Green_Led, 0, Green_Led.Length);
                        Thread.Sleep(200);
                        serialPort4.Write(Led_Off, 0, Led_Off.Length);
                        Thread.Sleep(200);
                        serialPort4.Write(Green_Led, 0, Green_Led.Length);
                    }
                    serialPort4.Write(Test_Beep, 0, Test_Beep.Length);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
            Task_Test.Start();
        }
        private void Test_com_5_Click(object sender, EventArgs e)
        {
            Task Task_Test = new Task(() =>
            {
                try
                {
                    for (int test = 0; test < 5; test++)
                    {
                        serialPort5.Write(Green_Led, 0, Green_Led.Length);
                        Thread.Sleep(200);
                        serialPort5.Write(Led_Off, 0, Led_Off.Length);
                        Thread.Sleep(200);
                        serialPort5.Write(Green_Led, 0, Green_Led.Length);
                    }
                    serialPort5.Write(Test_Beep, 0, Test_Beep.Length);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
            Task_Test.Start();
        }
        private void Test_com_6_Click(object sender, EventArgs e)
        {
            Task Task_Test = new Task(() =>
            {
                try
                {
                    for (int test = 0; test < 5; test++)
                    {
                        serialPort6.Write(Green_Led, 0, Green_Led.Length);
                        Thread.Sleep(200);
                        serialPort6.Write(Led_Off, 0, Led_Off.Length);
                        Thread.Sleep(200);
                        serialPort6.Write(Green_Led, 0, Green_Led.Length);
                    }
                    serialPort6.Write(Test_Beep, 0, Test_Beep.Length);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
            Task_Test.Start();
        }
        #endregion

        private void Switch_Line_1_CheckedChanged(object sender, EventArgs e)
        {
            if (Switch_Line_1.Checked == true)
            {
                try
                {
                    serialPort1.Write(Scan_Enable, 0, Scan_Enable.Length);
                    serialPort2.Write(Scan_Enable, 0, Scan_Enable.Length);
                    serialPort1.Write(Green_Led, 0, Green_Led.Length);
                    serialPort2.Write(Green_Led, 0, Green_Led.Length);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    serialPort1.Write(Scan_Disable, 0, Scan_Disable.Length);
                    serialPort2.Write(Scan_Disable, 0, Scan_Disable.Length);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #region Switch ON/OFF Line
        private void Switch_Line_2_CheckedChanged(object sender, EventArgs e)
        {
            if (Switch_Line_2.Checked == true)
            {
                try
                {
                    serialPort3.Write(Scan_Enable, 0, Scan_Enable.Length);
                    serialPort4.Write(Scan_Enable, 0, Scan_Enable.Length);
                    serialPort3.Write(Green_Led, 0, Green_Led.Length);
                    serialPort4.Write(Green_Led, 0, Green_Led.Length);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    serialPort3.Write(Scan_Disable, 0, Scan_Disable.Length);
                    serialPort4.Write(Scan_Disable, 0, Scan_Disable.Length);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Switch_Line_3_CheckedChanged(object sender, EventArgs e)
        {
            if (Switch_Line_3.Checked == true)
            {
                try
                {
                    serialPort5.Write(Scan_Enable, 0, Scan_Enable.Length);
                    serialPort6.Write(Scan_Enable, 0, Scan_Enable.Length);
                    serialPort5.Write(Green_Led, 0, Green_Led.Length);
                    serialPort6.Write(Green_Led, 0, Green_Led.Length);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    serialPort5.Write(Scan_Disable, 0, Scan_Disable.Length);
                    serialPort6.Write(Scan_Disable, 0, Scan_Disable.Length);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion
        private void btn_Restart_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn khởi động lại?", "Restart", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Process.Start("shutdown.exe", "-r -t 03");
                Application.Exit();

            }
        }
        private void cbbox_1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void cbbox_2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void cbbox_3_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void cbbox_4_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void cbbox_5_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void cbbox_6_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btn_save_com_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.COM_Scanner1 = cbbox_1.Text;
            Properties.Settings.Default.COM_Scanner2 = cbbox_2.Text;
            Properties.Settings.Default.COM_Scanner3 = cbbox_3.Text;
            Properties.Settings.Default.COM_Scanner4 = cbbox_4.Text;
            Properties.Settings.Default.COM_Scanner5 = cbbox_5.Text;
            Properties.Settings.Default.COM_Scanner6 = cbbox_6.Text;
            Properties.Settings.Default.Save();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btn_close_1_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            cbbox_1.Text = string.Empty;
        }

        private void btn_close_2_Click(object sender, EventArgs e)
        {
            serialPort2.Close();
            cbbox_2.Text = string.Empty;
        }

        private void btn_close_3_Click(object sender, EventArgs e)
        {
            serialPort3.Close();
            cbbox_3.Text = string.Empty;
        }

        private void btn_close_4_Click(object sender, EventArgs e)
        {
            serialPort4.Close();
            cbbox_4.Text = string.Empty;
        }

        private void btn_close_5_Click(object sender, EventArgs e)
        {
            serialPort5.Close();
            cbbox_5.Text = string.Empty;
        }

        private void btn_close_6_Click(object sender, EventArgs e)
        {
            serialPort6.Close();
            cbbox_6.Text = string.Empty;
        }

        private void metroTabPage3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            panel15.Visible = true;
            txb_login.Text = "";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (txb_login.Text == "123456")
            {
                panel15.Visible = false;
            }
        }

        private void Switch_1_CheckedChanged(object sender, EventArgs e)
        {
            if (Switch_1.Checked == true)
            {
                timer1.Enabled = false;
                timer1.Stop();
                panel14.Enabled = true;
                panel14.Visible = true;
            }
            else
            {
                timer1.Enabled = true;
                timer1.Start();
                panel14.Enabled = false;
                panel14.Visible = false;
            }
        }
        private void Enable_Setting_Click(object sender, EventArgs e)
        {
            //bool setting = false;

            //if (setting == true)
            //{
            //    panel_setting.Enabled = true;
            //    btn_Enable_Setting.FillColor = Color.FromArgb(94, 148, 255);
            //    setting = false;
            //}
            //else
            //{
            //    panel_setting.Enabled = false;
            //    btn_Enable_Setting.FillColor = Color.Gray;
            //    setting = true;
            //}
        }
        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (metroTabControl1.SelectedIndex == 2)
            //{
            //    timer1.Enabled = false;
            //    timer1.Stop();
            //}
            //else
            //{
            //    timer1.Enabled = true;
            //    timer1.Start();
            //}
            panel15.Visible = true;
            txb_login.Text = "";
        }
        private void btn_Shutdown_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn tắt máy?", "Shutdown", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Process.Start("shutdown.exe", "-s -t 03");
                Application.Exit();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            bool check_1 = Check_port_1();
            if (check_1 == false || serialPort1.IsOpen == false)
            {
                lblStatus1.Text = "DISCONNECTED";
                lblStatus1.ForeColor = Color.Red;
                Test_com_1.ForeColor = Color.Red;
                try
                {
                    serialPort1.PortName = cbbox_1.Text;
                    serialPort1.Open();
                }
                catch (Exception) { }
            }
            else if (check_1 == true || serialPort1.IsOpen == true)
            {
                lblStatus1.Text = "CONNECTED";
                lblStatus1.ForeColor = Color.Lime;
                Test_com_1.ForeColor = Color.Lime;
            }

            bool check_2 = Check_port_2();
            if (check_2 == false || serialPort2.IsOpen == false)
            {
                lblStatus2.Text = "DISCONNECTED";
                lblStatus2.ForeColor = Color.Red;
                Test_com_2.ForeColor = Color.Red;

                try
                {
                    serialPort2.PortName = cbbox_2.Text;
                    serialPort2.Open();
                }
                catch (Exception) { }
            }
            else if (check_2 == true || serialPort2.IsOpen == true)
            {
                lblStatus2.Text = "CONNECTED";
                lblStatus2.ForeColor = Color.Lime;
                Test_com_2.ForeColor = Color.Lime;
            }

            bool check_3 = Check_port_3();
            if (check_3 == false || serialPort3.IsOpen == false)
            {
                lblStatus3.Text = "DISCONNECTED";
                lblStatus3.ForeColor = Color.Red;
                Test_com_3.ForeColor = Color.Red;

                try
                {
                    serialPort3.PortName = cbbox_3.Text;
                    serialPort3.Open();
                }
                catch (Exception) { }
            }
            else if (check_3 == true || serialPort3.IsOpen == true)
            {
                lblStatus3.Text = "CONNECTED";
                lblStatus3.ForeColor = Color.Lime;
                Test_com_3.ForeColor = Color.Lime;
            }

            bool check_4 = Check_port_4();
            if (check_4 == false || serialPort4.IsOpen == false)
            {
                lblStatus4.Text = "DISCONNECTED";
                lblStatus4.ForeColor = Color.Red;
                Test_com_4.ForeColor = Color.Red;

                try
                {
                    serialPort4.PortName = cbbox_4.Text;
                    serialPort4.Open();
                }
                catch (Exception) { }
            }
            else if (check_4 == true || serialPort4.IsOpen == true)
            {
                lblStatus4.Text = "CONNECTED";
                lblStatus4.ForeColor = Color.Lime;
                Test_com_4.ForeColor = Color.Lime;

            }

            bool check_5 = Check_port_5();
            if (check_5 == false || serialPort5.IsOpen == false)
            {
                lblStatus5.Text = "DISCONNECTED";
                lblStatus5.ForeColor = Color.Red;
                Test_com_5.ForeColor = Color.Red;

                try
                {
                    serialPort5.PortName = cbbox_5.Text;
                    serialPort5.Open();
                }
                catch (Exception) { }
            }
            else if (check_5 == true || serialPort5.IsOpen == true)
            {
                lblStatus5.Text = "CONNECTED";
                lblStatus5.ForeColor = Color.Lime;
                Test_com_5.ForeColor = Color.Lime;
            }

            bool check_6 = Check_port_6();
            if (check_6 == false || serialPort6.IsOpen == false)
            {
                lblStatus6.Text = "DISCONNECTED";
                lblStatus6.ForeColor = Color.Red;
                Test_com_6.ForeColor = Color.Red;

                try
                {
                    serialPort6.PortName = cbbox_6.Text;
                    serialPort6.Open();
                }
                catch (Exception) { }
            }
            else if (check_6 == true || serialPort6.IsOpen == true)
            {
                lblStatus6.Text = "CONNECTED";
                lblStatus6.ForeColor = Color.Lime;
                Test_com_6.ForeColor = Color.Lime;

            }
        }
 
    }
}
