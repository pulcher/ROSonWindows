using LattePanda.Firmata;
using System;
using Terminal.Gui;
using terminalGui.Views;

namespace terminalGui
{
    class Program
    {
        private const string robotName = "Robot Need A Name";
        private static JoystickView LeftJoystickView;
        private static JoystickView RightJoystickView;
        private static ControllerPad GamePad;

        public const int SERVO1 = 5;
        public const int SERVO2 = 6;
        private static Arduino Arduino;

        static void Main(string[] args)
        {
            Application.Init();
            SetupDisplay();

            GamePad = new ControllerPad();

            Application.MainLoop.AddTimeout(TimeSpan.FromMilliseconds(100), UpdateJoysticks);

            Arduino = new Arduino("COM7", 250000, true, 8000);

            Arduino.pinMode(SERVO1, Arduino.SERVO);
            Arduino.pinMode(SERVO2, Arduino.SERVO);

            Application.Run();
        }

        private static bool UpdateJoysticks(MainLoop arg)
        {
            if (!GamePad.Poll()) return true;

            if (LeftJoystickView != null)
            {
                AdjustTextField(LeftJoystickView.XValue, GamePad.LeftJoystickX);
                AdjustTextField(LeftJoystickView.YValue , GamePad.LeftJoystickY);
                AdjustTextField(LeftJoystickView.XDegree, GamePad.LeftDegreeX);
                AdjustTextField(LeftJoystickView.YDegree, GamePad.LeftDegreeY);
            }

            if (RightJoystickView != null)
            {
                AdjustTextField(RightJoystickView.XValue, GamePad.RightJoystickX);
                AdjustTextField(RightJoystickView.YValue, GamePad.RightJoystickY);
            }

            return true;
        }

        private static void AdjustTextField(TextField textField, double value)
        {
            if (textField != null)
                textField.Text = value.ToString();
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
