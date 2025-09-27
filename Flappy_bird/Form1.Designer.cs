namespace Flappy_bird
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            bird1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)bird1).BeginInit();
            SuspendLayout();
            // 
            // bird1
            // 
            bird1.BackColor = Color.Transparent;
            bird1.Image = Properties.Resources.bird2;
            bird1.Location = new Point(12, 167);
            bird1.Name = "bird1";
            bird1.Size = new Size(56, 50);
            bird1.SizeMode = PictureBoxSizeMode.StretchImage;
            bird1.TabIndex = 0;
            bird1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.background;
            ClientSize = new Size(800, 450);
            Controls.Add(bird1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)bird1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox bird1;
    }
}
