using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeRunner
{
    public class Gremlin
    {
        public int x, y, health;
        string direction;

        public Gremlin(int _x, int _y, int _health, string _direction)
        {
            x = _x;
            y = _y;
            health = _health;
            direction = _direction;
        }

        public void Shoot(string direction)
        {
            if (direction == "left")
            {

            }
            if (direction == "right")
            {

            }
            if (direction == "up")
            {

            }
            if (direction == "down")
            {

            }
        }
    }
}
