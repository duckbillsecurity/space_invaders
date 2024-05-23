using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceInvaders
{
    public partial class GameForm : Form
    {
        private GameLogic game;

        public GameForm()
        {
            InitializeComponent();
            game = new GameLogic(this);
            this.Paint += new PaintEventHandler(OnPaint);
            this.KeyDown += new KeyEventHandler(OnKeyDown);
            Timer timer = new Timer();
            timer.Interval = 20;
            timer.Tick += new EventHandler(OnTimerTick);
            timer.Start();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            game.Draw(e.Graphics);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            game.OnKeyDown(e.KeyCode);
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            game.Update();
            this.Invalidate();
        }
    }
}
