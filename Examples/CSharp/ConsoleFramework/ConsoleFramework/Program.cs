using LattePanda.Firmata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Lattepanda! - again ");

            Arduino arduino = new Arduino("COM7", 250000, true, 8000);
            arduino.pinMode(13, Arduino.OUTPUT);//Set the digital pin 13 as output
            while (true)
            {
                Console.Write("on...");
                // ==== set the led on or off
                arduino.digitalWrite(13, Arduino.HIGH);//set the LED　on
                Thread.Sleep(10);//delay a seconds
                Console.WriteLine(" off");
                arduino.digitalWrite(13, Arduino.LOW);//set the LED　off
                Thread.Sleep(10);//delay a seconds
            }
        }
    }
}
