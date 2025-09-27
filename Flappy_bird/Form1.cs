using System;
using System.Drawing;
using System.Windows.Forms;

namespace Flappy_bird
{
    public partial class Form1 : Form
    {
        int pipeSpeed = 8;
        int gravity = 5;
        int score = 0;
        Random rand = new Random();

        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Timer gameTimer;

        // Oyun bitti popup panel ve label
        private Panel gameOverPanel;
        private Label gameOverLabel;

        // 3 çift pipe için PictureBox referanslarý
        private PictureBox[] pipeTops;
        private PictureBox[] pipeBottoms;
        private int pipeCount = 3;
        private int pipeGap = 150;
        private int pipeDistance = 250;

        private bool isGameOver = false; // Sýnýf deðiþkenlerine ekleyin

        public Form1()
        {
            InitializeComponent();
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;

            this.DoubleBuffered = true;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            // scoreLabel
            this.scoreLabel = new System.Windows.Forms.Label();
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Text = "Skor: 0";
            this.scoreLabel.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.scoreLabel.Location = new System.Drawing.Point(10, 10);
            this.scoreLabel.BackColor = Color.Transparent;
            this.Controls.Add(this.scoreLabel);

            // Oyun bitti paneli ve label'ý
            this.gameOverPanel = new Panel();
            this.gameOverPanel.Size = new Size(300, 160); // Yüksekliði 160 yapýldý
            this.gameOverPanel.BackColor = Color.FromArgb(220, 30, 30, 30);
            this.gameOverPanel.Location = new Point((this.ClientSize.Width - 300) / 2, (this.ClientSize.Height - 160) / 2);
            this.gameOverPanel.Visible = false;
            this.gameOverPanel.Anchor = AnchorStyles.None;

            this.gameOverLabel = new Label();
            this.gameOverLabel.AutoSize = false;
            this.gameOverLabel.Size = new Size(280, 60); // Yüksekliði 60 yapýldý
            this.gameOverLabel.Location = new Point(10, 10);
            this.gameOverLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.gameOverLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            this.gameOverLabel.ForeColor = Color.White;
            this.gameOverPanel.Controls.Add(this.gameOverLabel);

            // Restart butonu
            Button restartButton = new Button();
            restartButton.Text = "Yeniden Baþlat";
            restartButton.Size = new Size(180, 35);
            restartButton.Location = new Point(60, 80); // Y ekseni 80'e çekildi
            restartButton.Font = new Font("Arial", 12, FontStyle.Bold);
            restartButton.Click += RestartButton_Click;
            gameOverPanel.Controls.Add(restartButton);

            this.Controls.Add(this.gameOverPanel);

            // 3 çift pipe oluþtur
            pipeTops = new PictureBox[pipeCount];
            pipeBottoms = new PictureBox[pipeCount];
            for (int i = 0; i < pipeCount; i++)
            {
                pipeTops[i] = new PictureBox();
                pipeTops[i].Width = 60;
                pipeTops[i].Left = 400 + i * pipeDistance;
                pipeTops[i].Top = 0;
                pipeTops[i].Image = Properties.Resources.pipeTop;
                pipeTops[i].SizeMode = PictureBoxSizeMode.StretchImage;
                pipeTops[i].BackColor = Color.Transparent;
                this.Controls.Add(pipeTops[i]);

                pipeBottoms[i] = new PictureBox();
                pipeBottoms[i].Width = 60;
                pipeBottoms[i].Left = pipeTops[i].Left;
                pipeBottoms[i].Image = Properties.Resources.pipeBottom;
                pipeBottoms[i].SizeMode = PictureBoxSizeMode.StretchImage;
                pipeBottoms[i].BackColor = Color.Transparent;
                this.Controls.Add(pipeBottoms[i]);
            }

            // Ýlk konumlandýrma
            for (int i = 0; i < pipeCount; i++)
            {
                SetPipePair(pipeTops[i], pipeBottoms[i]);
            }

            // gameTimer
            this.gameTimer = new System.Windows.Forms.Timer();
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            this.gameTimer.Interval = 25;
            this.gameTimer.Enabled = true;
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (!isGameOver) // Oyun bitmediyse devam et
            {
                bird1.Top += gravity;

                for (int i = 0; i < pipeCount; i++)
                {
                    MovePipe(pipeTops[i], pipeBottoms[i]);
                }

                scoreLabel.Text = "Skor: " + score;

                for (int i = 0; i < pipeCount; i++)
                {
                    if (bird1.Bounds.IntersectsWith(pipeTops[i].Bounds) ||
                        bird1.Bounds.IntersectsWith(pipeBottoms[i].Bounds))
                    {
                        GameOver();
                    }
                }

                if (bird1.Top < 0 || bird1.Bottom > this.ClientSize.Height)
                {
                    GameOver();
                }
            }
        }

        private void MovePipe(PictureBox topPipe, PictureBox bottomPipe)
        {
            topPipe.Left -= pipeSpeed;
            bottomPipe.Left -= pipeSpeed;

            if (topPipe.Right < 0)
            {
                topPipe.Left = this.ClientSize.Width;
                bottomPipe.Left = this.ClientSize.Width;
                SetPipePair(topPipe, bottomPipe);
                score++;
            }
        }

        // Pipe çiftini kurallara uygun þekilde konumlandýrýr
        private void SetPipePair(PictureBox topPipe, PictureBox bottomPipe)
        {
            int minTopHeight = 50;
            int maxTopHeight = this.ClientSize.Height - pipeGap - 50;
            int pipeTopHeight = rand.Next(minTopHeight, maxTopHeight);

            topPipe.Height = pipeTopHeight;
            topPipe.Top = 0;

            bottomPipe.Top = pipeTopHeight + pipeGap;
            bottomPipe.Height = this.ClientSize.Height - (pipeTopHeight + pipeGap);
        }

        // GameOver metodunu güncelleyin
        private void GameOver()
        {
            gameTimer.Stop();
            gameOverLabel.Text = $"Öldünüz!\nSkor: {score}";
            gameOverPanel.Visible = true;
            gameOverPanel.BringToFront();
            isGameOver = true;
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

        // RestartGame metodunu güncelleyin
        private void RestartGame()
        {
            score = 0;
            scoreLabel.Text = "Skor: 0";
            gravity = 5;

            bird1.Top = this.ClientSize.Height / 2;
            bird1.Left = 100;

            for (int i = 0; i < pipeCount; i++)
            {
                pipeTops[i].Left = 400 + i * pipeDistance;
                pipeBottoms[i].Left = pipeTops[i].Left;
                SetPipePair(pipeTops[i], pipeBottoms[i]);
            }

            gameOverPanel.Visible = false;
            isGameOver = false;
            gameTimer.Start();

            this.ActiveControl = null; // ODAK hiçbir kontrolde olmasýn
            this.Focus();              // Form'a odaklan
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !isGameOver)
            {
                gravity = -25;
            }
        }

        // Form1_KeyUp metodunu güncelleyin
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !isGameOver)
            {
                gravity = 5;
            }
        }
    }
}
