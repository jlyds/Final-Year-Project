using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MonoBrick.EV3;
using System.Threading;


namespace Application
{
    public static class Program
    {
        static Brick<Sensor, Sensor, Sensor, Sensor> brick;
 
        static void Main(string[] args)
        {          
            brick = new Brick<Sensor, Sensor, Sensor, Sensor>("COM4"); 
            
            sbyte speed = 10;

            sbyte sp = -10;
                        
            //uint degree = 360;
            
            //sbyte speed2 = 0;
                        
            //uint t = 1;

            
            
            try
            {
                brick.Connection.Close();
                brick.Connection.Open();

                Console.WriteLine("Start experiment");

                Console.WriteLine("Press q to quit");
              
                brick.Sensor1 = new ColorSensor(ColorMode.Color);

                string colorRead = brick.Sensor1.ReadAsString();

                string d;

                d = Console.ReadLine();

                do
                {
                    Break();

                    if (d == "303")
                    {
                        string cmd = Console.ReadLine();
                        Go();
                        if (cmd == "303")
                        {
                            Break();
                            Console.WriteLine("Reach destination Room 303.");
                            brick.Connection.Close();
                        }
                    }

                    else if (d == "306")
                    {
                        string cmd = Console.ReadLine();
                        Go();
                        if (cmd == "303")
                        {    Go();

                            if (cmd == "A")
                            {
                                Go();

                                if (cmd == "306")
                                {
                                    Break();
                                    Console.WriteLine("Reach destination Room 306.");
                                    brick.Connection.Close();
                                }
                            }
                        }
                    }

                    else if (d == "313")
                    {
                        string cmd = Console.ReadLine(); 
                        TurnRight();
                        Go();
                        if (cmd == "B")
                        {
                            TurnLeft();
                            Go();
                            if (cmd == "318")
                            {
                                Go();
                                if (cmd == "C")
                                {
                                    Go();
                                    if (cmd == "313")
                                    { 
                                        Break();                                    
                                        Console.WriteLine("Reach destination Room 313.");                                    
                                        brick.Connection.Close();
                                   }                               
                                                                       
                                
                                }
                            }
                        }

                    }

                    else if (d == "305")
                    {
                        string cmd = Console.ReadLine();
                        Go();
                        if (cmd == "303")
                        {
                            Go();
                            if(cmd == "A")
                            { 
                                TurnRight();
                                Go();
                                if(cmd == "305")
                                {
                                    Break();
                                    Console.WriteLine("Reach destination Room 305.");
                                    brick.Connection.Close();
                                }
                            }
                        }
                    }

                    else if (d == "318")
                    {
                        string cmd = Console.ReadLine();
                        Go();
                    }

                } while (d != "q");
                                

                    //                do
                    //                {
                    //                    Console.WriteLine("S3: " + brick.Sensor4
                    //                        .ReadAsString());
                    //                   
                    //                } while (!true);

                    
                //brick.MotorA.ResetTacho();
                //brick.MotorD.ResetTacho();         
               

                if (colorRead == "White")
                {
                    TurnRight();
                }
                
                //brick.MotorA.Off();
                //brick.MotorD.Off();
                //Console.WriteLine("Position A: " + brick.MotorA.GetTachoCount());                   
                //Console.WriteLine("Position D: " + brick.MotorD.GetTachoCount());              
                    
                do {

                    Console.WriteLine("color: " + brick.Sensor1.ReadAsString());
                    Thread.Sleep(500);                   
     
                } 

                while (true);

                string cki = Console.ReadKey().ToString();

                if (cki == "m") { brick.Connection.Close(); }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                Console.WriteLine("Press any key to end...");
                Console.ReadKey();
            }
            finally
            {
                brick.Connection.Close();
            }  
        }

        static void WaitForMotorToStop()
        {
            Thread.Sleep(500);
            while (brick.MotorA.IsRunning()) { Thread.Sleep(50); }
        }

        static void TurnRight() {
            brick.MotorD.On(10, 495, true);
            brick.MotorA.On(-10, 495, true);
            if (brick.Sensor1.ReadAsString() != "White")
            {
                Break();
            }
        }

        static void TurnLeft() {
            brick.MotorA.On(10, 495, true);
            brick.MotorD.On(-10, 495, true);
            if (brick.Sensor1.ReadAsString() != "White")
            {
                Break();
            }
        }

        static void Go() 
        {
            brick.MotorA.On(10);
            brick.MotorD.On(10);
        }

        static void Stop()
        {
            brick.MotorA.Off();
            brick.MotorD.Off();
        }

        static void Break()
        {
            brick.MotorA.Brake();
            brick.MotorD.Brake();
        }

        static void TurnAround()
        {
            brick.MotorA.On(10, 990, true);
            brick.MotorD.On(-10, 990, true);
        }

    }
}


//namespace WpfApplication1
//{



//    /// <summary>
//    /// MainWindow.xaml 的交互逻辑
//    /// </summary>
//    public partial class MainWindow : Window
//    {
//        public MainWindow()
//        {
//            InitializeComponent();
//        }

//        private void Button_Click(object sender, RoutedEventArgs e)
//        {

//        }

//        public byte[] sendRawData(byte[] rawData)
//        {
//            const int byteSize = 256;
//            Byte[] sendLength = { 0x00, 0x00 };
//            sendLength[0] = (byte)(rawData.Length % byteSize);
//            sendLength[1] = (byte)(rawData.Length / byteSize);
//            nxtConnection.Write(sendLength, 0, sendLength.Length);
//            nxtConnection.Write(rawData, 0, rawData.Length);
//            int responseLength = nxtConnection.ReadByte() + byteSize * nxtConnection.ReadByte();
//            byte[] response = new byte[responseLength];
//            for (int i = 0; i < responseLength; i++)
//                response[i] = (byte)nxtConnection.ReadByte();
//            return response;
//        }
//    }
//}