using System;

namespace roboClaw_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var devices = RoboclawClassLib.Roboclaw.GetDevices();

            Console.WriteLine("found devices:");

            foreach (var device in devices)
            {
                Console.WriteLine($"{device.Description}");
            }
        }
    }
}
