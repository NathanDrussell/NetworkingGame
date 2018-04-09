using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multiPlayer_shooter
{
    class main
    {
        static void Main(string[] args)
        {

            Game g = new Game(800, 600);
            g.Run(30.0);


        }
    }
}
