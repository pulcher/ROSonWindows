﻿using HIDDevices;
using HIDDevices.Controllers;
using System;
using System.Threading.Tasks;

namespace terminalGui
{
    public class ControllerPad
    {
        public Devices devices { get; }
        public Gamepad? gamepad { get; set; }

        public double LeftJoystickX { get; set; }
        public double LeftJoystickY { get; set; }
        public int LeftDegreeX { get; set; }
        public int LeftDegreeY { get; set; }

        public double RightJoystickX { get; set; }
        public double RightJoystickY { get; set; }

        public ControllerPad()
        {
            devices = new Devices();
        }

        public bool Poll()
        {
            using var subscription = devices.Controllers<Gamepad>().Subscribe(g =>
            {
                if (gamepad?.IsConnected == true)
                {
                    return;
                }

                // Assign this gamepad and connect to it.
                gamepad = g;
                g.Connect();
            });

            var currentGamepad = gamepad;
            if (currentGamepad?.IsConnected != true) { return true; }

            LeftJoystickX = currentGamepad.X;
            LeftJoystickY = currentGamepad.Y;
            LeftDegreeX = MapDegree(currentGamepad.X);
            LeftDegreeY = MapDegree(currentGamepad.Y);

            RightJoystickX = 0.00d;
            RightJoystickY = 0.00d;

            return true;
        }

        private int MapDegree(double x)
        {
            return Convert.ToInt32(180 * (x + 1.0) / 2);
        }
    }
}
