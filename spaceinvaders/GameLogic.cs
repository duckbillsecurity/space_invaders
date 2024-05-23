using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceInvaders
{
    public class GameLogic
    {
        private Form form;
        private Rectangle player;
        private Rectangle[] enemies;
        private Rectangle bullet;
        private bool bulletFired;
        private int score;
        private Random random;

        public GameLogic(Form form)
        {
            this.form = form;
            player = new Rectangle(375, 500, 50, 20);
            enemies = new Rectangle[5];
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = new Rectangle(50 + i * 150, 50, 40, 20);
            }
            bullet = new Rectangle(0, 0, 5, 10);
            bulletFired = false;
            score = 0;
            random = new Random();
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.Green, player);
            foreach (Rectangle enemy in enemies)
            {
                g.FillRectangle(Brushes.Red, enemy);
            }
            if (bulletFired)
            {
                g.FillRectangle(Brushes.White, bullet);
            }
            g.DrawString($"Score: {score}", new Font("Arial", 16), Brushes.White, 10, 10);
        }

        public void OnKeyDown(Keys key)
        {
            if (key == Keys.Left && player.Left > 0)
            {
                player.X -= 10;
            }
            if (key == Keys.Right && player.Right < form.ClientSize.Width)
            {
                player.X += 10;
            }
            if (key == Keys.Space && !bulletFired)
            {
                bullet.X = player.X + player.Width / 2;
                bullet.Y = player.Y;
                bulletFired = true;
            }
        }

        public void Update()
        {
            if (bulletFired)
            {
                bullet.Y -= 10;
                if (bullet.Y < 0)
                {
                    bulletFired = false;
                }

                for (int i = 0; i < enemies.Length; i++)
                {
                    if (bullet.IntersectsWith(enemies[i]))
                    {
                        score += 10;
                        enemies[i] = new Rectangle(random.Next(form.ClientSize.Width - 40), 50, 40, 20);
                        bulletFired = false;
                        break;
                    }
                }
            }

            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].IntersectsWith(player))
                {
                    MessageBox.Show("Game Over! Your score is " + score);
                    Application.Exit();
                }
            }
        }
    }
}
