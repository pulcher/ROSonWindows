using NStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace terminalGui.Views
{
    public class JoystickView: FrameView
    {
        public Label XLabel { get; set; }
        public TextField XValue { get; set; }

        public Label YLabel { get; set; }
        public TextField YValue { get; set; }

        public Label XDegreeLable { get; private set; }
        public TextField XDegree { get; set; }

        public Label YDegreeLabel { get; private set; }
        public TextField YDegree { get; set; }


        public JoystickView(ustring title) : base(title)
        {
            XLabel = new Label("Raw X:")
            {
                X = 0,
                Y = 0
            };
            Add(XLabel);

            XValue = new TextField("0.0000000000")
            {
                X = Pos.Right(XLabel) + 1,
                Y = Pos.Top(XLabel)
            };
            Add(XValue);

            YLabel = new Label("Raw Y:")
            {
                X = Pos.Left(XLabel),
                Y = Pos.Bottom(XLabel)
            };
            Add(YLabel);

            YValue = new TextField("0.0000000000")
            {
                X = Pos.Right(YLabel) + 1,
                Y = Pos.Top(YLabel)
            };
            Add(YValue);

            XDegree = new TextField("180")
            {
                X = 50,
                Y = Pos.Top(XLabel)
            };
            Add(XDegree);

            XDegreeLable = new Label("Servo X:")
            {
                X = Pos.Left(XDegree) - 9,
                Y = 0,
            };
            Add(XDegreeLable);

            var YDegree = new TextField("180")
            {
                X = 50,
                Y = Pos.Bottom(XDegreeLable)
            };
            Add(YDegree);

            YDegreeLabel = new Label("Servo X:")
            {
                X = Pos.Left(YDegree) - 9,
                Y = Pos.Bottom(XDegree),
            };
            Add(YDegreeLabel);
        }
    }
}
