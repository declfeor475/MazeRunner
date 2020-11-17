using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeRunner
{
    public class Wall
    {
        public int x, y, width, height;
        public Color colour;

        public Wall(int _x, int _y, int _width, int _height)
        {
            x = _x;
            y = _y;
            width = _width;
            height = _height;
        }

        public bool BulletCollision(Bullet b)
        {
            Rectangle bulletRec = new Rectangle(b.x, b.y, b.size, b.size);
            Rectangle wallRec = new Rectangle(x, y, width, height);

            return bulletRec.IntersectsWith(wallRec);
        }
    }
}
