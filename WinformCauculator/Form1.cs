using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformCauculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double left_op = Double.Parse(textBox2.Text);
            double right_op = Double.Parse(textBox1.Text);
            char myOperator = '+';
            myOperator = comboBox1.Text[0];
            double result = 0.0;
            switch (myOperator)
            {
                case '+': result = left_op + right_op; break;
                case '-': result = left_op - right_op; break;
                case '*': result = left_op * right_op; break;
                case '/': result = left_op / right_op; break;
                case '%': result = left_op % right_op; break;
            }
            resultBox.Text = result.ToString();

        }


    }
}
