using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.IO;
using System.Data.SqlClient;

namespace Barcode_Tiger
{
    public partial class Form1 : Form
    {

        bool train_button = false;
        int i;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer_nhayden.Enabled = true;

            CheckForIllegalCrossThreadCalls = false;
            //serialPort1.PortName = "COM5";
            //serialPort1.Open();
            //serialPort2.PortName = "COM12";
            //serialPort2.Open();
            train_sidelabel.Text = Properties.Settings.Default.Barcode_Train1;
            train_toplabel.Text = Properties.Settings.Default.Barcode_Train1;
            guna2TileButton1.FillColor = Color.Gainsboro;
            guna2TileButton2.FillColor = Color.Gainsboro;
            guna2TileButton1.Text = "OK/NG";
            guna2TileButton2.Text = "OK/NG";

            //this.FormBorderStyle = FormBorderStyle.None;
            //this.Left = 0;
            //this.Top = 0;
            //this.Bounds = Screen.PrimaryScreen.Bounds;
//hi

        }
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=barcode_data;Integrated Security=True");
        SqlConnection con2 = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=barcode_data;Integrated Security=True");
        SqlConnection con3 = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=barcode_data;Integrated Security=True");
        SqlConnection con4 = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=barcode_data;Integrated Security=True");
        SqlConnection con5 = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=barcode_data;Integrated Security=True");
        SqlConnection con6 = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=barcode_data;Integrated Security=True");


        void demOK()
        {
            string from_day = fromdaypicker.Value.ToString("dd-MM-yyyy");
            string to_day = todaypicker.Value.ToString("dd-MM-yyy");
            string from_time = fromtimepicker.Value.ToString("HH:mm:ss");
            string to_time = totimepicker.Value.ToString("HH:mm:ss");

            con.Open();
            SqlCommand cmd = new SqlCommand("select count (OK_Code) from OK_Code where ID_Scanner = 1 and Time >= CONVERT(datetime,N'"+ from_day+" " + from_time + "',103) and Time <= CONVERT(datetime,N'" + to_day + " " + to_time + "',103);", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                totalOK1.Text = reader.GetValue(0).ToString();
            }
            con.Close();

            con2.Open();
            SqlCommand cmd2 = new SqlCommand("select count (OK_Code) from OK_Code where ID_Scanner = 2  and Time >= CONVERT(datetime,N'" + from_day + " " + from_time + "',103) and Time <= CONVERT(datetime,N'" + to_day + " " + to_time + "',103);", con2);
            SqlDataReader reader2 = cmd2.ExecuteReader();
            while (reader2.Read())
            {
                totalOK2.Text = reader2.GetValue(0).ToString();
            }
            con2.Close();

        }
        void demNG()
        {
            string from_day = fromdaypicker.Value.ToString("dd-MM-yyyy");
            string to_day = todaypicker.Value.ToString("dd-MM-yyy");
            string from_time = fromtimepicker.Value.ToString("HH:mm:ss");
            string to_time = totimepicker.Value.ToString("HH:mm:ss");

            con.Open();
            SqlCommand cmd = new SqlCommand("select count (NG_Code) from NG_Code where ID_Scanner = 1 and Time >= CONVERT(datetime,N'" + from_day + " " + from_time + "',103) and Time <= CONVERT(datetime,N'" + to_day + " " + to_time + "',103);", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                totalNG1.Text = reader.GetValue(0).ToString();
            }
            con.Close();

            con2.Open();
            SqlCommand cmd2 = new SqlCommand("select count (NG_Code) from NG_Code where ID_Scanner = 2  and Time >= CONVERT(datetime,N'" + from_day + " " + from_time + "',103) and Time <= CONVERT(datetime,N'" + to_day + " " + to_time + "',103);", con2);
            SqlDataReader reader2 = cmd2.ExecuteReader();
            while (reader2.Read())
            {
                totalNG2.Text = reader2.GetValue(0).ToString();
            }
            con2.Close();

        }

        void NG_list()
        {
            string from_day = fromdaypicker.Value.ToString("dd-MM-yyyy");
            string to_day = todaypicker.Value.ToString("dd-MM-yyy");
            string from_time = fromtimepicker.Value.ToString("HH:mm:ss");
            string to_time = totimepicker.Value.ToString("HH:mm:ss");
            SqlConnection connect = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=barcode_data;Integrated Security=True");
            connect.Open();
            SqlCommand cmd = new SqlCommand("select * from NG_Code where  Time >= CONVERT(datetime,N'" + from_day + " " + from_time + "',103) and Time <= CONVERT(datetime,N'" + to_day + " " + to_time + "',103) order by time desc;", connect) ;
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            bunifuDataGridView1.DataSource = table;
            connect.Close();
        }
        private void guna2Button8_Click(object sender, EventArgs e)
        {
            demOK();
            demNG();
            NG_list();
            int OK,NG;
            OK = Convert.ToInt32(totalOK1.Text) + Convert.ToInt32(totalOK2.Text);
            NG = Convert.ToInt32(totalNG1.Text) + Convert.ToInt32(totalNG2.Text);
            total_OK.Text = OK.ToString();
            total_NG.Text = NG.ToString();
        }
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (train_button == true)
            {
                TrainButton.FillColor = Color.FromArgb(94, 148, 255);
         
                train_button = false;
                guna2TileButton1.FillColor = Color.Gainsboro;
                guna2TileButton2.FillColor = Color.Gainsboro;
                guna2TileButton1.Text = "OK/NG";
                guna2TileButton2.Text = "OK/NG";
                timer_nhayden.Enabled = false;
                i = 1;
            }
            else
            { //active
                TrainButton.FillColor = Color.Green;
             
                train_button = true;
                guna2TileButton1.FillColor = Color.DodgerBlue;
                guna2TileButton2.FillColor = Color.DodgerBlue;
                guna2TileButton1.Text = "Train";
                guna2TileButton2.Text = "Train";
                i = 0;
                Task Task_led = new Task(
                    () =>
                    {
                        while (i==0)
                        {
                            byte[] bytesToSend_beep = new byte[7] { 0x05, 0xE7, 0x04, 0x00, 0x2A, 0xFE, 0xE6 }; //GREEN LED OFF
                            serialPort1.Write(bytesToSend_beep, 0, bytesToSend_beep.Length);

                            byte[] bytesToSend_beep2 = new byte[7] { 0x05, 0xE7, 0x04, 0x00, 0x2A, 0xFE, 0xE6 }; //GREEN LED OFF
                            serialPort2.Write(bytesToSend_beep, 0, bytesToSend_beep2.Length);
                            Thread.Sleep(300);
                            byte[] bytesToSend_led = new byte[7] { 0x05, 0xE8, 0x04, 0x00, 0x2F, 0xFE, 0xE0 }; //GREEN LED ON
                            serialPort1.Write(bytesToSend_led, 0, bytesToSend_led.Length);

                            byte[] bytesToSend_led2 = new byte[7] { 0x05, 0xE8, 0x04, 0x00, 0x2F, 0xFE, 0xE0 }; //GREEN LED ON
                            serialPort2.Write(bytesToSend_led, 0, bytesToSend_led2.Length);

                            Thread.Sleep(300);
                            if (i != 0) break;
                        }
                    });
                Task_led.Start();
                //timer_nhayden.Enabled = true;
                //timer_nhayden.Start();

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
         
        }
        
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string get_code = sp.ReadExisting();
            string traincode;
            label8.Text = get_code;
            if (get_code == "NR")
            {
                guna2Button5_Click(null, null);
            }
            else if (train_button == true && get_code != "NR")
            {
                Properties.Settings.Default.Barcode_Train1 = get_code;
                Properties.Settings.Default.Save();
                train_sidelabel.Text = get_code;
                train_toplabel.Text = get_code;
                return;
            }
            else if (train_button == false)
            {
                traincode = Properties.Settings.Default.Barcode_Train1;
                if (traincode == get_code)
                {
                    guna2TileButton1.FillColor = Color.LimeGreen;
                    guna2TileButton1.Text = "OK";
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into dbo.OK_Code values (2,'Side Scanner',@get_code,GETDATE());", con);
                    cmd.Parameters.AddWithValue("@get_code", get_code);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    byte[] bytesToSend_led = new byte[7] { 0x05, 0xE8, 0x04, 0x00, 0x2F, 0xFE, 0xE0 }; //GREEN LED ON
                    serialPort1.Write(bytesToSend_led, 0, bytesToSend_led.Length);
                }
                else
                {
                    guna2TileButton1.FillColor = Color.Red;
                    guna2TileButton1.Text = "NG";

                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into dbo.NG_Code values (2,'Side Scanner',@get_code,GETDATE());", con);
                    cmd.Parameters.AddWithValue("@get_code", get_code);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    byte[] bytestoSend = new byte[7] { 0x05, 0xE6, 0x04, 0x00, 0x0C, 0xff, 0x05 };
                    serialPort1.Write(bytestoSend, 0, bytestoSend.Length);

                    byte[] bytesToSend_beep = new byte[7] { 0x05, 0xE7, 0x04, 0x00, 0x2A, 0xFE, 0xE6 }; //GREEN LED OFF
                    serialPort1.Write(bytesToSend_beep, 0, bytesToSend_beep.Length);

                }
            }
        }

        private void serialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string get_code = sp.ReadExisting();
            string traincode;
            label2.Text = get_code;
            if (get_code == "NR")
            {
                guna2Button5_Click(sender, e);
                          
            }
            else if (train_button == true && get_code != "NR")
            {
                Properties.Settings.Default.Barcode_Train1 = get_code;
                Properties.Settings.Default.Save();
                train_sidelabel.Text = get_code;
                train_toplabel.Text = get_code;
                return;
            }
            else if (train_button == false)
            {
                traincode = Properties.Settings.Default.Barcode_Train1;
                if (traincode == get_code)
                {
                    guna2TileButton2.FillColor = Color.LimeGreen;
                    guna2TileButton2.Text = "OK";
                    con2.Open();
                    SqlCommand cmd = new SqlCommand("insert into dbo.OK_Code values (1,'Top Scanner',@get_code,GETDATE());", con2);
                    cmd.Parameters.AddWithValue("@get_code", get_code);
                    cmd.ExecuteNonQuery();
                    con2.Close();
                    //MessageBox.Show(MAIN_FORM.Dir);
                    byte[] bytesToSend_led = new byte[7] { 0x05, 0xE8, 0x04, 0x00, 0x2F, 0xFE, 0xE0 }; //GREEN LED ON
                    serialPort2.Write(bytesToSend_led, 0, bytesToSend_led.Length);

                }
                else
                {
                    guna2TileButton2.FillColor = Color.Red;
                    guna2TileButton2.Text = "NG";
                    con2.Open();
                    SqlCommand cmd = new SqlCommand("insert into dbo.NG_Code values (1,'Top Scanner',@get_code,GETDATE());", con2);
                    cmd.Parameters.AddWithValue("@get_code", get_code);
                    cmd.ExecuteNonQuery();
                    con2.Close();

                    byte[] bytestoSend = new byte[7] { 0x05, 0xE6, 0x04, 0x00, 0x0C, 0xff, 0x05 };
                    serialPort2.Write(bytestoSend, 0, bytestoSend.Length);

                    byte[] bytesToSend_beep = new byte[7] { 0x05, 0xE7, 0x04, 0x00, 0x2A, 0xFE, 0xE6 }; //GREEN LED OFF
                    serialPort2.Write(bytesToSend_beep, 0, bytesToSend_beep.Length);


                }
            }
        }
        private void guna2TileButton2_Click(object sender, EventArgs e)
        {

        }

        private void DATA_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {

        }
        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void LINE1_Click(object sender, EventArgs e)
        {

        }

        private void serialPort2_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBox1.Text;
            serialPort1.Open();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("LINE1");
            guna2Button1.BackColor = Color.DodgerBlue;
            guna2Button4.BackColor = Color.RoyalBlue;
            guna2Button7.BackColor = Color.RoyalBlue;


        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("DATA");
            guna2Button4.BackColor = Color.DodgerBlue;
            guna2Button1.BackColor = Color.RoyalBlue;
            guna2Button7.BackColor = Color.RoyalBlue;

        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("SETTING");
            guna2Button7.BackColor = Color.DodgerBlue;
            guna2Button1.BackColor = Color.RoyalBlue;
            guna2Button4.BackColor = Color.RoyalBlue;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
      
        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);

        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            comboBox2.Items.AddRange(ports);
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            serialPort2.PortName = comboBox2.Text;
            serialPort2.Open();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
