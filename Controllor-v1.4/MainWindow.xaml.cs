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
                brick.Connection.Open();

                Console.WriteLine("Start experiment");

                Console.WriteLine("Press M to quit");
              
                brick.Sensor1 = new ColorSensor(ColorMode.Color);

                string colorRead = brick.Sensor1.ReadAsString();
                
                                

                    //                do
                    //                {
                    //                    Console.WriteLine("S3: " + brick.Sensor4
                    //                        .ReadAsString());
                    //                   
                    //                } while (!true);

                    
                //brick.MotorA.ResetTacho();
                //brick.MotorD.ResetTacho();
                brick.MotorA.Brake();
                brick.MotorD.Brake();

                brick.MotorD.On(speed);
                brick.MotorA.On(speed);

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
                brick.MotorA.Brake();
                brick.MotorD.Brake();
            }
        }

        static void TurnLeft() {
            brick.MotorA.On(10, 495, true);
            brick.MotorD.On(-10, 495, true);
            if (brick.Sensor1.ReadAsString() != "White")
            {
                brick.MotorA.Brake();
                brick.MotorD.Brake();
            }
        }

        static void Go() 
        {
            brick.MotorA.On(10);
            brick.MotorD.On(10);
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