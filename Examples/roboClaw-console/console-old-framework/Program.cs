using System;
using RoboclawClassLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace console_old_framework
{
    class Program
    {
        static double voltage = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var devices = SerialPort.GetDevices();
            var deviceId = devices.First().DeviceID;

            var roboClaw = new Roboclaw("COM3", 38400, 0x80);

            var test = roboClaw.Open();
            roboClaw.GetLogicVoltage(ref voltage);

            Console.WriteLine($"Logic voltage: {voltage}");


            //roboClaw.ST_M1Forward(100);
            //Thread.Sleep(5000);
            //roboClaw.ST_M1Backward(100);
            //Thread.Sleep(3000);
            //roboClaw.ST_M1Forward(0);

            //Thread.Sleep(1000);

            roboClaw.ST_MixedForward(127);
            Thread.Sleep(10000);
            roboClaw.ST_MixedBackward(127);
            Thread.Sleep(2000);
            roboClaw.ST_MixedBackward(0);
            

            roboClaw.Close();

            Console.WriteLine("found devices:");

            foreach (var device in devices)
            {
                Console.WriteLine($"{device.Description}");
            }

            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }
    }
}
