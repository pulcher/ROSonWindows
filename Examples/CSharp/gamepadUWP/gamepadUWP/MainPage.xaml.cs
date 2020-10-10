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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

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

            //GetGamepads();

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
            var test = RawGameController.RawGameControllers;

            foreach (var controller in test)
            {
                var sometihng = controller.DisplayName;
                var somethingElse = controller.ButtonCount;

                var ugh = string.Empty;
            }
        }

        //private void GetGamepads()
        //{

        //    foreach (var gamepad in Gamepad.Gamepads)
        //    {
        //        // Check if the gamepad is already in myGamepads; if it isn't, add it.
        //        bool gamepadInList = myGamepads.Contains(gamepad);

        //        if (!gamepadInList)
        //        {
        //            // This code assumes that you're interested in all gamepads.
        //            myGamepads.Add(gamepad);
        //        }
        //    }
        //}
    }
}


/*
            // setup delegates for adding/removing to the gamepad list
            Gamepad.GamepadAdded += (object sender, Gamepad e) =>
            {
                // Check if the just-added gamepad is already in myGamepads; if it isn't, add
                // it.
                lock (myLock)
                {
                    bool gamepadInList = myGamepads.Contains(e);

                    if (!gamepadInList)
                    {
                        myGamepads.Add(e);
                    }
                }
            };

            Gamepad.GamepadRemoved += (object sender, Gamepad e) =>
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
 */

