using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        private List<Circle> Snake = new List<Circle>();
        private Circle food = new Circle();
        public Form1()
        {
            InitializeComponent();
            new Settings();
            gameTimer.Interval = 1800 / Settings.Speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();
            StartGame();

        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            if (Settings.GameOver == true)
            {
                if (Inputs.keyPressed(Keys.Enter))
                {
                    StartGame();
                }
            }
            else
            {
                if (Inputs.keyPressed(Keys.Right) && Settings.direction != Settings.Directions.Left)
                    Settings.direction = Settings.Directions.Right;
                else if (Inputs.keyPressed(Keys.Left) && Settings.direction != Settings.Directions.Right)
                    Settings.direction = Settings.Directions.Left;
                else if (Inputs.keyPressed(Keys.Up) && Settings.direction != Settings.Directions.Down)
                    Settings.direction = Settings.Directions.Up;
                else if (Inputs.keyPressed(Keys.Down) && Settings.direction != Settings.Directions.Up)
                    Settings.direction = Settings.Directions.Down;
                MovePlayer();
            }
            pbCanvas.Invalidate();
        }

        private void StartGame()
        {
            lblGameOver.Visible = false;
            new Settings();
            Snake.Clear();
            Circle head = new Circle();
            head.X = 10;
            head.Y = 5;
            Snake.Add(head);

            lblScore.Text = Settings.Score.ToString();
            GenerateFood();
        }

        private void GenerateFood()
        {
            int maxXPos = pbCanvas.Size.Width / Settings.Width;
            int maxYPos = pbCanvas.Size.Height / Settings.Height;

            Random random = new Random();
            food = new Circle();
            food.X = random.Next(0, maxXPos);
            food.Y = random.Next(0, maxYPos);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
        private void MovePlayer()
        {
            for (int i=Snake.Count - 1; i >= 0; i--){
                if (i == 0)
                {
                    switch (Settings.direction)
                    {
                        case Settings.Directions.Right:
                        Snake[i].X++;
                             break;
                        case Settings.Directions.Left:
                            Snake[i].X--;
                            break;
                        case Settings.Directions.Up:
                            Snake[i].Y--;
                            break;
                        case Settings.Directions.Down:
                            Snake[i].Y++;
                            break;
                    }
                    int maxXPos = pbCanvas.Size.Width / Settings.Width;
                    int maxYPos = pbCanvas.Size.Height / Settings.Height;
                    //detect collision
                    if(Snake[i].X<0 || Snake[i].Y<0 || Snake[i].X>=maxXPos || Snake[i].Y >= maxYPos)
                    {
                        Die();
                    }
                    //detect collision with body
                    for(int j = 1; j < Snake.Count; j++)
                    {
                        if(Snake[i].X==Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            Die();
                        }
                    }
                    //detect collision with food
                    if(Snake[0].X==food.X && Snake[0].Y == food.Y)
                    {
                        Eat();
                    }

                }
                else
                {
                    Snake[i].X = Snake[i- 1].X;
                    Snake[i].Y = Snake[i - 1].Y;

                }

            }
        }
        private void Die()
        {
            Settings.GameOver = true;
        }
        private void Eat()
        {
            Circle food = new Circle();
            food.X = Snake[Snake.Count - 1].X;
            food.Y = Snake[Snake.Count - 1].Y;

            Snake.Add(food);
            //update score
            Settings.Score += Settings.Points;
            lblScore.Text = Settings.Score.ToString();
            GenerateFood();

        }
        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            if (!Settings.GameOver)
            {
                Brush snakeColor;
                for (int i = 0; i < Snake.Count; i++)
                {
                    if (i == 0)
                        snakeColor = Brushes.Black;//draw head
                    else
                        snakeColor = Brushes.Green;//rest of body

                    //draw snake
                    canvas.FillEllipse(snakeColor, new Rectangle(Snake[i].X * Settings.Width, Snake[i].Y * Settings.Height, Settings.Width, Settings.Height));
                    //draw food
                    canvas.FillEllipse(Brushes.Red,
                        new RectangleF(food.X * Settings.Width, food.Y * Settings.Height, Settings.Width, Settings.Height));

                }
            }
            else
            {
                string gameOver="Game Over\nYour final Score is:"+Settings.Score+"\nPress Enter to try again";
                lblGameOver.Text = gameOver;
                lblGameOver.Visible = true;

            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Inputs.ChangeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Inputs.ChangeState(e.KeyCode, false);
        }

        private void lblGameOver_Click(object sender, EventArgs e)
        {

        }
    }
}
