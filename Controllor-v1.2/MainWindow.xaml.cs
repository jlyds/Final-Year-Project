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
        static void Main(string[] args)
        {
            try
            {
                var brick = new Brick<Sensor, Sensor, Sensor, Sensor>("COM3");


                sbyte speed = 40;
                //sbyte speed2 = 0;

                uint t = 1;


                brick.Connection.Open();

                Console.WriteLine("Start experiment");

                //Console.WriteLine("Please enter commands");

                //Console.WriteLine("Enter q to exit");

                //string cmd;

                brick.Sensor1 = new ColorSensor(ColorMode.Color);


                //                do
                //                {
                //                    Console.WriteLine("S3: " + brick.Sensor4
                //                        .ReadAsString());
                //                   
                //                } while (!true);

                //brick.MotorA.ResetTacho();
                //brick.MotorD.ResetTacho();
                brick.MotorD.On(speed, 360*t, true);
                brick.MotorA.On(speed, 360*t, true);


                brick.Connection.Close();

            }

                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                    Console.WriteLine("Press any key to end...");
                    Console.ReadKey();
                }
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