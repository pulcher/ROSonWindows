using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Gaming.Input;
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
        private List<RawGameController> myGamepads = new List<RawGameController>();
        private RawGameController mainGamepad;

        public MainPage()
        {
            this.InitializeComponent();

            GameControllersSetup();

            GetRawGameControllers();
        }

        private void GameControllersSetup()
        {
            // setup delegates for adding/removing to the gamepad list
            RawGameController.RawGameControllerAdded += (object sender, RawGameController e) =>
            {
                //Check if the just - added gamepad is already in myGamepads; if it isn't, add
                // it.

                bool gamepadInList = myGamepads.Contains(e);

                if (!gamepadInList)
                {
                    myGamepads.Add(e);
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

        private void GetRawGameControllers()
        {
            foreach (var controller in myGamepads)
            {
                var sometihng = controller.DisplayName;
                var somethingElse = controller.ButtonCount;

                var ugh = string.Empty;
            }
        }
    }
}
