namespace MazeRunner
{
    partial class GameOverScreen
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.playAgainButton = new System.Windows.Forms.Button();
            this.menuButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // playAgainButton
            // 
            this.playAgainButton.BackColor = System.Drawing.Color.Black;
            this.playAgainButton.FlatAppearance.BorderColor = System.Drawing.Color.LimeGreen;
            this.playAgainButton.FlatAppearance.BorderSize = 3;
            this.playAgainButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.playAgainButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lime;
            this.playAgainButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playAgainButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playAgainButton.ForeColor = System.Drawing.Color.LimeGreen;
            this.playAgainButton.Location = new System.Drawing.Point(326, 415);
            this.playAgainButton.Name = "playAgainButton";
            this.playAgainButton.Size = new System.Drawing.Size(145, 60);
            this.playAgainButton.TabIndex = 0;
            this.playAgainButton.Text = "Play Again";
            this.playAgainButton.UseVisualStyleBackColor = false;
            this.playAgainButton.Click += new System.EventHandler(this.playAgainButton_Click);
            // 
            // menuButton
            // 
            this.menuButton.BackColor = System.Drawing.Color.Black;
            this.menuButton.FlatAppearance.BorderColor = System.Drawing.Color.LimeGreen;
            this.menuButton.FlatAppearance.BorderSize = 3;
            this.menuButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.menuButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lime;
            this.menuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menuButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuButton.ForeColor = System.Drawing.Color.LimeGreen;
            this.menuButton.Location = new System.Drawing.Point(548, 415);
            this.menuButton.Name = "menuButton";
            this.menuButton.Size = new System.Drawing.Size(145, 60);
            this.menuButton.TabIndex = 1;
            this.menuButton.Text = "Main Menu";
            this.menuButton.UseVisualStyleBackColor = false;
            this.menuButton.Click += new System.EventHandler(this.menuButton_Click);
            // 
            // GameOverScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.menuButton);
            this.Controls.Add(this.playAgainButton);
            this.DoubleBuffered = true;
            this.Name = "GameOverScreen";
            this.Size = new System.Drawing.Size(1000, 530);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button playAgainButton;
        private System.Windows.Forms.Button menuButton;
    }
}
