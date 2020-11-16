using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeRunner
{
    public class Player
    {
        public int x, y, width, height, speed, health;
        public string direction;

        public Player(int _x, int _y, int _width, int _height, int _speed, int _health)
        {
            x = _x;
            y = _y;
            width = _width;
            height = _height;
            speed = _speed;
            health = _health;
        }

        public void Move(int speed, string direction)
        {
            // player moving along x axis
            if (direction == "left")
            {
                x -= speed;
            }
            if (direction == "right")
            {
                x += speed;
            }

            // player moving along y axis
            if (direction == "up")
            {
                y -= speed;
            }
            if (direction == "down")
            {
                y += speed;
            }
        }

        public bool WallCollision(Wall w)
        {
            Rectangle wallRec = new Rectangle(w.x, w.y, w.width, w.height);
            Rectangle playerRec = new Rectangle(x, y, width, height);

            return wallRec.IntersectsWith(playerRec);
        }

        public bool BulletCollision(Bullet b)
        {
            Rectangle bulletRec = new Rectangle(b.x, b.y, b.size, b.size);
            Rectangle playerRec = new Rectangle(x, y, width, height);

            return bulletRec.IntersectsWith(playerRec);
        }
    }
}
