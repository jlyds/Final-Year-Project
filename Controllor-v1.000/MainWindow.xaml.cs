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
namespace Application
{
    public static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var brick = new Brick<Sensor, Sensor, Sensor, Sensor>("COM3");
                //var brick = new Brick<Sensor, Sensor, Sensor, Sensor>("usb");                

                sbyte speed = 0;
                sbyte speed2 = 0;

                brick.Connection.Open();

                Console.WriteLine("Please enter commands");

                Console.WriteLine("Enter q to exit");

                string cmd;

                do
                { 
                    cmd = Console.ReadLine();
                    if (cmd == "up" || cmd =="u" )
                    {
                        if (speed < 80 && speed > -80)
                            speed = (sbyte)(speed + 10);
                        Console.WriteLine("Motor A&D speed set to " + speed);
                        brick.MotorA.On(speed);
                        brick.MotorD.On(speed);
                    }

                    else if (cmd == "down" || cmd == "d" )
                    {
                        if (speed < 80 && speed > -80)
                            speed = (sbyte)(speed - 10);
                        Console.WriteLine("Motor A&D speed set to " + speed);
                        brick.MotorA.On(speed);
                        brick.MotorD.On(speed);

                    }

                    else if (cmd == "left" || cmd == "l" )
                    {
                        speed2 = brick.MotorD.GetSpeed();

                        if (speed < 100 && speed > 0)
                        {
                            speed = (sbyte)(speed - 10);
                            Console.WriteLine("Motor A set to " + speed);
                            brick.MotorA.On(speed);
                        }
                        else if (speed < 0 && speed > -100)
                        {
                            speed = (sbyte)(speed + 10);
                            Console.WriteLine("Motor A set to " + speed);
                            brick.MotorA.On(speed);
                        }
                        else if (speed2 == 0)
                        {
                            brick.MotorA.On(-50);
                            brick.MotorD.On(50);
                            System.Threading.Thread.Sleep(333);
                            brick.MotorA.Brake();
                            brick.MotorD.Brake();
                        }
                    }

                    else if (cmd == "right" || cmd == "r" )
                    {
                        speed2 = brick.MotorA.GetSpeed();
                        
                        if (speed < 100 && speed > 0)
                        {
                            speed = (sbyte)(speed - 10);
                            Console.WriteLine("Motor D set to " + speed);
                            brick.MotorD.On(speed);
                        }
                        else if (speed < 0 && speed > -100)
                        {
                            speed = (sbyte)(speed + 10);
                            Console.WriteLine("Motor D set to " + speed);
                            brick.MotorD.On(speed);
                        }
                        else if (speed2 == 0)
                        {
                            brick.MotorD.On(-50);
                            brick.MotorA.On(50);
                            System.Threading.Thread.Sleep(333);
                            brick.MotorD.Brake();
                            brick.MotorA.Brake();
                        }

                    }


                    else if (cmd == "turnAround" || cmd == "a" )
                    {
                        brick.MotorA.On(-50);
                        brick.MotorD.On(50);
                        System.Threading.Thread.Sleep(1950);
                        brick.MotorD.Brake();
                        brick.MotorA.Brake();
                    }

                    else if (cmd == "stop" || cmd == "s" )
                    {
                        brick.MotorA.Off();
                        brick.MotorD.Off();
                        speed = 0;
                    }

                    else if (cmd == "brake" || cmd == "break" || cmd == "b" )
                    {
                        brick.MotorA.Brake();
                        brick.MotorD.Brake();                                                 
                        speed = 0;
                    }

                    else if (cmd == "reverse" || cmd == "v" ) 
                    {
                        brick.MotorA.Brake();
                        brick.MotorD.Brake();
                        System.Threading.Thread.Sleep(100);
                        brick.MotorA.On((sbyte)(-speed));
                        brick.MotorD.On((sbyte)(-speed));
                    }

                    else if (cmd == "turnLeft180" ) 
                    {
                        Console.WriteLine("turn left with brake");
                        brick.MotorD.On(50);
                        brick.MotorA.On(-50);
                        System.Threading.Thread.Sleep(1950);
                        brick.MotorD.Brake();
                        brick.MotorA.Brake();
                    }

                    else if ( cmd == "turnRight180" )
                    {
                        Console.WriteLine("turn right without brake");
                        brick.MotorD.On(-50);
                        brick.MotorA.On(50);
                        System.Threading.Thread.Sleep(1950);
                        brick.MotorD.Brake();
                        brick.MotorA.Brake();
                    }

                    Console.WriteLine(speed);


                } while (cmd != "q");

                ConsoleKeyInfo cki;
                Console.WriteLine("Press M to quit");
                do
                {
                    cki = Console.ReadKey(true); //press a key  
                    switch (cki.Key)
                    {
                        case ConsoleKey.Q:
                            Console.WriteLine("turn left with brake");
                            brick.MotorD.On(50);
                            brick.MotorA.On(-50);
                            System.Threading.Thread.Sleep(333);
                            brick.MotorD.Brake();
                            brick.MotorA.Brake();
                            break;

                        case ConsoleKey.E:
                            Console.WriteLine("turn right with brake");
                            brick.MotorD.On(-50);
                            brick.MotorA.On(50);
                            System.Threading.Thread.Sleep(333);
                            brick.MotorD.Brake();
                            brick.MotorA.Brake();
                            break;

                        case ConsoleKey.W:
                            Console.WriteLine("brake");
                            brick.MotorD.Brake();
                            brick.MotorA.Brake();
                            break;

                        case ConsoleKey.A:
                            Console.WriteLine("turn left without brake");
                            brick.MotorD.On(50);
                            brick.MotorA.On(-50);
                            System.Threading.Thread.Sleep(333);
                            brick.MotorD.Off();
                            brick.MotorA.Off();
                            break;

                        case ConsoleKey.D:
                            Console.WriteLine("turn right without brake");
                            brick.MotorD.On(-50);
                            brick.MotorA.On(50);
                            System.Threading.Thread.Sleep(100);
                            brick.MotorD.Off();
                            brick.MotorA.Off();
                            break;

                        case ConsoleKey.S:
                            Console.WriteLine("motor off");
                            brick.MotorD.Off();
                            brick.MotorA.Off();
                            break;
                    }
                } while (cki.Key != ConsoleKey.M);

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