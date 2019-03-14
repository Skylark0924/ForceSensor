using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;

namespace ForceSensor
{
    public partial class Form1 : Form
    {
        
        private delegate void MyInvoke();
        public char[] delimiterChars = { ' ', ',', '\t' };
        public Form1()
        {
            InitializeComponent();
            
            Control.CheckForIllegalCrossThreadCalls = false;   //防止跨线程访问出错，好多地方会用到
            this.Load += Form1_Load;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //获取单片机与计算机连接的端口号
            string[] ports = SerialPort.GetPortNames();
            if (ports.Length == 0)  MessageBox.Show("No ports are found!");
            //绑定串口列表到下拉列表,设置第一项为默认值   
            foreach (var com in ports)
            {
                comboPortName1.Items.Add(com);
                comboPortName2.Items.Add(com);
            }
            comboPortName1.SelectedIndex = 0;
            comboPortName2.SelectedIndex = 0;

            //启动定时器,用来接受信息,没有使用多线程,更易于理解
        }

        /// <summary>
        /// 代理到主界面线程执行更新
        /// </summary>
        private void UpdateForm()
        {
            serialPort1.DataReceived += serialPort1_DataReceived;
            serialPort2.DataReceived += serialPort2_DataReceived;
            textBox17.Text = (double.Parse(textBox5.Text) - double.Parse(textBox13.Text)).ToString();
            textBox18.Text = (double.Parse(textBox7.Text) - double.Parse(textBox14.Text)).ToString();
            textBox19.Text = (double.Parse(textBox6.Text) - double.Parse(textBox15.Text)).ToString();
            textBox20.Text = (double.Parse(textBox2.Text) - double.Parse(textBox10.Text)).ToString();
            textBox21.Text = (double.Parse(textBox3.Text) - double.Parse(textBox11.Text)).ToString();
            textBox22.Text = (double.Parse(textBox4.Text) - double.Parse(textBox12.Text)).ToString();
        }

        int num = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            System.Threading.Timer mytimer = new System.Threading.Timer(new TimerCallback(mytimer_done), this, 0, 15);
            if (comboPortName1.Items.Count == 0 || comboPortName2.Items.Count == 0)
            {
                MessageBox.Show("No ports are found!");
                return;
            }
            try
            {
                if (!serialPort1.IsOpen)
                {
                    serialPort1.PortName = comboPortName1.SelectedItem.ToString();
                    serialPort2.PortName = comboPortName2.SelectedItem.ToString();

                    serialPort1.Handshake = Handshake.RequestToSendXOnXOff;
                    serialPort1.Open();
                    
                    serialPort2.Handshake = Handshake.RequestToSendXOnXOff;
                    serialPort2.Open();

                    button1.Text = "关闭串口";
                    this.comboPortName1.Enabled = false;
                    this.comboPortName2.Enabled = false;
                }
                else
                {
                    mytimer.Dispose();
                    serialPort1.Close();
                    serialPort2.Close();
                    button1.Text = "打开串口";
                    this.comboPortName1.Enabled = true;
                    this.comboPortName2.Enabled = true;
                }    
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }          
            //this.button1.Enabled = false;
        }

        void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
            TextBox[] tbs = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8 };
            //byte[] SerialBuff = new byte[serialPort1.BytesToRead];//串口接收数据临时缓存            
            string buff1;
            buff1 = serialPort1.ReadLine();
            buff1 = buff1.Replace(", ", " ");
            string[] str1 = buff1.Split(delimiterChars) ;
            if (str1[0] != ""&& str1[1]!="" && str1.Length >= 8)
            {
                float first = float.Parse(str1[0]);
                if (first > 2400000 || first < 1000)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        tbs[i].Text = str1[i];
                    }
                }
            }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
        }
        void serialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            TextBox[] tbs = { textBox9, textBox10, textBox11, textBox12, textBox13, textBox14, textBox15, textBox16 };
            //byte[] SerialBuff = new byte[serialPort1.BytesToRead];//串口接收数据临时缓存

            string buff2;
            buff2 = serialPort2.ReadLine();
            buff2 = buff2.Replace(", ", " ");
            string[] str2 =  buff2.Split(delimiterChars);
            if (str2[0] != "" && str2[1]!="" && str2.Length>=8)
            {
                float first = float.Parse(str2[0]);
                if (first < 70000)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        tbs[i].Text = str2[i];
                    }
                }
            }
        }

        void mytimer_done(object state)
        {
            this.BeginInvoke(new MyInvoke(UpdateForm));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //richTextBox1.Text = (++num).ToString();
            serialPort1.DataReceived += serialPort1_DataReceived;
            serialPort2.DataReceived += serialPort2_DataReceived;
            textBox17.Text = (double.Parse(textBox5.Text) - double.Parse(textBox13.Text)).ToString();
            textBox18.Text = (double.Parse(textBox7.Text) - double.Parse(textBox14.Text)).ToString();
            textBox19.Text = (double.Parse(textBox6.Text) - double.Parse(textBox15.Text)).ToString();
            textBox20.Text = (double.Parse(textBox2.Text) - double.Parse(textBox10.Text)).ToString();
            textBox21.Text = (double.Parse(textBox3.Text) - double.Parse(textBox11.Text)).ToString();
            textBox22.Text = (double.Parse(textBox4.Text) - double.Parse(textBox12.Text)).ToString();
            //Thread.Sleep(15);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TextBox[] tbs = { textBox17, textBox18, textBox19, textBox20, textBox21, textBox22 };
            if (!File.Exists("E:\\Academic_information\\Robotics\\力传感器解算\\data.txt"))
            {
                StreamWriter strmsave = new StreamWriter("D:\\Academic_information\\Robotics\\力传感器解算\\data.txt", true, System.Text.Encoding.Default);
                for (int i = 0; i < 6; i++)
                {
                    strmsave.Write(tbs[i].Text+", ");
                }
                strmsave.Write("\n");
                strmsave.Close();
            }
            //D:\\Academic_information\\Robotics\\力传感器解算\\data.txt存在这个文档的话直接打开写入。
            else
            {
                StreamWriter strmsave = new StreamWriter("D:\\Academic_information\\Robotics\\力传感器解算\\data.txt", true, System.Text.Encoding.Default);
                for (int i = 0; i < 6; i++)
                {
                    strmsave.Write(tbs[i].Text + ", ");
                }
                strmsave.Write("\n");
                strmsave.Close();
            }
        }
    }
}