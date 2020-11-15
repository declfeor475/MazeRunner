using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeRunner
{
    public class Powerup
    {
        public int x, y, width, height;
        string powerup;

        public Powerup(int _x, int _y, int _width, int _height, string _powerup)
        {
            x = _x;
            y = _y;
            width = _width;
            height = _height;
            powerup = _powerup;
        }
    }
}
