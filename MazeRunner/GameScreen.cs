using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Linq.Expressions;

namespace MazeRunner
{
    public partial class GameScreen : UserControl
    {
        // player control keys
        Boolean leftArrowDown, rightArrowDown, upArrowDown, downArrowDown;

        // shooting control keys
        Boolean wKeyDown, aKeyDown, sKeyDown, dKeyDown;

        // game values
        public static int level;

        // player object
        Player player;

        // player values
        int playerLives;
        int playerSpeed;

        // powerup values
        int powerPick;

        // bullet values
        int bulletSize = 10;
        int xBulletSpeed = 15;
        double yBulletSpeed = 15;
        Boolean bulletRight, bulletUp;

        // lists
        public static List<Bullet> playerBullets = new List<Bullet>();
        public static List<Bullet> gremlinBullets = new List<Bullet>();
        List<Gremlin> gremlins = new List<Gremlin>();
        List<Powerup> powerups = new List<Powerup>();
        public static List<Wall> walls = new List<Wall>();

        // brushes
        SolidBrush wallBrush = new SolidBrush(Color.LimeGreen);
        SolidBrush pBulletBrush = new SolidBrush(Color.Blue);
        SolidBrush gBulletBrush = new SolidBrush(Color.Red);
        SolidBrush lifeUpBrush = new SolidBrush(Color.DarkGreen);
        SolidBrush fasterBrush = new SolidBrush(Color.Red);

        // images
        Image playerImage = Properties.Resources.player;

        // for random values
        Random randGen = new Random();

        public GameScreen()
        {
            InitializeComponent();
            OnStart(); // call onstart method
        }

        public void OnStart()
        {
            // starting level
            level = 1;

            // set player life counter
            playerLives = 3;

            // set all button presses to false
            leftArrowDown = rightArrowDown = upArrowDown = downArrowDown = wKeyDown = aKeyDown = sKeyDown = dKeyDown = false;

            // setup starting player values and create player
            int playerWidth = 40;
            int playerHeight = 60;
            int playerX = playerWidth;
            int playerY = ((this.Height / 2) - (playerHeight / 2));
            playerSpeed = 5;
            player = new Player(playerX, playerY, playerWidth, playerHeight, playerSpeed, playerLives);

            LevelMaker();
        }

        public void LevelMaker()
        {
            // variables for block x and y values
            string wallX;
            string wallY;
            string wallWidth;
            string wallHeight;
            int intX;
            int intY;
            int intWidth;
            int intHeight;

            // create xml reader
            XmlTextReader reader = new XmlTextReader($"Resources/level{level}.xml");

            reader.ReadStartElement("level");

            //Grabs all the blocks for the current level and adds them to the list
            while (reader.Read())
            {
                reader.ReadToFollowing("x");
                wallX = reader.ReadString();

                reader.ReadToFollowing("y");
                wallY = reader.ReadString();

                reader.ReadToFollowing("width");
                wallWidth = reader.ReadString();

                reader.ReadToFollowing("height");
                wallHeight = reader.ReadString();

                if (wallX != "")
                {
                    intX = Convert.ToInt32(wallX);
                    intY = Convert.ToInt32(wallY);
                    intWidth = Convert.ToInt32(wallWidth);
                    intHeight = Convert.ToInt32(wallHeight);
                    Wall w = new Wall(intX, intY, intWidth, intHeight);
                    walls.Add(w);
                }
            }
            // close reader
            reader.Close();
        }

        public void MakeBullet()
        {
            // create player bullet to go in the desired direction based on key press and add it to the list of player bullets
            if (dKeyDown == true)
            {
                bulletRight = true;
                Bullet newBullet = new Bullet(player.x, player.y, bulletSize, xBulletSpeed, bulletRight);
                playerBullets.Add(newBullet);
            }
            else if (aKeyDown == true)
            {
                bulletRight = false;
                Bullet newBullet = new Bullet(player.x, player.y, bulletSize, xBulletSpeed, bulletRight);
                playerBullets.Add(newBullet);
            }
            else if (wKeyDown == true)
            {
                bulletUp = true;
                Bullet newBullet = new Bullet(player.x, player.y, bulletSize, yBulletSpeed, bulletUp);
                playerBullets.Add(newBullet);
            }
            else if (sKeyDown == true)
            {
                bulletUp = false;
                Bullet newBullet = new Bullet(player.x, player.y, bulletSize, yBulletSpeed, bulletUp);
                playerBullets.Add(newBullet);
            }
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                // player moving button presses
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                // shooting button presses
                case Keys.W:
                    wKeyDown = true;
                    break;
                case Keys.A:
                    aKeyDown = true;
                    break;
                case Keys.S:
                    sKeyDown = true;
                    break;
                case Keys.D:
                    dKeyDown = true;
                    break;
                default:
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                // player moving button releases
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                // shooting button releases
                case Keys.W:
                    wKeyDown = false;
                    break;
                case Keys.A:
                    aKeyDown = false;
                    break;
                case Keys.S:
                    sKeyDown = false;
                    break;
                case Keys.D:
                    dKeyDown = false;
                    break;
                default:
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            #region player movement
            
            // move player in the direction of current arrow being pressed
            if (leftArrowDown == true && player.x > 0)
            {
                player.Move(playerSpeed, "left");
            }
            if (rightArrowDown == true && player.x < this.Width)
            {
                player.Move(playerSpeed, "right");
            }
            if (upArrowDown == true && player.y > 0)
            {
                player.Move(playerSpeed, "up");
            }
            if (downArrowDown == true && player.y < (this.Height - player.width))
            {
                player.Move(playerSpeed, "down");
            }

            #endregion

            #region player bullet drawing and movement 

            // see if a bullet should be made and call method to make the bullet 
            if (wKeyDown == true || aKeyDown == true || sKeyDown == true || dKeyDown == true)
            {
                MakeBullet();
            }

            // move player bullets
            foreach(Bullet b in playerBullets)
            {
                b.Move();
            }

            #endregion

            #region player collisions

            // check if player has collided with any bullets
            foreach (Bullet b in gremlinBullets)
            {
                if (player.BulletCollision(b))
                {
                    // play collision sound
                    playerLives--;

                    if (playerLives == 0)
                    {
                        OnEnd();
                    }
                }
            }

            // *NOT WORKING* check if player has collided with any walls 
            foreach (Wall w in walls)
            {
                if (player.WallCollision(w))
                {
                    if (leftArrowDown == true)
                    {
                        leftArrowDown = false;
                    }
                    else if (rightArrowDown == true)
                    {
                        rightArrowDown = false;
                    }
                    else if (upArrowDown == true)
                    {
                        upArrowDown = false;
                    }
                    else
                    {
                        downArrowDown = false;
                    }
                }
            }

            #endregion

            // redraw the screen
            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            // draws player image to screen
            e.Graphics.DrawImage(playerImage, player.x, player.y, player.width, player.height);

            // draws walls for the current level
            foreach (Wall w in walls)
            {
                e.Graphics.FillRectangle(wallBrush, w.x, w.y, w.width, w.height);
            }

            foreach (Bullet b in playerBullets)
            {
                e.Graphics.FillEllipse(pBulletBrush, b.x, b.y, b.size, b.size);
            }

            //foreach (Bullet b in gremlinBullets)
            //{
            //    e.Graphics.FillEllipse(gBulletBrush, b.x, b.y, b.size, b.size);
            //}
        }

        public void OnEnd()
        {
            // Goes to the game over screen
            Form form = this.FindForm();
            GameOverScreen go = new GameOverScreen();

            go.Location = new Point((form.Width - go.Width) / 2, (form.Height - go.Height) / 2);

            form.Controls.Add(go);
            form.Controls.Remove(this);
        }
    }
}

