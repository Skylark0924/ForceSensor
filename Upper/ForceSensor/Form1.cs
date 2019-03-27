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
        System.Threading.Timer mytimer2;
        public StreamWriter strmsave;
        static int num = 0;
        public static double[] zero =new double[6];
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

        //串口接收按钮触发，创建串口接收定时器，调用mytimer_done，间隔为50ms
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

        //串口接收
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

        //以代理的方式调用UpdateForm
        void mytimer_done(object state)
        {
            this.BeginInvoke(new MyInvoke2(UpdateForm));
        }

        //将差分后的数据保存到txt文件中
        void mytimer_save(object state)
        {
            TextBox[] tbs = { textBox17, textBox18, textBox19, textBox20, textBox21, textBox22 };
            this.button2.Enabled = false;
            if (num < 20)
            {
                for (int i = 0; i < 6; i++)
                {
                    strmsave.Write(tbs[i].Text + ", ");

                }
                strmsave.Write("\n");
            }
            else
            {
                strmsave.Close();
                this.button2.Enabled = true;
            }
            num++;
        }

        //以代理的方式调用UpdateDecouple
        void mytimer_decouple(object state)
        {
            this.BeginInvoke(new MyInvoke1(UpdateDecouple));
        }

        //更新串口接收
        private void UpdateForm()
        {
            serialPort1.DataReceived += serialPort_DataReceived;
        }

        //创建数据保存定时器，调用mytimer_save，间隔为50ms
        private void UpdateSave()
        {
            strmsave = new StreamWriter("D:\\ForceSensor\\data\\data_z+向上_x+ .txt", true, System.Text.Encoding.Default);
            mytimer1 = new System.Threading.Timer(new TimerCallback(mytimer_save), this, 0, 50);
            num = 0;
        }

        //更新解耦结果
        private void UpdateDecouple()
        {
            double Fa, Fb, Fc, Ma, Mb, Mc;

            Fa = double.Parse(textBox17.Text) - zero[0];
            Fb = double.Parse(textBox18.Text) - zero[1];
            Fc = double.Parse(textBox19.Text) - zero[2];
            Ma = double.Parse(textBox20.Text) - zero[3];
            Mb = double.Parse(textBox21.Text) - zero[4];
            Mc = double.Parse(textBox22.Text) - zero[5];

            Matrix Input = Matrix.Create(6, 1, new double[] { Fa, Fb, Fc, Ma, Mb, Mc });
            Matrix W1 = Matrix.Create(14, 6, new double[] { -0.15632,2.374,-1.4819,-0.94383,-0.62193,3.0122,
1.131,-1.1987,-1.7571,0.52364,1.1223,0.34813,
-3.9728,0.75634,2.0663,0.90675,-0.20605,2.4475,
-0.060906,1.1495,0.80966,-0.50873,-1.2962,-0.011752,
1.9469,1.0296,-1.6758,1.0986,0.80094,-1.633,
-2.8672,0.7764,1.3579,0.44587,-1.9883,2.0682,
-5.3289,-2.1081,-0.47989,-0.9464,0.81415,-0.74016,
0.31131,-0.12027,1.1533,2.4475,0.81835,-2.4902,
-3.3303,0.32404,-1.8687,1.3661,2.0824,0.80144,
1.0275,0.31557,-1.7698,-2.3951,-2.0874,-3.4407,
-0.016132,-0.45925,-2.1377,-0.76092,3.5301,1.3112,
1.3668,-1.3992,-1.3709,-2.9168,-0.51436,1.0034,
-0.4291,-1.8261,1.2127,-0.85885,-1.0317,1.2611,
-1.0672,0.83285,1.8992,0.10787,-1.6683,1.017});
            Matrix W2 = Matrix.Create(6, 14, new double[] { -2.197,-0.29459,0.021208,-0.054804,1.4025,-0.40052,-1.2694,2.3204,1.5856,0.49778,-0.36891,0.23737,-1.5312,-0.63889,
-0.44786,1.591,0.042424,0.6819,1.6805,-1.696,-0.57972,-0.69643,-0.55358,-0.58947,2.2779,-0.18774,-0.10861,-2.0872,
3.2745,1.7639,-3.05,-1.485,-0.68628,1.5906,1.3385,-1.368,-3.2814,-2.715,2.6074,0.79674,2.029,0.32095,
-0.31489,0.9714,-0.14411,0.3451,0.68909,-1.2071,-0.21849,-0.12121,-0.72586,1.7216,1.3248,-0.7916,-0.18491,-2.0784,
-0.82897,-0.16131,1.2917,0.46615,-0.16346,-0.74435,1.0578,0.27791,0.70185,1.9673,0.92311,-0.45621,-1.6414,-0.91682,
0.26582,1.2278,-0.20272,-2.8198,-0.30292,-0.22389,0.11291,-0.32368,-0.020349,-0.077105,-0.36408,-1.6775,-0.014188,1.0452
 });
            Matrix b1 = Matrix.Create(14, 1, new double[] { 2.3652,
-2.6665,
0.038258,
-1.4035,
1.9082,
-0.85702,
-0.39308,
1.7767,
0.12681,
1.4947,
2.1385,
1.9042,
1.996,
2.5599});
            Matrix b2 = Matrix.Create(6, 1, new double[] { 1.5398,
-0.51488,
0.46699,
-1.2084,
-1.4908,
-0.17314 });
            Matrix Output_hide = W1 * Input + b1;
            for (int i = 0; i < 14; i++)
            {
                Output_hide[i, 0] = 2 / (1 + Math.Exp(-2 * Output_hide[i, 0])) - 1;
            }

            Matrix Output = W2 * Output_hide + b2;

            textBox23.Text = ((Output[0, 0] - 1) / 2.0).ToString();
            textBox24.Text = ((Output[1, 0] + 1) / 2.0).ToString();
            textBox25.Text = ((Output[2, 0] - 1) / 2.0).ToString();
            textBox26.Text = ((Output[3, 0] + 1) / 2.0 * 0.032).ToString();
            textBox27.Text = ((Output[4, 0] + 1) / 2.0 * 0.090).ToString();
            textBox28.Text = (Output[5, 0] * 0.090).ToString();
        }

        //数据保存按钮触发，创建UpdateSave线程
        private void button2_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(UpdateSave);
            th.Start();
        }

        //设置零点按钮触发
        private void button3_Click(object sender, EventArgs e)
        {
            zero[0] = double.Parse(textBox17.Text);
            zero[1] = double.Parse(textBox18.Text);
            zero[2] = double.Parse(textBox19.Text);
            zero[3] = double.Parse(textBox20.Text);
            zero[4] = double.Parse(textBox21.Text);
            zero[5] = double.Parse(textBox22.Text);
        }

        //解耦按钮触发，创建解耦定时器，调用mytimer_decouple，间隔为50ms
        private void button4_Click(object sender, EventArgs e)
        {
            mytimer2 = new System.Threading.Timer(new TimerCallback(mytimer_decouple), this, 0, 50);
        }
    }
}