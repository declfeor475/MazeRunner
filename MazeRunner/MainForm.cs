/// Created by Declan Feore and James Koch
/// November 2020
/// A simple dungeon crawler game where the player has to navigate through the levels and take out any gremlins along their way to victory

using System;
using System.Drawing;
using System.Windows.Forms;

namespace MazeRunner
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Start the program centred on the Menu Screen
            MenuScreen ms = new MenuScreen();
            this.Controls.Add(ms);

            ms.Location = new Point((this.Width - ms.Width) / 2, (this.Height - ms.Height) / 2);
        }
    }
}
