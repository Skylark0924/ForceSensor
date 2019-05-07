using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using LinearAlgebra;
using System.Diagnostics;
using System.Timers;

namespace ForceSensor
{
    public partial class Form1 : Form
    {
        public static bool sav_flag = false;
        public static bool dec_flag = false;
        public static bool enter_flag = false;
        SerialPort serialPort1 = new SerialPort();
        SerialPort serialPort2 = new SerialPort();
        public delegate void MyInvoke1();
        public delegate void MyInvoke2();
        public System.Threading.Timer mytimer;
        public System.Timers.Timer mytimer1 = new System.Timers.Timer();
        public Thread th;
        public StreamWriter strmsave;
        static int num = 0;
        static int numDec = 0;
        public static double[] zero = { 0, 0, 0, 0, 0, 0 };
        public char[] delimiterChars = { ' ', ',', '\t' };
        public static string Fx= "0", Fy = "0", Fz = "0", Mx = "0", My = "0", Mz = "0";
        public static double Fa, Fb, Fc, Ma, Mb, Mc;
        
        public static Matrix W1 = Matrix.Create(8, 6, new double[] { 6.4829,1.1148,-0.13988,23.07,-47.672,17.958,
0.5375,0.74908,1.2078,-5.1785,8.3943,-0.55088,
1.9802,0.27723,0.34958,-7.3335,8.9195,-1.4104,
-1.2901,1.0389,-5.6256,2.5598,-6.0037,-5.7818,
-0.6234,0.049202,0.00032841,-1.0126,-1.0834,1.0995,
0.9623,-0.10382,0.0098178,-1.2066,-4.1708,3.072,
1.1683,-0.068036,0.02108,0.533,-1.7462,0.80022,
-3.8056,5.3824,-9.3102,-16.162,9.4043,-1.9689});
        public static Matrix W2 = Matrix.Create(6, 8, new double[] { -0.63419,-0.10049,-2.2026,1.0969,-39.319,21.327,-31.042,-0.76115,
-0.19545,0.39484,3.1786,-0.22743,20.999,-12.783,19.599,0.017117,
-1.9775,0.58714,0.018123,-0.013965,44.338,-27.354,43.777,0.016039,
0.2312,-2.8439,3.7261,-0.43346,20.488,-12.926,18.837,0.43594,
0.3162,-0.76167,0.48851,0.25225,-21.558,12.562,-23.813,-0.1922,
0.57657,0.60697,0.66608,-0.78137,17.457,-12.867,15.388,0.4914 });
        public static Matrix b1 = Matrix.Create(8, 1, new double[] {
-5.1876,
-2.0022,
-0.10171,
3.2602,
-0.40239,
-0.46435,
0.38449,
11.927,
});
        public static Matrix b2 = Matrix.Create(6, 1, new double[] { 27.142,
-16.714,
-32.494,
-16.208,
17.04,
-11.492
 });

        public static Matrix X1min = Matrix.Create(6, 1, new double[] { -372.552853203721497266087681055, -76.4230888083136505883885547519, -180.001060311078390441252849996, -207.795864872748552443226799369, -241.622604556569740452687256038, -141.860710131439077485993038863 });
        public static Matrix X1max = Matrix.Create(6, 1, new double[] { 356.084770538921361549000721425, 181.553264518068544930429197848, 54.6139020180621059807890560478, 150.781893507945767396449809894, 271.451733334138452846673317254, 196.935402052381846260686870664 });
        public static int Y1min = -1;
        public static int Y1max = 1;

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
                    serialPort1.PortName = "COM22";
                    serialPort1.BaudRate = 9600;//波特率
                    serialPort1.Parity = Parity.None;//无奇偶校验位
                    serialPort1.StopBits = StopBits.One;//一个停止位

                    serialPort2.PortName = "COM23";
                    serialPort2.BaudRate = 9600;//波特率
                    serialPort2.Parity = Parity.None;//无奇偶校验位
                    serialPort2.StopBits = StopBits.One;//一个停止位

                    //serialPort1.PortName = comboPortName1.SelectedItem.ToString();
                    //serialPort2.PortName = comboPortName2.SelectedItem.ToString();

                    serialPort1.Handshake = Handshake.RequestToSendXOnXOff;
                    serialPort1.Open();
                    serialPort2.Handshake = Handshake.RequestToSendXOnXOff;
                    serialPort2.Open();

                    //数据接收，Threading.Timer
                    mytimer = new System.Threading.Timer(new TimerCallback(Mytimer_done), null, 0, 50);
                    //解耦加保存，Timers.Timer
                    mytimer1.Interval = 50;
                    mytimer1.Elapsed += new ElapsedEventHandler(Mytimer_decouple);

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
        void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
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
            //if (str1[0] != "" && str1[1] != "" && str1.Length >= 8 && str1[0] != "\n" && str1[0] != "\r\n")
            if (enter_flag)
            {
                float first = float.Parse(str1[0]);
                if (first > 2400000 || first < 1000)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        tbs1[i].Text = str1[i];
                    }

                }
            }
            //if (str2[0] != "" && str2[1] != "" && str2.Length >= 8 && str2[0] != "\n")
            if (enter_flag)
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
            enter_flag = true;
            textBox17.Text = (double.Parse(textBox2.Text) - double.Parse(textBox3.Text)).ToString();
            textBox18.Text = (double.Parse(textBox4.Text) - double.Parse(textBox5.Text)).ToString();
            textBox19.Text = (double.Parse(textBox6.Text) - double.Parse(textBox7.Text)).ToString();
            textBox20.Text = (double.Parse(textBox10.Text) - double.Parse(textBox11.Text)).ToString();
            textBox21.Text = (double.Parse(textBox12.Text) - double.Parse(textBox13.Text)).ToString();
            textBox22.Text = (double.Parse(textBox14.Text) - double.Parse(textBox15.Text)).ToString();

            Fa = double.Parse(textBox17.Text) - zero[0];
            Fb = double.Parse(textBox18.Text) - zero[1];
            Fc = double.Parse(textBox19.Text) - zero[2];
            Ma = double.Parse(textBox20.Text) - zero[3];
            Mb = double.Parse(textBox21.Text) - zero[4];
            Mc = double.Parse(textBox22.Text) - zero[5];
        }

        //数据前处理
        static Matrix Pretreatment(Matrix Input)
        {
            Matrix Input_temp = new double[6,1];
            for (int i=0;i<6;i++)
            {
                Input_temp[i, 0] = (Input[i, 0] - X1min[i, 0]) * (Y1max - Y1min)
                                / (X1max[i, 0] - X1min[i, 0]) -1;
            }
            return Input_temp;
        }

        //以代理的方式调用UpdateForm
        void Mytimer_done(object state)
        {
            this.BeginInvoke(new MyInvoke2(UpdateForm));
            if (dec_flag)
                mytimer1.Enabled = true;
            else
                mytimer1.Enabled = false;
        }

        //将差分后的数据保存到txt文件中
        void Mytimer_save()
        {
            //TextBox[] tbs = { textBox28, textBox27, textBox26, textBox23, textBox24, textBox25 };
            //TextBox[] tbs = { textBox17, textBox18, textBox19, textBox20, textBox21, textBox22 };
            this.button2.Enabled = false;
            if (num < 20)
            {
                //for (int i = 0; i < 6; i++)
                //{
                //    if (!string.IsNullOrEmpty(tbs[i].Text))
                //        strmsave.Write(tbs[i].Text + ", ");
                //}
                strmsave.Write(Fx + ", " + Fy + ", " + Fz + ", " + Mx + ", " + My + ", " + Mz);
                strmsave.Write("\n");
                num++;
            }
            else
            {
                strmsave.Close();
                this.button2.Enabled = true;
                sav_flag = false;
                num = 0;
            }
        }

        //以代理的方式调用UpdateDecouple
        void Mytimer_decouple(object source, ElapsedEventArgs e)
        {
            
            this.BeginInvoke(new MyInvoke1(UpdateDecouple));

            //Matrix Input = Matrix.Create(6, 1, new double[] { Fa, Fb, Fc, Ma, Mb, Mc });
            //Input = Pretreatment(Input);

            //Matrix Output_hide = W1 * Input + b1;
            //for (int i = 0; i < 68; i++)
            //{
            //    Output_hide[i, 0] = 2 / (1 + Math.Exp(-2 * Output_hide[i, 0])) - 1;
            //}

            //Matrix Output = W2 * Output_hide + b2;

            //Fx = ((Output[0, 0] - 1) / 2.0 * 1000).ToString();
            //Fy = ((Output[1, 0] + 1) / 2.0 * 1000).ToString();
            //Fz = ((Output[2, 0] - 1) / 2.0 * 1000).ToString();
            //Mx = ((Output[3, 0] + 1) / 2.0 * 0.032 * 1000).ToString();
            //My = ((Output[4, 0] + 1) / 2.0 * 0.090 * 1000).ToString();
            //Mz = (Output[5, 0] * 0.090 * 1000).ToString();

            //textBox28.Text = Fx;
            //textBox27.Text = Fy;
            //textBox26.Text = Fz;
            //textBox23.Text = Mx;
            //textBox24.Text = My;
            //textBox25.Text = Mz;
        }

        //更新串口接收
        private void UpdateForm()
        {
            serialPort1.DataReceived += SerialPort_DataReceived;
            //Random ran17 = new Random();
            //Random ran18 = new Random();
            //Random ran19 = new Random();
            //Random ran20 = new Random();
            //Random ran21 = new Random();
            //Random ran22 = new Random();
            //int n17 = ran17.Next(10000);
            //int n18 = ran18.Next(10000);
            //int n19 = ran19.Next(10000);
            //int n20 = ran20.Next(10000);
            //int n21 = ran21.Next(10000);
            //int n22 = ran22.Next(1000);
            ////int n17 = 1000;
            ////int n18 = 1000;
            ////int n19 = 10000;
            ////int n20 = 100;
            ////int n21 = 1000;
            ////int n22 = 1000;
            //textBox17.Text = n17.ToString();
            //textBox18.Text = n18.ToString();
            //textBox19.Text = n19.ToString();
            //textBox20.Text = n20.ToString();
            //textBox21.Text = n21.ToString();
            //textBox22.Text = n22.ToString();
        }

        //更新解耦结果
        private void UpdateDecouple()
        {
            textBox29.Text = numDec.ToString();
            numDec++;
            Matrix Input = Matrix.Create(6, 1, new double[] { Fa, Fb, Fc, Ma, Mb, Mc });
            Input = Pretreatment(Input);

            Matrix Output_hide = W1 * Input + b1;
            for (int i = 0; i < 8; i++)
            {
                Output_hide[i, 0] = 1 / (1 + Math.Exp(-1 * Output_hide[i, 0])) ;
            }

            Matrix Output = W2 * Output_hide + b2;

            textBox28.Text= ((Output[0, 0] - 1) / 2.0 * 1000).ToString();
            textBox27.Text = ((Output[1, 0] + 1) / 2.0 * 1000).ToString();
            textBox26.Text = ((Output[2, 0] - 1) / 2.0 * 1000).ToString();
            textBox23.Text = ((Output[3, 0] + 1) / 2.0 * 0.032 * 1000).ToString();
            textBox24.Text = ((Output[4, 0] + 1) / 2.0 * 0.090 * 1000).ToString();
            textBox25.Text = (Output[5, 0] * 0.090 * 1000).ToString();

            Fx = textBox28.Text;
            Fy = textBox27.Text;
            Fz = textBox26.Text;
            Mx = textBox23.Text;
            My = textBox24.Text;
            Mz = textBox25.Text;

            //保存
            if (sav_flag)
            {
                Mytimer_save();
            }
        }

        //数据保存按钮触发，创建UpdateSave线程
        private void button2_Click(object sender, EventArgs e)
        {
            sav_flag= true;
            strmsave = new StreamWriter("E:\\Github\\ForceSensor\\data\\test2.txt", true, System.Text.Encoding.Default);
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
            if (mytimer1.Enabled == false)
                dec_flag = true;
            else
                dec_flag = false;
        }
    }
}