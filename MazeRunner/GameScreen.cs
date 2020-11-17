﻿using System;
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
using System.Threading;

namespace MazeRunner
{
    public partial class GameScreen : UserControl
    {
        #region public variables, objects, lists, brushes, and resources 

        // player control keys
        Boolean leftArrowDown, rightArrowDown, upArrowDown, downArrowDown;

        // shooting control keys
        Boolean wKeyDown, aKeyDown, sKeyDown, dKeyDown;

        public static int level; // level variable

        // player object and values
        Player player;
        int playerLives, playerSpeed;

        // bullet values
        int bulletSize = 10;
        int xBulletSpeed = 15;
        double yBulletSpeed = 15;
        Boolean bulletRight, bulletUp;

        // gremlin object and values
        Gremlin gremlin;
        int gremlinX, gremlinY, gremlinWidth, gremlinHeight, gremlinHealth;

        public static List<Bullet> playerBullets = new List<Bullet>(); // player bullets list
        public static List<Bullet> gremlinBullets = new List<Bullet>(); // gremlin bullets list
        public static List<Wall> walls = new List<Wall>(); // wall list

        SolidBrush wallBrush = new SolidBrush(Color.LimeGreen); // wall brush
        SolidBrush pBulletBrush = new SolidBrush(Color.Blue); // player bullets brush
        SolidBrush gBulletBrush = new SolidBrush(Color.Red); // gremlin bullets brush
        SolidBrush pHitBrush = new SolidBrush(Color.DarkRed); // player hit brush
        SolidBrush gHitBrush = new SolidBrush(Color.LightBlue); // gremlin hit brush

        Boolean playerHit, gremlinHit; // player and gremlin hit 

        Image playerImage = Properties.Resources.player; // player image
        Image gremlinImage = Properties.Resources.gremlin; // gremlin image

        Random randGen = new Random(); // for random values

        int bulletCounter = 21; // player bullet cooldown timer
        int gremlinCounter = 0; // gremlin bullet cooldown timer

        #endregion

        public GameScreen()
        {
            InitializeComponent();
            OnStart(); // call onstart method
            MakeGremlin(); // make gremlin for level
            LevelMaker(); // make level
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
            int playerWidth = 35;
            int playerHeight = 50;
            int playerX = playerWidth;
            int playerY = ((this.Height / 2) - (playerHeight / 2));
            playerSpeed = 5;
            player = new Player(playerX, playerY, playerWidth, playerHeight, playerSpeed, playerLives);
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

        public void MakeGremlin()
        {
            if (level == 1)
            {
                gremlinWidth = 80;
                gremlinHeight = 80;
                gremlinX = 750;
                gremlinY = 400;
                gremlinHealth = 1;
                gremlin = new Gremlin(gremlinX, gremlinY, gremlinWidth, gremlinHeight, gremlinHealth);
            }
        }

        public void MakePlayerBullet()
        {
            // create player bullet to go in the desired direction based on key press and add it to the list of player bullets
            if (rightArrowDown == true)
            {
                bulletRight = true;
                Bullet newBullet = new Bullet(player.x, player.y, bulletSize, xBulletSpeed, bulletRight);
                playerBullets.Add(newBullet);
            }
            else if (leftArrowDown == true)
            {
                bulletRight = false;
                Bullet newBullet = new Bullet(player.x, player.y, bulletSize, xBulletSpeed, bulletRight);
                playerBullets.Add(newBullet);
            }
            else if (upArrowDown == true)
            {
                bulletUp = true;
                Bullet newBullet = new Bullet(player.x, player.y, bulletSize, yBulletSpeed, bulletUp);
                playerBullets.Add(newBullet);
            }
            else if (downArrowDown == true)
            {
                bulletUp = false;
                Bullet newBullet = new Bullet(player.x, player.y, bulletSize, yBulletSpeed, bulletUp);
                playerBullets.Add(newBullet);
            }
        }

        public void MakeGremlinBullet()
        {
            int direction = randGen.Next(1, 5);

            if (direction == 1)
            {
                bulletRight = true;
                Bullet newBullet = new Bullet(gremlin.x, gremlin.y, bulletSize, xBulletSpeed, bulletRight);
                gremlinBullets.Add(newBullet);
            }
            else if (direction == 2)
            {
                bulletRight = false;
                Bullet newBullet = new Bullet(gremlin.x, gremlin.y, bulletSize, xBulletSpeed, bulletRight);
                gremlinBullets.Add(newBullet);
            }
            else if (direction == 3)
            {
                bulletUp = true;
                Bullet newBullet = new Bullet(gremlin.x, gremlin.y, bulletSize, yBulletSpeed, bulletUp);
                gremlinBullets.Add(newBullet);
            }
            else if (direction == 4)
            {
                bulletUp = false;
                Bullet newBullet = new Bullet(gremlin.x, gremlin.y, bulletSize, yBulletSpeed, bulletUp);
                gremlinBullets.Add(newBullet);
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
            // current player x and y positions
            int x = player.x;
            int y = player.y;

            // counters
            bulletCounter++;
            gremlinCounter++;

            #region player movement

            // move player in the direction of current arrow being pressed
            if (aKeyDown == true && player.x > 15)
            {
                player.Move(playerSpeed, "left");
            }
            if (dKeyDown == true)
            {
                player.Move(playerSpeed, "right");
            }
            if (wKeyDown == true && player.y > 0)
            {
                player.Move(playerSpeed, "up");
            }
            if (sKeyDown == true && player.y < (this.Height - player.height + 25))
            {
                player.Move(playerSpeed, "down");
            }

            #endregion

            #region player collisions

            // check if player has collided with any walls 
            foreach (Wall w in walls)
            {
                if (player.WallCollision(w))
                {
                    player.x = x;
                    player.y = y;
                    break;
                }
            }

            // check if player has collided with any bullets
            foreach (Bullet b in gremlinBullets)
            {
                if (player.BulletCollision(b))
                {
                    // play collision sound
                    playerHit = true;
                    playerLives--;
                    gremlinBullets.Remove(b);

                    if (playerLives == 0)
                    {
                        Thread.Sleep(2000);
                        OnEnd();
                    }
                    break;
                }
            }

            #endregion

            #region player bullet drawing and movement 

            // see if a bullet should be made and call method to make the bullet 
            if (bulletCounter > 10 && upArrowDown == true || bulletCounter > 10 && leftArrowDown == true || bulletCounter > 10 && downArrowDown == true || bulletCounter > 10 && rightArrowDown == true)
            {
                bulletCounter = 0;
                MakePlayerBullet();
            }

            // move player bullets
            foreach (Bullet b in playerBullets)
            {
                b.Move();
            }

            #endregion

            #region gremlin collisions

            // check if gremlin has been hit by any player bullets
            foreach (Bullet b in playerBullets)
            {
                if (gremlin.BulletCollision(b))
                {
                    // play collision sound
                    gremlinHit = true;
                    gremlinHealth--;
                    playerBullets.Remove(b);
                    break;
                }
            }

            #endregion

            #region gremlin bullet drawing and movement

            // see if the gremlin should fire a bullet
            if (gremlinCounter > 10)
            {
                gremlinCounter = 0;
                MakeGremlinBullet();
            }

            // move gremlin bullets
            foreach (Bullet b in gremlinBullets)
            {
                b.Move();
            }

            // do not fire any more gremlin bullets once gremlin has no health left
            if (gremlinHealth == 0)
            {
                gremlinCounter--;
            }

            #endregion

            #region bullet to wall collisions

            #endregion

            #region beat level

            // check if player has reached the end of the level
            if (player.x > this.Width - player.width && level < 5)
            {
                level++; // go to next level

                // player starting position
                player.x = 35;
                player.y = (this.Height / 2) - 25;

                // clear bullets and walls still on screen from previous level
                playerBullets.Clear();
                gremlinBullets.Clear();
                walls.Clear();

                LevelMaker(); // make next level
            }
            else if (player.x > this.Width - player.width && level == 5) // if player was on final level
            {
                gameTimer.Enabled = false;
                OnEnd(); // go to game over screen
            }

            #endregion

            // redraw the screen
            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            // draws player image to screen
            e.Graphics.DrawImage(playerImage, player.x, player.y, player.width, player.height);

            // draws gremlin image to screen
            if (gremlinHealth > 0)
            {
                e.Graphics.DrawImage(gremlinImage, gremlin.x, gremlin.y, gremlin.width, gremlin.height);
            }

            // draws walls for the current level
            foreach (Wall w in walls)
            {
                e.Graphics.FillRectangle(wallBrush, w.x, w.y, w.width, w.height);
            }

            // draws player fired bullets 
            foreach (Bullet b in playerBullets)
            {
                e.Graphics.FillEllipse(pBulletBrush, b.x, b.y, b.size, b.size);
            }

            // draws gremlin fired bullets
            foreach (Bullet b in gremlinBullets)
            {
                e.Graphics.FillEllipse(gBulletBrush, b.x, b.y, b.size, b.size);
            }

            // player getting hit animation
            if (playerHit == true)
            {
                for (int i = 0; i < 3; i++)
                {
                    e.Graphics.FillRectangle(pHitBrush, player.x, player.y, player.width, player.height);
                }

                playerHit = false;
            }

            // gremlin getting hit animation
            if (gremlinHit == true)
            {
                for (int i = 0; i < 3; i++)
                {
                    e.Graphics.FillRectangle(gHitBrush, gremlin.x, gremlin.y, gremlin.width, gremlin.height);
                }

                gremlinHit = false;
            }
        }

        public void HitAnimation()
        {

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

