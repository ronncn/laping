using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laping
{
    public partial class Form1 : Form
    {
        private Bitmap bitmap; Graphics g;
        private Bitmap bitmap1; Graphics g1;
        private Bitmap bitmap2; Graphics g2;
        private Graphics gShow;
        int pointer;//指针
        int cursor = 1;//游标
        int bitmapLock;//画布锁

        List<byte> list;

        public Form1()
        {
            InitializeComponent();
            list = new List<byte>();
            bitmap = new Bitmap(this.Width, this.Height);
            bitmap1 = (Bitmap)bitmap.Clone();
            bitmap2 = (Bitmap)bitmap1.Clone();
            g = Graphics.FromImage(bitmap);
            g1 = Graphics.FromImage(bitmap1);
            g2 = Graphics.FromImage(bitmap2);
            gShow = this.CreateGraphics();
            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.Clear(this.BackColor);
            switch (pointer)
            {
                case 1:
                    e.Graphics.DrawImage(bitmap1, 0, 0);
                    break;
                case 2:
                    e.Graphics.DrawImage(bitmap2, 0, 0);
                    break;
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
            //timer1.Enabled = !timer1.Enabled;
        }
        private void DrawLaping1()
        {
            Graphics g = Graphics.FromImage(bitmap1);
            Bitmap bmp = (Bitmap)bitmap2.Clone();
            g.Clear(Color.Transparent);
            g.DrawImage(bmp, -10, 0);
            g.DrawLine(Pens.Black, new Point(this.Width - 10, this.Height / 2), new Point(this.Width, this.Height / 2));
            bmp.Dispose();
            g.Dispose();
        }
        private void DrawLaping2()
        {
            Graphics g = Graphics.FromImage(bitmap2);
            Bitmap bmp = (Bitmap)bitmap1.Clone();
            g.Clear(Color.Transparent);
            g.DrawImage(bmp, -10, 0);
            g.DrawLine(Pens.Red, new Point(this.Width - 10, this.Height / 2 + 10), new Point(this.Width, this.Height / 2 + 10));
            bmp.Dispose();
            g.Dispose();
        }

        private void Render(byte b)
        {
            if(list.Count == 0)
            {
                list.Add(b);
                return;
            }
            Bitmap map = (Bitmap)bitmap.Clone();
            g.Clear(Color.Transparent);
            g.DrawImage(map, -10, 0);
            map.Dispose();
            g.DrawLine(Pens.Black, bitmap.Width - 10, bitmap.Height / 2 + list[list.Count - 1],
                bitmap.Width - 10, bitmap.Height / 2 + b);
            g.DrawLine(Pens.Red, bitmap.Width - 10, bitmap.Height / 2 + b, bitmap.Width, bitmap.Height / 2 + b);
            list.Add(b);
        }
        byte a = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //DrawLaping1();
            //DrawLaping2();
            //base.Invalidate();
            Render(a);
            if (a == 0) { a = 100; } else { a = 0; }
            switch (cursor)
            {
                case 1:
                    bitmapLock = 1;
                    g1.Clear(Color.Transparent);
                    g1.DrawImage(bitmap, 0, 0);
                    pointer = 1;
                    cursor = 2;
                    break;
                case 2:
                    bitmapLock = 2;
                    g2.Clear(Color.Transparent);
                    g2.DrawImage(bitmap, 0, 0);
                    pointer = 2;
                    cursor = 1;
                    break;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (pointer == 0)
                return;
            base.Invalidate();
        }
    }
}
