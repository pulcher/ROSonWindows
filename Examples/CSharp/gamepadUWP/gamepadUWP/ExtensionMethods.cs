using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamepadUWP
{
    public static class MyExtensions
    {
        private static double biasAmount = 0.5d;

        public static double Bias(this double value)
        {
            return value - biasAmount;
        }
    }
}
