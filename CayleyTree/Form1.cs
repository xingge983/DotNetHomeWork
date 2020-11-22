using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L4_CayleyTree
{
    public partial class Form1 : Form
    {

        private Graphics graphics;

        public int N { get; set; }
        public double Length { get; set; }
        public double LeftPer { get; set; }
        public double RightPer { get; set; }
        public int LeftDegree { get; set; }
        public int RightDegree { get; set; }
        BindingSource PenColor = new BindingSource();

        public Form1()
        {
            InitializeComponent();
            N = 10;
            Length = 150;
            LeftDegree = 20;
            RightDegree = 40;
            LeftPer = 0.6;
            RightPer = 0.7;
            PenColor.Add(new { Name = "Black", Value = Pens.Black });
            PenColor.Add(new { Name = "Blue", Value = Pens.Blue });
            PenColor.Add(new { Name = "Gold", Value = Pens.Gold });
            PenColor.Add(new { Name = "LightPink", Value = Pens.LightPink });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.DataBindings.Add("Text", this, "N");
            textBox2.DataBindings.Add("Text", this, "Length");
            textBox3.DataBindings.Add("Text", this, "LeftPer");
            textBox4.DataBindings.Add("Text", this, "RightPer");
            textBox5.DataBindings.Add("Text", this, "LeftDegree");
            textBox6.DataBindings.Add("Text", this, "RightDegree");

            comboBox1.DataSource = PenColor;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Value";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(graphics==null)
            {
                graphics = this.panel1.CreateGraphics();
            }
            graphics.Clear(panel1.BackColor);
            DrawCayleyTree(N, panel1.Width / 2, panel1.Height - 50, Length, -Math.PI / 2);
        }

        public void DrawCayleyTree(int n, double x0, double y0, double length, double th)
        {
            if(n==0)
            {
                return;
            }
            double x1 = x0 + length * Math.Cos(th);
            double y1 = y0 + length * Math.Sin(th);
            double x2 = x1 - length / 2 * Math.Cos(th);
            double y2 = y1 - length / 2 * Math.Sin(th);
            Pen SelectedPenColor = comboBox1.SelectedValue as Pen;
            graphics.DrawLine(SelectedPenColor, (int)x0, (int)y0, (int)x1, (int)y1);

            DrawCayleyTree(n - 1, x1, y1, LeftPer * length, th + LeftDegree * Math.PI/180);
            DrawCayleyTree(n - 1, x2, y2, RightPer * length, th - RightDegree * Math.PI/180);
        }


    }

     
}
