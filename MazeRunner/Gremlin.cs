using System.Drawing;

namespace MazeRunner
{
    public class Gremlin
    {
        // gremlin variables
        public int x, y, width, height, health;

        public Gremlin(int _x, int _y, int _width, int _height, int _health)
        {
            x = _x;
            y = _y;
            width = _width;
            height = _height;
            health = _health;
        }

        public bool BulletCollision(Bullet b)
        {
            // create rectangle for bullet and gremlin
            Rectangle bulletRec = new Rectangle(b.x, b.y, b.size, b.size);
            Rectangle gremlinRec = new Rectangle(x, y, width, height);

            return bulletRec.IntersectsWith(gremlinRec); // true if collided, false if not
        }
    }
}
