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
using LinearAlgebra;

namespace ForceSensor
{
    public partial class Form1 : Form
    {
        SerialPort serialPort1 = new SerialPort();
        SerialPort serialPort2 = new SerialPort();
        public delegate void MyInvoke1();
        public delegate void MyInvoke2();
        System.Threading.Timer mytimer;
        System.Threading.Timer mytimer1;
        public StreamWriter strmsave;
        public double[] zero;
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
            if (ports.Length == 0) MessageBox.Show("No ports are found!");
            //绑定串口列表到下拉列表,设置第一项为默认值   
            foreach (var com in ports)
            {
                comboPortName1.Items.Add(com);
                comboPortName2.Items.Add(com);
            }
            comboPortName1.SelectedIndex = 0;
            comboPortName2.SelectedIndex = 0;
        }
        int num = 0;
        static int num2 = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboPortName1.Items.Count == 0 || comboPortName2.Items.Count == 0)
            {
                MessageBox.Show("No ports are found!");
                return;
            }
            try
            {
                if (!serialPort1.IsOpen)
                {
                    serialPort1.BaudRate = 9600;//波特率
                    serialPort1.Parity = Parity.None;//无奇偶校验位
                    serialPort1.StopBits = StopBits.One;//一个停止位

                    serialPort2.BaudRate = 9600;//波特率
                    serialPort2.Parity = Parity.None;//无奇偶校验位
                    serialPort2.StopBits = StopBits.One;//一个停止位

                    serialPort1.PortName = comboPortName1.SelectedItem.ToString();
                    serialPort2.PortName = comboPortName2.SelectedItem.ToString();

                    serialPort1.Handshake = Handshake.RequestToSendXOnXOff;
                    serialPort1.Open();
                    serialPort2.Handshake = Handshake.RequestToSendXOnXOff;
                    serialPort2.Open();
                    mytimer = new System.Threading.Timer(new TimerCallback(mytimer_done), this, 0, 50);
                    button1.Text = "关闭串口";
                    this.comboPortName1.Enabled = false;
                    this.comboPortName2.Enabled = false;
                }
                else
                {                    
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

        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            TextBox[] tbs1 = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8 };       
            TextBox[] tbs2 = { textBox9, textBox10, textBox11, textBox12, textBox13, textBox14, textBox15, textBox16 };
 
                string buff1;
                buff1 = serialPort1.ReadLine();
                buff1 = buff1.Replace(", ", " ");
                string[] str1 = buff1.Split(delimiterChars);
                string buff2;
                buff2 = serialPort2.ReadLine();
                buff2 = buff2.Replace(", ", " ");
                string[] str2 = buff2.Split(delimiterChars);
                if (str1[0] != "" && str1[1] != "" && str1.Length >= 8 && str1[0] != "\n" && str1[0] != "\r\n")
                {
                    float first = float.Parse(str1[0]);
                    if (first > 2400000 ||  first < 1000)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            tbs1[i].Text = str1[i];
                        }

                    }
                }
                else
                {
                     
                }
                if (str2[0] != "" && str2[1] != "" && str2.Length >= 8 && str2[0] != "\n")
                {
                    float first = float.Parse(str2[0]);
                    if (first < 1000)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            tbs2[i].Text = str2[i];
                        }
                    }
                }
                textBox17.Text = (double.Parse(textBox2.Text) - double.Parse(textBox3.Text)).ToString();
                textBox18.Text = (double.Parse(textBox4.Text) - double.Parse(textBox5.Text)).ToString();
                textBox19.Text = (double.Parse(textBox6.Text) - double.Parse(textBox7.Text)).ToString();
                textBox20.Text = (double.Parse(textBox10.Text) - double.Parse(textBox11.Text)).ToString();
                textBox21.Text = (double.Parse(textBox12.Text) - double.Parse(textBox13.Text)).ToString();
                textBox22.Text = (double.Parse(textBox14.Text) - double.Parse(textBox15.Text)).ToString();
        }
        //void serialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    TextBox[] tbs = { textBox9, textBox10, textBox11, textBox12, textBox13, textBox14, textBox15, textBox16 };
        //    //byte[] SerialBuff = new byte[serialPort1.BytesToRead];//串口接收数据临时缓存

        //    string buff2;
        //    buff2 = serialPort2.ReadLine();
        //    buff2 = buff2.Replace(", ", " ");
        //    string[] str2 =  buff2.Split(delimiterChars);
        //    if (str2[0] != "" && str2[1] != "" && str2.Length >= 8)
        //    {
        //        float first = float.Parse(str2[0]);
        //        if (first < 1000)
        //        { 
        //            for (int i = 0; i < 8; i++)
        //            {
        //                tbs[i].Text = str2[i];
        //            }
        //        }
        //    }
        //    textBox17.Text = (double.Parse(textBox2.Text) - double.Parse(textBox3.Text)).ToString();
        //    textBox18.Text = (double.Parse(textBox4.Text) - double.Parse(textBox5.Text)).ToString();
        //    textBox19.Text = (double.Parse(textBox6.Text) - double.Parse(textBox7.Text)).ToString();
        //    textBox20.Text = (double.Parse(textBox10.Text) - double.Parse(textBox11.Text)).ToString();
        //    textBox21.Text = (double.Parse(textBox12.Text) - double.Parse(textBox13.Text)).ToString();
        //    textBox22.Text = (double.Parse(textBox14.Text) - double.Parse(textBox15.Text)).ToString();
        //}
        void mytimer_done(object state)
        {
            num++;
            this.BeginInvoke(new MyInvoke2(UpdateForm));
            //richTextBox1.Text = num.ToString();
            //if (serialPort1.IsOpen)
            //    serialPort1.DataReceived += serialPort_DataReceived;

        }
        void mytimer_save(object state)
        {

            //StreamWriter strmsave = new StreamWriter("D:\\Academic_information\\Robotics\\力传感器解算\\data.txt", true, System.Text.Encoding.Default);
            
            //this.BeginInvoke(new MyInvoke1(UpdateSave));
            TextBox[] tbs = { textBox17, textBox18, textBox19, textBox20, textBox21, textBox22 };
            this.button2.Enabled = false;
            if (num2 < 20)
            {
                for (int i = 0; i < 6; i++)
                {
                    strmsave.Write(tbs[i].Text + ", ");

                }
                strmsave.Write("\n");
                //strmsave.Close();
                num2++;
                //richTextBox1.Text = num2.ToString();
            }
            else
            {
                strmsave.Close();
                this.button2.Enabled = true;
            }
            
        }

        /// <summary>
        /// 代理到主界面线程执行更新
        /// </summary>
        private void UpdateForm()
        {
            serialPort1.DataReceived += serialPort_DataReceived;
        }

        private void UpdateSave()
        {
            strmsave = new StreamWriter("D:\\ForceSensor\\data\\data_z+向上_x+ .txt", true, System.Text.Encoding.Default);

            mytimer1 = new System.Threading.Timer(new TimerCallback(mytimer_save), this, 0, 50);

            num2 = 0;
            //TextBox[] tbs = { textBox17, textBox18, textBox19, textBox20, textBox21, textBox22 };
            //this.button2.Enabled = false;
            //if (num2 <= 100)
            //{
            //    for (int i = 0; i < 6; i++)
            //    {
            //        strmsave.Write(tbs[i].Text + ", ");

            //    }
            //    strmsave.Write("\n");
            //    //strmsave.Close();
            //    num2++;
            //    //richTextBox1.Text = num2.ToString();
            //}
            //else
            //{
            //    strmsave.Close();
            //    this.button2.Enabled = true;
            //}
            //this.button2.Enabled = false;
            //for (int num = 0; num < 1000; num++)
            //{
            //    for (int i = 0; i < 6; i++)
            //    {
            //        strmsave.Write(tbs[i].Text + ", ");
            //    }
            //    strmsave.Write("\n");
            //    Thread.Sleep(10);
            //}
            //this.button2.Enabled = true;
            //strmsave.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(UpdateSave);
            th.Start();

            //StreamWriter strmsave = new StreamWriter("D:\\Academic_information\\Robotics\\力传感器解算\\data.txt", true, System.Text.Encoding.Default);
            //this.BeginInvoke(new MyInvoke(UpdateSave));
            //TextBox[] tbs = { textBox17, textBox18, textBox19, textBox20, textBox21, textBox22 };
            //this.button2.Enabled = false;
            //for (int num = 0; num < 1000; num++)
            //{
            //    for (int i = 0; i < 6; i++)
            //    {
            //        strmsave.Write(tbs[i].Text + ", ");
            //    }
            //    strmsave.Write("\n");
            //    Thread.Sleep(15);
            //}
            //strmsave.Close();
            //this.button2.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //serialPort1.DataReceived += serialPort1_DataReceived;
            //serialPort2.DataReceived += serialPort2_DataReceived;
            //textBox17.Text = (double.Parse(textBox5.Text) - double.Parse(textBox13.Text)).ToString();
            //textBox18.Text = (double.Parse(textBox7.Text) - double.Parse(textBox14.Text)).ToString();
            //textBox19.Text = (double.Parse(textBox6.Text) - double.Parse(textBox15.Text)).ToString();
            //textBox20.Text = (double.Parse(textBox2.Text) - double.Parse(textBox10.Text)).ToString();
            //textBox21.Text = (double.Parse(textBox3.Text) - double.Parse(textBox11.Text)).ToString();
            //textBox22.Text = (double.Parse(textBox4.Text) - double.Parse(textBox12.Text)).ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double[] zero = { double.Parse(textBox17.Text) ,double.Parse(textBox18.Text),
                            double.Parse(textBox19.Text) ,double.Parse(textBox20.Text),
                            double.Parse(textBox21.Text) ,double.Parse(textBox22.Text)};
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double Fa, Fb, Fc, Ma, Mb, Mc;

            Fa = double.Parse(textBox17.Text) - zero[0];
            Fb = double.Parse(textBox18.Text) - zero[1];
            Fc = double.Parse(textBox19.Text) - zero[2];
            Ma = double.Parse(textBox20.Text) - zero[3];
            Mb = double.Parse(textBox21.Text) - zero[4];
            Mc = double.Parse(textBox22.Text) - zero[5];

            Matrix Input = Matrix.Create(6, 1, new double[] { Fa, Fb, Fc, Ma, Mb, Mc });            
            Matrix W1 = Matrix.Create(14, 6, new double[] { 0.63337, - 1.1272, - 1.5342, - 0.56733, 0.66849, 0.039983,
            1.3337, 0.44928 ,- 1.7017, 0.93795, - 0.17461 ,- 0.27137,
            1.1179 ,- 0.40411, - 1.3276, 0.3862, 2.3409, - 2.0363,
            -0.46299, - 0.9143, - 0.88511 ,1.761, 0.94753, 1.1403,
            -1.6507, 0.31255, 0.1082 ,- 0.01328 ,- 1.895 ,2.5343,
            -3.1842, - 0.11251, - 2.6758 ,- 1.2917, - 0.76891, 1.7323,
            -1.1563, 0.1402 ,- 0.65556 ,- 0.4824, 0.51707, 2.8077,
            -0.53393, - 0.75989, - 0.82918, 0.97974, 2.0019, - 0.31393,
            -3.0904 ,- 0.13556, - 1.2127, - 1.1083, - 0.53207, - 0.11072,
            0.27465, - 0.25401, - 0.44492, - 1.3815, 0.8516, - 0.92643,
            0.55502, 0.74228 ,- 0.57512 ,- 0.82994, - 0.97358, - 0.34665,
            -1.2818 ,- 0.34766, 0.023372, - 1.3822, 1.0846, 0.65515,
            0.091166 ,0.88056,1.2115 ,0.60513 ,1.0874, 0.13495,
            -0.74252 ,0.071643, 0.62802, 0.70967 ,- 0.63781, - 1.1794 }); 
            Matrix W2 = Matrix.Create(6, 14, new double[] { 0.34736, 1.3615, 1.5231, 0.7146, - 0.5188 ,- 2.4115, - 2.1215, 1.9167, 1.5521, - 0.5551 ,- 0.43438, - 0.85645, 0.59932, - 0.23796,
            0.57683 ,0.79039 ,1.1045, 1.2353 ,- 1.4183 ,- 0.48079 ,0.35877, - 0.0025082, - 0.67506, 0.58738, - 0.20025, 0.74078, - 0.70742, 0.31674,
            -0.5949, - 1.0485, - 1.0193, 0.4699, 1.2303, 0.54674, 0.58253, 0.77223 ,0.05946 ,- 1.1187, - 0.89498, - 1.3745, 1.6394, - 1.0293,
            0.86548, 1.2993 ,2.0001, - 0.22648, - 1.9726, 0.56777, - 0.20855 ,- 1.4382, - 1.4194 ,0.47746 ,0.69248 ,0.66263 ,0.34231, 1.0334,
            1.201, 1.1848, 1.2392 ,- 1.4946 ,- 0.84383, 0.25412, 0.25082, 0.16316, 0.090503, 1.2379, 0.53168, 0.59531 ,- 1.7451, 1.2869,
            -0.48278, 0.29337 ,- 0.056168, 0.07749, 0.12966 ,0.10207, 0.080698 ,1.4307, - 0.22549, - 0.38264, - 0.81533, - 0.16856, 0.72193 ,- 0.50005 }); 
            Matrix b1 = Matrix.Create(14, 1, new double[] { -2.1506,
            -1.9557,
            0.39472,
            0.31062,
            -0.38878,
            1.9378,
            -2.2876,
            -1.8975,
            0.39406,
            -1.7486,
            -1.8909,
            -1.2838,
            2.5348,
            -2.5117 }); 
            Matrix b2 = Matrix.Create(1, 2, new double[] { 0.80454,
            0.3391,
            0.91086,
            -1.3327,
            -0.29532,
            -1.244 });
            Matrix Output_hide = W1 * Input + b1;
            Matrix Output = W2 * Output_hide + b2;

            textBox23.Text = Output[0, 0].ToString();
            textBox24.Text = Output[1, 0].ToString();
            textBox25.Text = Output[2, 0].ToString();
            textBox26.Text = Output[3, 0].ToString();
            textBox27.Text = Output[4, 0].ToString();
            textBox28.Text = Output[5, 0].ToString();
        }
    }
}