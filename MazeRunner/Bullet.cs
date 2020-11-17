using System;

namespace MazeRunner
{
    public class Bullet
    {
        // bullet variables
        public int x, y, xSpeed, size;
        public double ySpeed; // use a double in order to let ther be two seperate objects for bullets moving along the x axis and bullets moving along the y axis
        Boolean bulletRight, bulletUp;

        public Bullet(int _x, int _y, int _size, int _xSpeed, bool _bulletRight)
        {
            x = _x;
            y = _y;
            size = _size;
            xSpeed = _xSpeed;
            bulletRight = _bulletRight;
        }

        public Bullet(int _x, int _y, int _size, double _ySpeed,  bool _bulletUp)
        {
            x = _x;
            y = _y;
            size = _size;
            ySpeed = _ySpeed;            
            bulletUp = _bulletUp;
        }

        public void Move()
        {
            //ball goes left/right
            if (bulletRight == true)
            {
                x += xSpeed;
            }
            else
            {
                x -= xSpeed;
            }

            // ball goes up/down
            if (bulletUp == true)
            {
                y -= Convert.ToInt16(ySpeed);
            }
            else
            {
                y += Convert.ToInt16(ySpeed);
            }
        }
    }
}
