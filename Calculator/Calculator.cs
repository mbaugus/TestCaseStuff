using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikesLib
{
    // throws application exception
    public class Calculator
    {
        public double Add(string input, out bool error)
        {
            if(input.Length == 0)
            {
                throw new ApplicationException();
            }

            error = true;
            input = input.Trim();
            input = RemoveJunk(input); // removes all BUT numbers and commas
            input = input.Trim(','); // remove leading and trailing commas

            string[] num = input.Split(',');

            if(num.Count() < 2 || num.Count() > 10) // less than 2 is bad, more than 10 is bad.
            {
                return 0;
            }

            double total = 0;
            foreach (string i in num)
            {
                if(!double.TryParse(i, out double result))
                {
                    return 0;
                }
                total += result;
            }

            // has to reach here for error to be false.
            error = false;
            return total;
        }

        private string RemoveJunk(string input)
        {
            StringBuilder sb = new StringBuilder();
            char[] good = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', ',', '.', '-' };
            //good.
            for (int i = 0; i < input.Length; i++)
            {
                if (good.Contains(input[i]))
                {
                    sb.Append(input[i]);
                }
            }
            return sb.ToString();
        }

    }
}
