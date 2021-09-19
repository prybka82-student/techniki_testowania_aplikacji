using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTO
{
    internal class Calculator
    {
        public int Sum(int a, int b)
        {
            return a + b;
        }

        public int Subtract(int a, int b)
        {
            return a - b;
        }

        public decimal Divide(int a, int b)
        {
            return ((decimal)a) / b;
        }
    }
}
