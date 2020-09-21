using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApp21
{
    public partial class Form1 : Form
    {
        Ball[] redballs = new Ball[2];
        Ball[] blueballs = new Ball[2];
        public Form1()
        {
            InitializeComponent();
            redballs[0] = new Ball(new Point(0, 100), "Right", false, Brushes.Red);
            redballs[1] = new Ball(new Point(0, 300), "Right", false, Brushes.Red);

            blueballs[0] = new Ball(new Point(1085, 200), "Left", false, Brushes.Blue);
            blueballs[1] = new Ball(new Point(1085, 400), "Left", false, Brushes.Blue);

           redballs[0].StartThr();

            redballs[0].stopm += blueballs[0].StartThr;
            blueballs[0].stopm += redballs[1].StartThr;
            redballs[1].stopm += blueballs[1].StartThr;
            blueballs[1].stopm += Finish;
        }
   
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            Invalidate();
        }
        private void стартToolStripMenuItem_Click(object sender, EventArgs timer1_Tick)
        {
            timer1.Start();
        }

        private void стопToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
        public void Finish()
        {
            MessageBox.Show(
        "Финиш");
        }


        class Ball
        {
            public Point position;
            Brush color;
            Size size;
            bool stop;
            public delegate void StopDelegate();
            string direction;
            public event StopDelegate stopm;
            public Thread Moving;
            

            public Ball(Point beginposition, string Direction, bool Stop, Brush Color)
            {
                this.color = Color;
                this.size = new Size(100, 100);
                this.position = beginposition;
                this.direction = Direction;
                this.stop = Stop;
                this.Moving = new Thread(new ThreadStart (Move));
                this.stop = true;
            }
            public void draw(Graphics context)
            {
                context.FillEllipse(this.color, new Rectangle(this.position, this.size));
            }
        

    public void Move()
            {
                if (this.stop == true)
                {
                    while (this.direction == "Right")
                    {
                        Thread.Sleep(100);
                        this.position.X += 10;
                        if (this.position.X == 1090)
                        {
                            this.StopMove();
                            this.stopm();
                            this.Moving.Abort();
                        }
                    }


                    while (this.direction == "Left")
                    {
                        Thread.Sleep(100);
                        this.position.X -= 10;

                        if (this.position.X <= 30)
                        {

                            this.StopMove();
                            this.stopm();
                            this.Moving.Abort();
                        }
                    }
     
                }
                

            }
            public void StopMove()
            {
                this.stop = false;
          
            }
            public void StartThr()
            {
                this.Moving.Start();
            }
 


        }

        private void информацияОРазработчикеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Выполнил: студент группы 4207 Антаев М.П.\nВариант: №3", "Информация о разработчике");
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            redballs[0].draw(e.Graphics);
            redballs[1].draw(e.Graphics);
            blueballs[0].draw(e.Graphics);
            blueballs[1].draw(e.Graphics);
        }

        private void зановоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
