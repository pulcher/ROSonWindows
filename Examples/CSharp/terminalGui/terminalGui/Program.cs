using System;
using Terminal.Gui;

namespace terminalGui
{
    class Program
    {
        private const string robotName = "Robot Need A Name";

        static void Main(string[] args)
        {
            Application.Init();
            SetupDisplay();

            Application.Run();
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

            //var login = new Label("Login: ") { X = 3, Y = 2 };
            //var password = new Label("Password: ")
            //{
            //	X = Pos.Left(login),
            //	Y = Pos.Top(login) + 1
            //};
            //var loginText = new TextField("")
            //{
            //	X = Pos.Right(password),
            //	Y = Pos.Top(login),
            //	Width = 40
            //};
            //var passText = new TextField("")
            //{
            //	Secret = true,
            //	X = Pos.Left(loginText),
            //	Y = Pos.Top(password),
            //	Width = Dim.Width(loginText)
            //};
        }

        private static void SetupJoystickInfo(Window win)
        {
            var leftJoystickView = new FrameView
            {
                X = 0,
                Y = 0,
                Title = "Left Joystick",
                Width = Dim.Percent(50),
                Height = 4
            };

            joystickDisplaySetup(leftJoystickView);

            //var servolabel

            var rightJoystickView = new FrameView
            {
                X = Pos.Right(leftJoystickView),
                Y = Pos.Top(leftJoystickView),
                Title = "Right Joystick",
                Width = Dim.Fill(),
                Height = 4
            };

            joystickDisplaySetup(rightJoystickView);

            win.Add(leftJoystickView, rightJoystickView);
        }

        private static void joystickDisplaySetup(FrameView joystickView)
        {
            var joystickXLabel = new Label("Raw X:")
            {
                X = 0,
                Y = 0
            };
            var joystickXText = new TextField("0.0000000000")
            {
                X = Pos.Right(joystickXLabel) + 1,
                Y = Pos.Top(joystickXLabel)
            };

            var joystickYLabel = new Label("Raw Y:")
            {
                X = Pos.Left(joystickXLabel),
                Y = Pos.Bottom(joystickXLabel)
            };

            var joystickYText = new TextField("0.0000000000")
            {
                X = Pos.Right(joystickYLabel) + 1,
                Y = Pos.Top(joystickYLabel)
            };

            var servoXText = new TextField("180")
            {
                X = 50,
                Y = Pos.Top(joystickXLabel)
            };

            var servoXLabel = new Label("Servo X:")
            {
                X = Pos.Left(servoXText) - 9,
                Y = 0,
            };

            var servoYText = new TextField("180")
            {
                X = 50,
                Y = Pos.Bottom(servoXLabel)
            };

            var servoYLabel = new Label("Servo X:")
            {
                X = Pos.Left(servoYText) - 9,
                Y = Pos.Bottom(servoXText),
            };

            joystickView.Add(
                joystickXLabel, joystickXText, joystickYLabel, joystickYText,
                servoXLabel, servoXText, servoYText, servoYLabel
                );
        }
    }
}
