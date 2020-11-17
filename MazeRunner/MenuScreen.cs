using System;
using System.Drawing;
using System.Windows.Forms;

namespace MazeRunner
{
    public partial class MenuScreen : UserControl
    {
        public MenuScreen()
        {
            InitializeComponent();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            // Goes to the game screen
            GameScreen gs = new GameScreen();
            Form form = this.FindForm();

            form.Controls.Add(gs);
            form.Controls.Remove(this);
            gs.Focus();

            gs.Location = new Point((form.Width - gs.Width) / 2, (form.Height - gs.Height) / 2);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit(); // exit game
        }
    }
}
