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
        private List<RawGameController> myGamepads = new List<RawGameController>();
        private RawGameController mainGamepad;
        private bool[] buttonArray;
        private GameControllerSwitchPosition[] switchArray;
        private double[] axisArray;

        public MainPage()
        {
            this.InitializeComponent();

            GameControllersSetup();

            GetRawGameControllers();

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
            var gamepad = myGamepads[0];

            var reading = gamepad.GetCurrentReading(buttonArray, switchArray, axisArray);

            UpdateButtonStatus();
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

            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                LeftXValue.Text = axisArray[0].ToString("F6");
                LeftYValue.Text = axisArray[1].ToString("F6");
                RightXValue.Text = axisArray[2].ToString("F6");
                RightYValue.Text = axisArray[3].ToString("F6");
                DPadXValue.Text = axisArray[4].ToString("F6");
                DPadYValue.Text = axisArray[5].ToString("F6");
            });
        }
    }
}
