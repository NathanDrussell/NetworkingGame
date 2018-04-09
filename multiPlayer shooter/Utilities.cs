using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multiPlayer_shooter
{
    class Utilities
    {

        public static float NormalizedValue(float input, float offset)
        {
            float res = input - offset;
            return res / offset;
        }
        public static void Log(params string[] log)
        {
            foreach (string s in log)
            {
                Console.WriteLine(s);
            }
        }

    }
}
