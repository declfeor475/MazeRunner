using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeRunner
{
    public class Gremlin
    {
        public int x, y, width, height, health;

        public Gremlin(int _x, int _y, int _width, int _height, int _health)
        {
            x = _x;
            y = _y;
            width = _width;
            height = _height;
            health = _health;
        }
    }
}
