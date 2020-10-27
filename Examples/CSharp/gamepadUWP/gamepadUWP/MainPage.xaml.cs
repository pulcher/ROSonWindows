using LattePanda.Firmata;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Timers;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Gaming.Input;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace gamepadUWP
{
    public sealed partial class MainPage : Page
    {
        private readonly object myLock = new object();
        private readonly Brush DefaultButtonBackground = new SolidColorBrush(Colors.AntiqueWhite);
        private readonly Brush HighlightedButtonBackground = new SolidColorBrush(Colors.Orange);
        private readonly Brush DeadzoneNormal = new SolidColorBrush(Colors.Yellow);
        private readonly Brush DeadzoneActivated = new SolidColorBrush(Colors.SpringGreen);
        private readonly double offsetBias = 0.5d;
        private double deadZoneRadius = 0.05d;
        private double[] currentRadi = new double[2];
        private List<RawGameController> myGamepads = new List<RawGameController>();
        private RawGameController mainGamepad;
        private bool[] buttonArray;
        private GameControllerSwitchPosition[] switchArray;
        private double[] axisArray;
        private int[] degrees = new int[4];

        // known degrees
        private readonly int StopDegrees = 91;
        private readonly int MinDegrees = 0;
        private readonly int MaxDegrees = 180;
        public const int SERVO1 = 5;
        public const int SERVO2 = 6;
        private static Arduino Arduino;

        public MainPage()
        {
            this.InitializeComponent();

            GameControllersSetup();

            GetRawGameControllers();

            Arduino = new Arduino("COM8", 250000, true, 8000);

            //Arduino.pinMode(5, Arduino.SERVO);
            //Arduino.pinMode(6, Arduino.SERVO);

            //StopMotors();

            var pollingTimer = new Timer();
            pollingTimer.Elapsed += new ElapsedEventHandler(PollTimeEvent);
            pollingTimer.Interval = 100;
            pollingTimer.Start();
        }

        private async Task GameControllersSetup()
        {
            // setup delegates for adding/removing to the gamepad list
            RawGameController.RawGameControllerAdded += async (object sender, RawGameController e) =>
            {
                //Check if the just - added gamepad is already in myGamepads; if it isn't, add
                // it.

                bool gamepadInList = myGamepads.Contains(e);

                if (!gamepadInList)
                {
                    buttonArray = new bool[e.ButtonCount];
                    switchArray = new GameControllerSwitchPosition[e.SwitchCount];
                    axisArray = new double[e.AxisCount];

                    myGamepads.Add(e);
                }

                // add a placeholder for the number of buttons
                for (int i = 0; i < e.ButtonCount; i++)
                {
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        ButtonPanelHolder.Children.Add(CreateButtonDisplay(i));
                    });

                }
            };

            RawGameController.RawGameControllerRemoved += (object sender, RawGameController e) =>
            {
                lock (myLock)
                {
                    int indexRemoved = myGamepads.IndexOf(e);

                    if (indexRemoved > -1)
                    {
                        if (mainGamepad == myGamepads[indexRemoved])
                        {
                            mainGamepad = null;
                        }

                        myGamepads.RemoveAt(indexRemoved);
                    }
                }
            };
        }

        private UIElement CreateButtonDisplay(int buttonNumber)
        {
            var border = new Border();
            border.Background = DefaultButtonBackground;
            border.Child = new TextBlock
            {
                Text = buttonNumber.ToString("D2"),
                Foreground = new SolidColorBrush(Colors.Black),
                FontSize = 12,
                Margin = new Thickness(0, 0, 5, 0)
            };

            return border;
        }

        private void GetRawGameControllers()
        {
            foreach (var controller in myGamepads)
            {
                var sometihng = controller.DisplayName;
                var somethingElse = controller.ButtonCount;

                var ugh = string.Empty;
            }
        }

        private void PollTimeEvent(object sender, ElapsedEventArgs e)
        {
            if (myGamepads.Count > 0)
                PollRawGameControllers();
        }

        private void PollRawGameControllers()
        {
            if (myGamepads.Count <= 0)
                return;

            var gamepad = myGamepads[0];

            var reading = gamepad.GetCurrentReading(buttonArray, switchArray, axisArray);

            UpdateDeadZoneStuff();

            UpdateDegrees();

            UpdateButtonStatus();
        }

        private void UpdateDegrees()
        {
            degrees[0] = CalculateDegrees(currentRadi[0], Bias(axisArray[0]));
            degrees[1] = CalculateDegrees(currentRadi[0], Bias(axisArray[1]));
            degrees[2] = CalculateDegrees(currentRadi[1], Bias(axisArray[2]));
            degrees[3] = CalculateDegrees(currentRadi[1], Bias(axisArray[3]));
        }

        private int CalculateDegrees(double radius, double value)
        {
            var result = StopDegrees;
            var scaledValue = 0.00d;

            if (radius <= deadZoneRadius)
            {
                return result;
            }

            scaledValue = 180 * (value + 0.5);

            result = Convert.ToInt32(scaledValue);
            return result;
        }

        private void UpdateDeadZoneStuff()
        {
            currentRadi[0] = Math.Sqrt(Bias(axisArray[0]) * Bias(axisArray[0]) + Bias(axisArray[1]) * Bias(axisArray[1]));
            currentRadi[1] = Math.Sqrt(Bias(axisArray[2]) * Bias(axisArray[2]) + Bias(axisArray[3]) * Bias(axisArray[3]));
        }

        private double Bias(double value)
        {
            return value - offsetBias;
        }

        private async void UpdateButtonStatus()
        {

            var buttonsDisplayed = ButtonPanelHolder.Children;
            for (int i = 0; i < buttonArray.Count(); i++)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    var currentBorder = buttonsDisplayed[i] as Border;
                    if (buttonArray[i])
                        currentBorder.Background = HighlightedButtonBackground;
                    else
                        currentBorder.Background = DefaultButtonBackground;

                });
            };

            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, (Windows.UI.Core.DispatchedHandler)(() =>
            {
                LeftXValue.Text = axisArray[0].ToString("F15");
                LeftBiasXValue.Text = Bias(axisArray[0]).ToString("F15");
                LeftXDeg.Text = degrees[0].ToString();

                LeftYValue.Text = axisArray[1].ToString("F15");
                LeftBiasYValue.Text = Bias(axisArray[1]).ToString("F15");
                LeftYDeg.Text = degrees[1].ToString();

                RightXValue.Text = axisArray[2].ToString("F15");
                RightBiasXValue.Text = Bias(axisArray[2]).ToString("F15");
                RightXDeg.Text = degrees[2].ToString();

                RightYValue.Text = axisArray[3].ToString("F15");
                RightBiasYValue.Text = Bias(axisArray[3]).ToString("F15");
                RightYDeg.Text = degrees[3].ToString();

                DPadXValue.Text = axisArray[4].ToString("F15");
                DPadYValue.Text = axisArray[5].ToString("F15");

                LeftDeadZone.Foreground = Math.Abs(currentRadi[0]) < deadZoneRadius ? DeadzoneActivated : DeadzoneNormal;
                RightDeadZone.Foreground = Math.Abs(currentRadi[1]) < deadZoneRadius ? DeadzoneActivated : DeadzoneNormal;
            }));
        }

        private static async Task StopMotors()
        {
            Console.WriteLine("Stopping...");
            //Arduino.servoWrite(SERVO1, 92);
            //Arduino.servoWrite(SERVO2, 92);
            await Task.Delay(2000);
        }
    }
}
