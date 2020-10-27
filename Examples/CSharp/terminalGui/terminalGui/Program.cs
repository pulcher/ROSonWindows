using System;
using System.Security.Cryptography;
using Terminal.Gui;
using terminalGui.Views;

namespace terminalGui
{
    class Program
    {
        private const string robotName = "Robot Need A Name";
        private static JoystickView LeftJoystickView;
        private static JoystickView RightJoystickView;
        private static Random Rng;

        static void Main(string[] args)
        {
            Application.Init();
            SetupDisplay();

            Rng = new Random();

            Application.MainLoop.AddTimeout(TimeSpan.FromMilliseconds(100), UpdateJoysticks);

            Application.Run();
        }

        private static bool UpdateJoysticks(MainLoop arg)
        {
            LeftJoystickView.XValue.Text = Rng.NextDouble().ToString();
            return true;
        }

        private static void SetupDisplay()
        {
            var top = Application.Top;

            var win = new Window(robotName)
            {
                X = 0,
                Y = 1, // Leave one row for the toplevel menu

                // By using Dim.Fill(), it will automatically resize without manual intervention
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            top.Add(win);

            SetupJoystickInfo(win);
        }

        private static void SetupJoystickInfo(Window win)
        {
            LeftJoystickView = new JoystickView("Left Joystick")
            {
                X = 0,
                Y = 0,
                Width = Dim.Percent(50),
                Height = 4
            };

            RightJoystickView = new JoystickView("Right Joystick")
            {
                X = Pos.Right(LeftJoystickView),
                Y = Pos.Top(LeftJoystickView),
                Width = Dim.Fill(),
                Height = 4
            };

            win.Add(LeftJoystickView, RightJoystickView);
        }
    }
}
