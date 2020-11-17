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
using System.Threading;
using System.Media;

namespace MazeRunner
{
    public partial class GameScreen : UserControl
    {
        #region public variables, objects, lists, brushes, and resources 

        public static Boolean win; // for determining game over screen background

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

        // gremlin objects and values
        Gremlin gremlin1, gremlin2, gremlin3, gremlin4, gremlin5;
        int gremlinX, gremlinY, gremlinWidth, gremlinHeight, gremlinHealth;

        public static List<Bullet> playerBullets = new List<Bullet>(); // player bullets list
        public static List<Bullet> gremlinBullets = new List<Bullet>(); // gremlin bullets list
        public static List<Wall> walls = new List<Wall>(); // wall list
        public static List<Gremlin> gremlins = new List<Gremlin>(); // gremlins list

        SolidBrush wallBrush = new SolidBrush(Color.LimeGreen); // wall brush
        SolidBrush pBulletBrush = new SolidBrush(Color.Blue); // player bullets brush
        SolidBrush gBulletBrush = new SolidBrush(Color.Red); // gremlin bullets brush
        SolidBrush pHitBrush = new SolidBrush(Color.DarkRed); // player hit brush
        SolidBrush gHitBrush = new SolidBrush(Color.LightBlue); // gremlin hit brush

        Boolean playerHit, gremlinHit; // player and gremlin hit 

        Image playerImage = Properties.Resources.player; // player image
        Image gremlinImage = Properties.Resources.gremlin; // gremlin image

        SoundPlayer playerSound = new SoundPlayer(Properties.Resources.playerHit); // player collision sound
        SoundPlayer gremlinSound = new SoundPlayer(Properties.Resources.gremlinHit); // gremlin collision sound

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

            // display current level and player health to labels
            levelLabel.Text = "Level: " + level;
            healthLabel.Text = "Lives: " + playerLives;

            // set all button presses to false
            leftArrowDown = rightArrowDown = upArrowDown = downArrowDown = wKeyDown = aKeyDown = sKeyDown = dKeyDown = false;

            // setup starting player values and create player
            int playerWidth = 35;
            int playerHeight = 50;
            int playerX = 0;
            int playerY = ((this.Height / 2) - (playerHeight / 2));
            playerSpeed = 5;            
            player = new Player(playerX, playerY, playerWidth, playerHeight, playerSpeed, playerLives);

            // start the game engine loop
            gameTimer.Enabled = true;
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
            // gremlins dimensions
            gremlinWidth = 60;
            gremlinHeight = 70;

            // add gremlins to level specified
            if (level == 1)
            {
                gremlinX = 750;
                gremlinY = 400;
                gremlinHealth = level;
                gremlin1 = new Gremlin(gremlinX, gremlinY, gremlinWidth, gremlinHeight, gremlinHealth);
                gremlins.Add(gremlin1);
            }
            else if (level == 2)
            {
                gremlinX = 400;
                gremlinY = 220;
                gremlinHealth = level;
                gremlin1 = new Gremlin(gremlinX, gremlinY, gremlinWidth, gremlinHeight, gremlinHealth);
                gremlins.Add(gremlin1);

                gremlinX = 600;
                gremlinY = 220;
                gremlinHealth = level;
                gremlin2 = new Gremlin(gremlinX, gremlinY, gremlinWidth, gremlinHeight, gremlinHealth);
                gremlins.Add(gremlin2);
            }
            else if (level == 3)
            {
                gremlinX = 110;
                gremlinY = 390;
                gremlinHealth = level;
                gremlin1 = new Gremlin(gremlinX, gremlinY, gremlinWidth, gremlinHeight, gremlinHealth);
                gremlins.Add(gremlin1);

                gremlinX = 520;
                gremlinY = 250;
                gremlinHealth = level;
                gremlin2 = new Gremlin(gremlinX, gremlinY, gremlinWidth, gremlinHeight, gremlinHealth);
                gremlins.Add(gremlin2);

                gremlinX = 900;
                gremlinY = 220;
                gremlinHealth = level;
                gremlin3 = new Gremlin(gremlinX, gremlinY, gremlinWidth, gremlinHeight, gremlinHealth);
                gremlins.Add(gremlin3);
            }
            else if (level == 4)
            {
                gremlinX = 30;
                gremlinY = 30;
                gremlinHealth = level;
                gremlin1 = new Gremlin(gremlinX, gremlinY, gremlinWidth, gremlinHeight, gremlinHealth);
                gremlins.Add(gremlin1);

                gremlinX = 30;
                gremlinY = 400;
                gremlinHealth = level;
                gremlin2 = new Gremlin(gremlinX, gremlinY, gremlinWidth, gremlinHeight, gremlinHealth);
                gremlins.Add(gremlin2);

                gremlinX = 910;
                gremlinY = 30;
                gremlinHealth = level;
                gremlin3 = new Gremlin(gremlinX, gremlinY, gremlinWidth, gremlinHeight, gremlinHealth);
                gremlins.Add(gremlin3);

                gremlinX = 910;
                gremlinY = 400;
                gremlinHealth = level;
                gremlin4 = new Gremlin(gremlinX, gremlinY, gremlinWidth, gremlinHeight, gremlinHealth);
                gremlins.Add(gremlin4);
            }
            else if (level == 5)
            {
                gremlinX = 470;
                gremlinY = 30;
                gremlinHealth = level;
                gremlin1 = new Gremlin(gremlinX, gremlinY, gremlinWidth, gremlinHeight, gremlinHealth);
                gremlins.Add(gremlin1);

                gremlinX = 300;
                gremlinY = 225;
                gremlinHealth = level;
                gremlin2 = new Gremlin(gremlinX, gremlinY, gremlinWidth, gremlinHeight, gremlinHealth);
                gremlins.Add(gremlin2);

                gremlinX = 470;
                gremlinY = 225;
                gremlinHealth = level;
                gremlin3 = new Gremlin(gremlinX, gremlinY, gremlinWidth, gremlinHeight, gremlinHealth);
                gremlins.Add(gremlin3);

                gremlinX = 640;
                gremlinY = 225;
                gremlinHealth = level;
                gremlin4 = new Gremlin(gremlinX, gremlinY, gremlinWidth, gremlinHeight, gremlinHealth);
                gremlins.Add(gremlin4);

                gremlinX = 470;
                gremlinY = 420;
                gremlinHealth = level;
                gremlin5 = new Gremlin(gremlinX, gremlinY, gremlinWidth, gremlinHeight, gremlinHealth);
                gremlins.Add(gremlin5);
            }
        }

        public void MakePlayerBullet()
        {
            // create player bullet to go in the desired direction based on key press and add it to the list of player bullets
            if (rightArrowDown == true)
            {
                bulletRight = true;
                Bullet newBullet = new Bullet(player.x + ((player.width / 2) - (bulletSize / 2)), player.y + ((player.height / 2) - (bulletSize / 2)), bulletSize, xBulletSpeed, bulletRight);
                playerBullets.Add(newBullet);
            }
            else if (leftArrowDown == true)
            {
                bulletRight = false;
                Bullet newBullet = new Bullet(player.x + ((player.width / 2) - (bulletSize / 2)), player.y + ((player.height / 2) - (bulletSize / 2)), bulletSize, xBulletSpeed, bulletRight);
                playerBullets.Add(newBullet);
            }
            else if (upArrowDown == true)
            {
                bulletUp = true;
                Bullet newBullet = new Bullet(player.x + ((player.width / 2) - (bulletSize / 2)), player.y + ((player.height / 2) - (bulletSize / 2)), bulletSize, yBulletSpeed, bulletUp);
                playerBullets.Add(newBullet);
            }
            else if (downArrowDown == true)
            {
                bulletUp = false;
                Bullet newBullet = new Bullet(player.x + ((player.width / 2) - (bulletSize / 2)), player.y + ((player.height / 2) - (bulletSize / 2)), bulletSize, yBulletSpeed, bulletUp);
                playerBullets.Add(newBullet);
            }
        }

        public void MakeGremlinBullet()
        {
            foreach (Gremlin g in gremlins)
            {
                int direction = randGen.Next(1, 5); // get direction bullet will go in

                // add correct gremlin bullet to the list
                if (direction == 1)
                {
                    bulletRight = true;
                    Bullet newBullet = new Bullet(g.x + ((g.width / 2) - (bulletSize / 2)), g.y + ((g.height / 2) - (bulletSize / 2)), bulletSize, xBulletSpeed, bulletRight);
                    gremlinBullets.Add(newBullet);
                }
                else if (direction == 2)
                {
                    bulletRight = false;
                    Bullet newBullet = new Bullet(g.x + ((g.width / 2) - (bulletSize / 2)), g.y + ((g.height / 2) - (bulletSize / 2)), bulletSize, xBulletSpeed, bulletRight);
                    gremlinBullets.Add(newBullet);
                }
                else if (direction == 3)
                {
                    bulletUp = true;
                    Bullet newBullet = new Bullet(g.x + ((g.width / 2) - (bulletSize / 2)), g.y + ((g.height / 2) - (bulletSize / 2)), bulletSize, yBulletSpeed, bulletUp);
                    gremlinBullets.Add(newBullet);
                }
                else if (direction == 4)
                {
                    bulletUp = false;
                    Bullet newBullet = new Bullet(g.x + ((g.width / 2) - (bulletSize / 2)), g.y + ((g.height / 2) - (bulletSize / 2)), bulletSize, yBulletSpeed, bulletUp);
                    gremlinBullets.Add(newBullet);
                }
            }
        }

        public void GremlinBulletsWallsCollision()
        {
            //will contain index values of all bullets that have collided with a wall 
            List<int> bulletsToRemove = new List<int>();

            foreach (Bullet b in gremlinBullets)
            {
                foreach (Wall w in walls)
                {
                    //uses collision method in wall class 
                    if (w.BulletCollision(b))
                    {
                        //checks to ensure that the bullet is not already in removal list 
                        if (!bulletsToRemove.Contains(gremlinBullets.IndexOf(b)))
                        {
                            //add the index value from bullets of the bullet that collided  
                            bulletsToRemove.Add(gremlinBullets.IndexOf(b));
                        }
                    }
                }
            }
            //reverse list so when removing you do so from the end of the list first 
            bulletsToRemove.Reverse();

            foreach (int i in bulletsToRemove)
            {
                gremlinBullets.RemoveAt(i);
            }
        }

        public void PlayerBulletsWallsCollision()
        {
            //will contain index values of all bullets that have collided with a wall 
            List<int> bulletsToRemove = new List<int>();

            foreach (Bullet b in playerBullets)
            {
                foreach (Wall w in walls)
                {
                    //uses collision method in wall class 
                    if (w.BulletCollision(b))
                    {
                        //checks to ensure that the bullet is not already in removal list 
                        if (!bulletsToRemove.Contains(playerBullets.IndexOf(b)))
                        {
                            //add the index value from bullets of the bullet that collided  
                            bulletsToRemove.Add(playerBullets.IndexOf(b));
                        }
                    }
                }
            }
            //reverse list so when removing you do so from the end of the list first 
            bulletsToRemove.Reverse();

            foreach (int i in bulletsToRemove)
            {
                playerBullets.RemoveAt(i);
            }
        }

        public void BulletsGremlinsCollision()
        {
            //will contain index values of all bullets that have collided with a gremlin 
            List<int> bulletsToRemove = new List<int>();

            //will contain index values of all gremlins that have collided with a bullet 
            List<int> gremlinsToRemove = new List<int>();

            foreach (Bullet b in playerBullets)
            {
                foreach (Gremlin g in gremlins)
                {
                    //uses collision method in gremlin class 
                    if (g.BulletCollision(b))
                    {
                        gremlinSound.Play();
                        g.health--;
                        gremlinHit = true;

                        //checks to ensure that the bullet is not already in removal list 
                        if (!bulletsToRemove.Contains(playerBullets.IndexOf(b)))
                        {
                            //add the index value from bullets of the bullet that collided  
                            bulletsToRemove.Add(playerBullets.IndexOf(b));
                        }

                        //checks to ensure that the gremlin is not already in removal list 
                        if (!gremlinsToRemove.Contains(gremlins.IndexOf(g)))
                        {
                            //add index value from gremlins of the gremlin that collided 
                            if (g.health == 0)
                            {
                                gremlinsToRemove.Add(gremlins.IndexOf(g));
                            }
                        }
                    }
                }
            }
            //reverse lists so when removing you do so from the end of the list first 
            bulletsToRemove.Reverse();
            gremlinsToRemove.Reverse();

            foreach (int i in bulletsToRemove)
            {
                playerBullets.RemoveAt(i);
            }

            foreach (int i in gremlinsToRemove)
            {
                gremlins.RemoveAt(i);
            }
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                // player moving button presses
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
                // shooting button presses
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
                default:
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                // player moving button releases
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
                // shooting button releases
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
                    playerSound.Play();                 
                    playerLives--;
                    healthLabel.Text = "Lives: " + playerLives;
                    gremlinBullets.Remove(b);
                    playerHit = true;

                    if (playerLives == 0) // go the game over screen if player dies
                    {
                        win = false;
                        gameTimer.Enabled = false;
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
            BulletsGremlinsCollision();

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

            #endregion

            #region bullet to wall collisions

            // check if any bullets have hit walls
            GremlinBulletsWallsCollision();
            PlayerBulletsWallsCollision();

            #endregion

            #region beat level

            // check if player has reached the end of the level
            if (player.x > this.Width - player.width && level < 5)
            {
                // go to next level and change label
                level++;
                levelLabel.Text = "Level: " + level;

                // player starting position
                player.x = 35;
                player.y = (this.Height / 2) - 25;

                // clear bullets, gremlins and walls still on screen from previous level
                gremlins.Clear();
                playerBullets.Clear();
                gremlinBullets.Clear();
                walls.Clear();

                MakeGremlin(); // make gremlins for next level
                LevelMaker(); // make next level
            }
            else if (player.x > this.Width - player.width && level == 5) // if player was on final level
            {
                gameTimer.Enabled = false;
                win = true;
                OnEnd(); // go to game over screen
            }

            #endregion

            // redraw the screen
            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            // draws walls for the current level
            foreach (Wall w in walls)
            {
                e.Graphics.FillRectangle(wallBrush, w.x, w.y, w.width, w.height);
            }

            // draws player image to screen
            e.Graphics.DrawImage(playerImage, player.x, player.y, player.width, player.height);

            // player getting hit animation
            if (playerHit == true)
            {
                for (int i = 0; i < 2; i++)
                { 
                    e.Graphics.FillRectangle(pHitBrush, player.x, player.y, player.width, player.height);
                }

                playerHit = false;
            }

            // draws gremlin image to screen
            foreach (Gremlin g in gremlins)
            {
                if (g.health != 0)
                {
                    e.Graphics.DrawImage(gremlinImage, g.x, g.y, g.width, g.height);
                }

                // gremlin getting hit animation
                if (gremlinHit == true)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        e.Graphics.FillRectangle(gHitBrush, g.x, g.y, g.width, g.height);
                    }

                    gremlinHit = false;
                }
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
        }

        public void OnEnd()
        {
            // clear bullets, gremlins and walls still on screen from previous level
            gremlins.Clear();
            playerBullets.Clear();
            gremlinBullets.Clear();
            walls.Clear();

            // Goes to the game over screen
            Form form = this.FindForm();
            GameOverScreen go = new GameOverScreen();

            go.Location = new Point((form.Width - go.Width) / 2, (form.Height - go.Height) / 2);

            form.Controls.Add(go);
            form.Controls.Remove(this);
        }
    }
}
