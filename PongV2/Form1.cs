using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PongV2
{
    public partial class Pong : Form
    {
        // Location Variables
        int ballXCoordinate = 10;
        int ballYCoordinate = 5;
        // Score Variables
        int playerScore = 0;
        int cpuScore = 0;
        // Size Variables
        int bottomBoundary;
        int centerPoint;
        int xMidpoint;
        int yMidpoint;
        // Detection variables
        bool playerDetectedUp;
        bool playerDetectedDown;
        // Special Keys
        int spaceBarClicked = 0;

        // Rectangles for the player, CPU and ball
        Rectangle player1;
        Rectangle cpuPlayer;
        Rectangle pongBall;

        // Transform positions
        int player1Y;
        int cpuPlayerY;
        int pongBallX;
        int pongBallY;

        // Current ball color
        Color ballColor = Color.Black;
        SolidBrush ballBrush;

        public Pong()
        {
            InitializeComponent();
            bottomBoundary = ClientSize.Height - 150; // Adjust as per player1's height
            xMidpoint = ClientSize.Width / 2;
            yMidpoint = ClientSize.Height / 2;

            // Initialize rectangles with larger dimensions
            player1 = new Rectangle(50, yMidpoint - 75, 20, 200);  // Width: 20, Height: 200
            cpuPlayer = new Rectangle(ClientSize.Width - 70, yMidpoint - 75, 20, 200);  // Width: 20, Height: 200
            pongBall = new Rectangle(xMidpoint, yMidpoint, 25, 25);  // Width: 25, Height: 25

            // Initial positions
            player1Y = player1.Y;
            cpuPlayerY = cpuPlayer.Y;
            pongBallX = pongBall.X;
            pongBallY = pongBall.Y;

            // Initialize ball brush
            ballBrush = new SolidBrush(ballColor);
        }

        private void Pong_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random newBallSpot = new Random();
            int newSpot = newBallSpot.Next(100, ClientSize.Height - 100);
            // Adjust where the ball is
            pongBallY -= ballYCoordinate;
            pongBallX -= ballXCoordinate;

            // CPU Movement
            Random cpuDirectionRandomizer = new Random();
            int cpuDirection = cpuDirectionRandomizer.Next(0, 7);
            if ((cpuPlayerY + cpuPlayer.Height / 2 < pongBallY + pongBall.Height / 2) && (cpuPlayerY + cpuDirection > 0) && (cpuPlayerY + cpuDirection < bottomBoundary))
            {
                cpuPlayerY += cpuDirection;
            }
            else if ((cpuPlayerY + cpuPlayer.Height / 2 > pongBallY + pongBall.Height / 2) && (cpuPlayerY - cpuDirection > 0) && (cpuPlayerY - cpuDirection < bottomBoundary))
            {
                cpuPlayerY -= cpuDirection;
            }

            // Check if the ball has exited the left side of the screen
            if (pongBallX <= 0)
            {
                pongBallX = xMidpoint;
                pongBallY = newSpot;
                ballXCoordinate = -ballXCoordinate;
                cpuScore++;
                cpuScoreLabel.Text = cpuScore.ToString();
            }

            // Check if the ball has exited the right side of the screen
            if (pongBallX + pongBall.Width >= ClientSize.Width)
            {
                pongBallX = xMidpoint;
                pongBallY = newSpot;
                ballXCoordinate = -ballXCoordinate;
                playerScore++;
                playerScoreLabel.Text = playerScore.ToString();
            }

            // Ensure the ball is within the boundaries of the screen
            if (pongBallY <= 0 || pongBallY + pongBall.Height >= ClientSize.Height)
            {
                ballYCoordinate = -ballYCoordinate;
            }

            // Check if the ball hits the player or CPU paddle
            Rectangle pongR = new Rectangle(pongBallX, pongBallY, pongBall.Width, pongBall.Height);
            Rectangle player = new Rectangle(player1.X, player1Y, player1.Width, player1.Height);
            Rectangle cpu = new Rectangle(cpuPlayer.X, cpuPlayerY, cpuPlayer.Width, cpuPlayer.Height);
            if (pongR.IntersectsWith(player))
            {
                ballXCoordinate = -ballXCoordinate;
                pongBallX = player1.Right;
                ballColor = GenerateRandomColor();
                ballBrush = new SolidBrush(ballColor);
            }
            else if (pongR.IntersectsWith(cpu))
            {
                ballXCoordinate = -ballXCoordinate;
                pongBallX = cpuPlayer.Left - pongBall.Width;
                ballColor = GenerateRandomColor();
                ballBrush = new SolidBrush(ballColor);
            }

            // Move player up
            if (playerDetectedUp && player1Y > 0)
            {
                player1Y -= 10;
            }

            // Move player down
            if (playerDetectedDown && player1Y < bottomBoundary)
            {
                player1Y += 10;
            }

            // Update player and CPU positions
            player1.Y = player1Y;
            cpuPlayer.Y = cpuPlayerY;

            // Update pongBall position
            pongBall.X = pongBallX;
            pongBall.Y = pongBallY;

            // Check for winner
            if (playerScore == 10)
            {
                timer1.Stop();
                MessageBox.Show("You won!");
            }

            if (cpuScore == 10)
            {
                timer1.Stop();
                MessageBox.Show("You lost!");
            }

            // Refresh the form to redraw the rectangles
            this.Invalidate();
        }

        private Color GenerateRandomColor()
        {
            Random random = new Random();
            return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }

        private void Pong_KeyUp(object sender, KeyEventArgs e)
        {
            // if player releases the up key, stop moving player up
            if (e.KeyCode == Keys.Up)
            {
                playerDetectedUp = false;
            }
            // if player releases the down key, stop moving player down
            if (e.KeyCode == Keys.Down)
            {
                playerDetectedDown = false;
            }
        }

        private void Pong_KeyDown(object sender, KeyEventArgs e)
        {
            // if player presses the up key, move player up
            if (e.KeyCode == Keys.Up)
            {
                playerDetectedUp = true;
            }
            // if player presses the down key, move player down
            if (e.KeyCode == Keys.Down)
            {
                playerDetectedDown = true;
            }

            // if player presses the space bar, pause the game
            if (e.KeyCode == Keys.Space)
            {
                spaceBarClicked++;
                if (spaceBarClicked % 2 == 0)
                {
                    timer1.Start();
                }
                else
                {
                    timer1.Stop();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Create a transformation matrix for the player1 paddle
            Matrix playerTransform = new Matrix();
            playerTransform.Translate(player1.X, player1.Y);
            e.Graphics.Transform = playerTransform;
            e.Graphics.FillRectangle(Brushes.Blue, 0, 0, player1.Width, player1.Height);

            // Reset transformation and create a transformation matrix for the cpuPlayer paddle
            e.Graphics.ResetTransform();
            Matrix cpuTransform = new Matrix();
            cpuTransform.Translate(cpuPlayer.X, cpuPlayer.Y);
            e.Graphics.Transform = cpuTransform;
            e.Graphics.FillRectangle(Brushes.Red, 0, 0, cpuPlayer.Width, cpuPlayer.Height);

            // Reset transformation and create a transformation matrix for the pongBall
            e.Graphics.ResetTransform();
            Matrix ballTransform = new Matrix();
            ballTransform.Translate(pongBall.X, pongBall.Y);
            e.Graphics.Transform = ballTransform;

            // Draw pongBall with the current brush color
            e.Graphics.FillEllipse(ballBrush, 0, 0, pongBall.Width, pongBall.Height);
        }
    }
}
