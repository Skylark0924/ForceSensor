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
        public static double[] zero = new double[6];
        public char[] delimiterChars = { ' ', ',', '\t' };
        public string Fx, Fy, Fz, Mx, My, Mz;


        public static Matrix W1 = Matrix.Create(68, 6, new double[] { -0.36014,-0.87604,0.91185,0.77,1.589,-0.9952,
1.4276,0.45914,0.56556,-1.6494,-1.2747,0.99901,
0.103,1.5319,0.83492,-1.4185,-0.23379,1.7076,
0.32941,1.9454,-0.5227,-1.2006,1.2553,0.23252,
0.98974,0.50938,0.50072,-0.097571,-2.7643,0.0064371,
-0.61344,-0.51597,-1.5236,1.4715,1.4168,-0.80534,
-1.041,1.0685,-1.242,0.79001,0.93564,-1.331,
0.62924,-0.52392,2.4133,-0.98635,-0.31617,-1.929,
1.237,1.4939,-0.81242,-0.59923,-0.98198,-1.2855,
-0.37192,-2.3831,0.59401,1.4548,-0.22467,-0.37085,
-1.177,0.042335,1.4175,1.3013,0.07186,-1.5871,
-0.012685,0.6724,-0.69681,0.49946,1.7045,1.406,
-0.72628,-1.4673,-0.88697,-0.93064,1.4697,0.57037,
0.21706,-0.72403,-1.0384,-2.0435,1.9202,-1.2361,
0.74569,-0.38607,1.622,1.5344,0.16501,-1.3515,
-0.97016,-1.2768,-0.84891,0.7424,-0.66383,-1.9301,
-0.1531,-0.41361,-0.48761,1.9642,1.8549,-0.17103,
-0.89268,-0.26713,-2.4438,-1.4965,0.22395,-1.5535,
-0.98203,-1.437,-0.47221,0.095142,-0.97571,-1.771,
-1.7489,0.22526,-1.1055,-1.3736,-0.83718,1.6805,
-1.0551,-1.0128,-0.91391,1.4749,0.8899,-0.52342,
-1.3044,-1.2357,-0.27853,-1.8184,0.057978,1.5776,
-1.231,-2.6115,-2.8573,-0.84829,2.4111,-0.4913,
-0.51196,-1.4474,0.64126,-0.88714,2.0808,-0.35855,
-0.040817,0.4893,-1.4205,0.94823,1.4955,-1.3913,
-1.7847,1.6592,0.68663,0.85022,0.40687,-1.0506,
0.74386,0.94387,1.1073,-0.74676,2.1848,-1.5415,
0.90663,0.86839,-1.4269,-0.9078,-1.6808,0.5563,
-0.4131,-1.3175,-0.36149,-1.602,-1.6689,-1.2533,
1.3836,-1.0148,0.60214,0.96003,-1.0725,1.5451,
-1.5612,-0.92107,-1.4776,0.20435,-0.11068,0.88608,
-1.2164,-2.5091,0.2809,-1.485,-0.076714,1.4786,
-1.7607,-0.3328,0.71552,-1.2316,1.4377,0.22801,
-1.5431,0.21133,-1.6408,-0.13535,1.0574,-1.0746,
-2.6616,1.0885,1.3528,1.4704,-0.24349,-1.1321,
-1.061,0.74923,2.3173,0.55478,0.40887,0.21847,
-1.5036,-0.76396,1.615,-1.7095,0.77054,1.0155,
-1.2157,-0.0043001,0.049595,-1.1124,-1.734,-0.17963,
-1.3586,-0.39982,-0.79189,1.4083,1.6906,-0.17719,
-0.55211,1.3921,1.3795,0.28467,-1.215,0.79004,
0.27829,1.4828,-0.77837,-0.82726,-0.25109,2.1852,
1.1171,-0.05178,1.3389,1.1888,-0.40631,-1.0861,
2.3259,1.093,1.1371,-0.68522,-0.843,0.90439,
-2.0456,1.0548,1.2631,0.47346,1.1593,0.56307,
-0.090547,-1.9497,-1.1838,1.0824,-1.3429,-0.069884,
-0.83054,0.18088,-1.6386,1.5967,-0.84562,1.5596,
-0.077781,-0.52759,-1.5929,-0.74568,-1.8936,-0.42206,
-1.1735,-2.2025,0.79113,0.35015,-1.0858,-0.94642,
-1.8028,-0.71303,0.92217,1.597,-0.82499,0.52114,
0.011385,-1.4615,1.1177,-1.8218,1.8583,-0.28305,
-1.1328,1.2107,1.1912,0.51099,-0.069275,-1.0998,
-2.0949,-0.35281,-0.33425,-0.30111,-0.75875,1.2462,
1.3125,0.21392,0.4177,-0.64712,-1.0169,1.5979,
-0.44205,-0.91379,0.83292,-0.71975,1.7439,1.5706,
1.466,-1.6985,-0.34215,0.82501,-0.42726,-1.2586,
0.8147,0.25725,1.5117,1.4619,0.95264,-1.0054,
-1.6372,-1.1496,-1.2376,-0.82499,0.85584,-1.5116,
1.1436,2.1108,-1.1003,1.1912,-0.55838,0.029684,
1.0415,2.1244,-0.066715,-0.87599,1.5217,-1.3667,
0.56568,-0.31256,-1.5824,1.7366,0.87136,-0.44977,
0.040862,-1.7286,0.63631,1.0499,-0.28783,1.2915,
-1.5788,0.94091,0.45877,-0.57379,1.7665,0.073093,
0.84107,1.4055,0.16446,1.0854,1.757,-0.23319,
-1.0027,1.9436,1.2868,-0.0498,-0.28104,-1.1174,
-0.10017,-1.0932,1.1522,1.2947,-0.56885,-0.10121,
1.4654,-0.20133,1.0894,-0.75648,0.17318,1.4668,
1.0875,0.16362,-0.69852,-1.4382,1.4721,-1.3817,
-1.1431,1.9186,-0.69223,0.8286,1.8602,-0.24449});
        public static Matrix W2 = Matrix.Create(6, 68, new double[] { 0.48121,0.26039,-0.41193,0.55561,0.63924,0.82339,0.35754,-0.75114,-0.60462,0.32735,0.21748,0.30531,0.4454,-0.1006,0.78734,0.3345,0.77584,0.32206,0.42968,-0.66673,-0.8105,-0.95088,-0.09755,0.59377,0.33791,-0.039963,1.0046,-0.00891,-0.36015,0.86129,0.85486,1.0366,-0.27811,0.13648,0.72656,0.29222,0.0958,0.41566,0.93606,0.50261,1.0095,0.15942,0.44852,-1.2284,-0.43697,-0.53154,0.66427,0.16464,-0.37618,0.17197,0.43258,-0.34118,-0.014018,0.053011,-0.27152,0.73219,0.99908,0.2385,-0.028119,0.39837,-0.844,-0.14309,-0.79699,0.74761,-0.15287,-0.32859,-0.66179,0.21232,
-0.85123,0.85066,-0.091187,-0.7198,-0.80936,0.17712,-0.84179,-0.83628,-0.31711,0.3723,-0.83164,-0.21947,0.95996,0.16622,-0.80039,-0.072926,-0.41321,0.13011,-0.60677,-0.60473,0.72489,-0.63567,0.67531,-0.80653,-0.4049,-0.25616,0.42697,-0.12466,0.19877,-0.041672,-0.72595,-0.56166,0.35282,0.21882,0.15948,0.20014,-0.15964,0.5643,0.51351,0.52452,0.20303,-0.43419,-0.090884,0.12789,0.52482,0.35185,-0.95913,-0.38587,-0.67286,1.0882,-0.74164,0.033371,0.32022,-0.92305,0.74362,0.17854,-0.40905,-0.16988,0.024389,-0.39626,-0.35302,0.32441,-0.39802,-0.62053,-0.0085351,0.30703,-0.9461,-0.85053,
0.17175,0.37593,-0.58538,0.80674,0.1235,0.071231,-0.051064,-0.96608,-0.43033,0.74326,0.27543,0.06703,-0.061813,1.9409,0.35613,-0.53728,-0.24285,0.32204,0.16133,-1.6655,-0.25515,-0.58269,2.2759,0.9646,-0.55238,1.1794,-0.80335,1.0001,0.43348,0.15504,-0.28999,-0.082219,-0.098881,-0.34958,1.4234,1.2214,-1.0021,0.39549,-0.23216,-0.25993,0.25609,-0.56425,1.1231,-0.50406,-0.65712,-0.26809,-1.3926,-1.4373,0.20771,-0.030574,-0.016508,0.9182,-0.30432,0.071034,0.45768,0.076678,-0.072665,-1.0134,-1.0499,-0.030801,-0.86985,0.72382,0.36115,-0.16701,-0.0050445,-0.66909,0.40432,-1.049,
0.6873,0.94957,0.81801,0.090454,0.32173,0.64492,-0.78975,-0.34442,0.00033243,0.64811,0.16929,0.079756,0.040393,0.11011,-0.088693,0.19306,0.17766,-0.60816,0.083783,0.2862,0.03177,-0.37272,0.63987,0.3124,0.20909,-0.035317,-0.13195,0.86488,0.26464,-0.78954,0.063525,-1.0665,-0.79122,-0.22778,-0.16534,0.15605,0.86444,-0.57401,0.13824,0.41811,0.23584,-0.054143,-0.6733,0.28835,-0.14995,-0.012015,0.28419,0.24039,-0.76988,0.81961,-0.6544,0.29981,0.0020577,-0.069314,0.44205,-0.014623,-0.74731,-0.17256,0.3936,-0.69055,0.52609,0.32636,-0.34665,0.88957,0.21921,0.072574,-0.23791,-0.66668,
-0.24046,-0.83469,0.040095,0.39813,0.61943,-0.73184,-0.15657,-0.17618,0.47671,-0.27434,0.17255,-0.010546,-0.75848,-0.15397,-0.77697,0.38991,0.63281,-0.46616,-0.40749,-0.36527,0.9051,-0.41437,-0.26332,0.61202,-0.037686,0.78574,0.34915,-0.61661,-0.57968,0.083953,-0.1983,0.043781,0.39151,0.57057,-0.43881,-0.37926,0.075889,-0.066063,0.28151,0.057801,0.24756,0.49558,-0.017056,-0.51056,0.19655,0.26532,1.1239,0.089065,0.51418,0.97198,0.65089,-0.93915,0.10008,-0.20323,-0.82129,-0.40205,0.76684,-0.43373,0.89857,-0.53851,0.3278,-0.58029,-0.33366,-0.19773,-0.62252,0.92383,-0.61613,0.41354,
-0.71246,0.42378,0.36899,0.45812,0.62783,0.78704,0.23576,0.44336,-0.52241,-0.46067,-0.33152,0.65449,0.55636,-0.74256,0.19798,1.0181,0.1828,-0.12572,-0.44989,0.35622,-0.31861,0.91515,0.012585,0.014735,0.35783,-0.61725,0.3832,-0.022168,-0.13649,0.24382,0.60054,-0.75343,-0.36445,-0.35648,0.41868,0.22098,0.46479,0.074002,-0.35595,-0.79158,-1.1748,0.071157,-0.034371,0.37332,-0.11069,0.85189,0.089018,0.071403,-0.54938,-0.47368,-0.64587,0.72476,0.91192,0.37668,-0.48133,0.52515,0.31204,-0.30619,0.78583,-0.71965,0.70717,0.85939,-0.7461,0.21119,0.47618,-0.95543,0.64368,-0.79659
 });
        public static Matrix b1 = Matrix.Create(68, 1, new double[] {
2.9497,
-2.7792,
-2.6475,
-2.6881,
-2.2012,
2.4653,
2.2688,
-2.6823,
-2.2735,
2.0121,
2.0191,
2.2795,
2.2175,
1.142,
-1.5079,
1.6291,
1.5081,
1.1255,
1.1192,
1.6848,
1.4507,
1.0197,
1.1525,
0.72097,
0.60856,
0.46581,
0.50426,
-0.10497,
0.25951,
-0.5476,
0.39548,
-0.021715,
-0.079365,
-0.25799,
-0.19576,
-0.03069,
-0.53323,
-0.1812,
-0.17276,
-0.42278,
0.050146,
1.3417,
1.3413,
-0.49211,
-0.56876,
-0.83313,
0.78964,
-1.8284,
-0.94179,
-1.2807,
-1.9391,
-1.2868,
2.2159,
-1.6869,
1.6544,
1.7924,
-1.2913,
2.3269,
1.9973,
2.2909,
2.4568,
-2.5594,
2.8134,
-2.6392,
-2.4701,
3.0285,
2.705,
-2.524
});
        public static Matrix b2 = Matrix.Create(6, 1, new double[] { -0.40488,
-0.57538,
-0.89729,
0.55424,
0.62722,
0.56478
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

            mytimer = new System.Threading.Timer(new TimerCallback(Mytimer_done), this, 0, 50);
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
            if (str1[0] != "" && str1[1] != "" && str1.Length >= 8 && str1[0] != "\n" && str1[0] != "\r\n")
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

        //数据前处理
        Matrix Pretreatment(Matrix Input)
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
        }

        //将差分后的数据保存到txt文件中
        void Mytimer_save(object state)
        {
            TextBox[] tbs = { textBox17, textBox18, textBox19, textBox20, textBox21, textBox22 };
            this.button2.Enabled = false;
            if (num < 2000)
            {
                for (int i = 0; i < 6; i++)
                {
                    strmsave.Write(tbs[i].Text + ", ");

                }
                //strmsave.Write(Fx + ", " + Fy + ", " + Fz + ", " + Mx + ", " + My + ", " + Mz + ", ");
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
        void Mytimer_decouple(object state)
        {
            //this.BeginInvoke(new MyInvoke1(UpdateDecouple));
            double Fa, Fb, Fc, Ma, Mb, Mc;

            Fa = double.Parse(textBox17.Text) - zero[0];
            Fb = double.Parse(textBox18.Text) - zero[1];
            Fc = double.Parse(textBox19.Text) - zero[2];
            Ma = double.Parse(textBox20.Text) - zero[3];
            Mb = double.Parse(textBox21.Text) - zero[4];
            Mc = double.Parse(textBox22.Text) - zero[5];

            Matrix Input = Matrix.Create(6, 1, new double[] { Fa, Fb, Fc, Ma, Mb, Mc });
            Input = Pretreatment(Input);

            Matrix Output_hide = W1 * Input + b1;
            for (int i = 0; i < 68; i++)
            {
                Output_hide[i, 0] = 2 / (1 + Math.Exp(-2 * Output_hide[i, 0])) - 1;
            }

            Matrix Output = W2 * Output_hide + b2;

            Fx = ((Output[0, 0] - 1) / 2.0 * 1000).ToString();
            Fy = ((Output[1, 0] + 1) / 2.0 * 1000).ToString();
            Fz = ((Output[2, 0] - 1) / 2.0 * 1000).ToString();
            Mx = ((Output[3, 0] + 1) / 2.0 * 0.032 * 1000).ToString();
            My = ((Output[4, 0] + 1) / 2.0 * 0.090 * 1000).ToString();
            Mz = (Output[5, 0] * 0.090 * 1000).ToString();

            textBox28.Text = Fx;
            textBox27.Text = Fy;
            textBox26.Text = Fz;
            textBox23.Text = Mx;
            textBox24.Text = My;
            textBox25.Text = Mz;
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
            ////int n17 = ran17.Next(10000);
            ////int n18 = ran18.Next(10000);
            ////int n19 = ran19.Next(10000);
            ////int n20 = ran20.Next(10000);
            ////int n21 = ran21.Next(10000);
            ////int n22 = ran22.Next(10000);
            //int n17 = 1000;
            //int n18 = 1000;
            //int n19 = 10000;
            //int n20 = 100;
            //int n21 = 1000;
            //int n22 = 1000;
            //textBox17.Text = n17.ToString();
            //textBox18.Text = n18.ToString();
            //textBox19.Text = n19.ToString();
            //textBox20.Text = n20.ToString();
            //textBox21.Text = n21.ToString();
            //textBox22.Text = n22.ToString();
        }

        //创建数据保存定时器，调用mytimer_save，间隔为50ms
        private void UpdateSave()
        {
            strmsave = new StreamWriter("D:\\ForceSensor\\data\\test2.txt", true, System.Text.Encoding.Default);
            mytimer1 = new System.Threading.Timer(new TimerCallback(Mytimer_save), this, 0, 50);
            num = 0;
        }

        //更新解耦结果
        private void UpdateDecouple()
        {
            mytimer2 = new System.Threading.Timer(new TimerCallback(Mytimer_decouple), this, 0, 50);

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
            Thread th1 = new Thread(UpdateDecouple);
            th1.Start();
        }
    }
}