using System.Drawing;

namespace MazeRunner
{
    public class Player
    {
        // player variables
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
            // create rectangle for wall and player
            Rectangle wallRec = new Rectangle(w.x, w.y, w.width, w.height);
            Rectangle playerRec = new Rectangle(x, y, width, height);

            return wallRec.IntersectsWith(playerRec); // true if collided, false if not
        }

        public bool BulletCollision(Bullet b)
        {
            // create rectangle for bullet and player
            Rectangle bulletRec = new Rectangle(b.x, b.y, b.size, b.size);
            Rectangle playerRec = new Rectangle(x, y, width, height);

            return bulletRec.IntersectsWith(playerRec); // true if collided, false if not
        }
    }
}
