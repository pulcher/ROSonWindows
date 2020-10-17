using LattePanda.Firmata;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMotors
{
    class Program
    {
        public const int SERVO1 = 5;
        public const int SERVO2 = 6;
        private static Arduino Arduino;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, Lattepanda! - again ");

            Arduino = new Arduino("COM7", 250000, true, 8000);

            Arduino.pinMode(5, Arduino.SERVO);
            Arduino.pinMode(6, Arduino.SERVO);

            await StopMotors();

            while(true)
            {
                await StopMotors();

                Console.WriteLine("Servo1: 180");
                Arduino.servoWrite(SERVO1, 180);
                await Task.Delay(1000);

                await StopMotors();

                Console.WriteLine("Servo1: 0");
                Arduino.servoWrite(SERVO1, 0);
                await Task.Delay(1000);

                await StopMotors();

                Console.WriteLine("Servo2: 180 - left turn");
                Arduino.servoWrite(SERVO2, 180);
                await Task.Delay(1000);

                await StopMotors();

                Console.WriteLine("Servo2: 0 - right turn");
                Arduino.servoWrite(SERVO2, 0);
                await Task.Delay(1000);

            }

        }

        private static async Task StopMotors()
        {
            Console.WriteLine("Stopping...");
            Arduino.servoWrite(SERVO1, 92);
            Arduino.servoWrite(SERVO2, 92);
            await Task.Delay(2000);
        }
    }
}
