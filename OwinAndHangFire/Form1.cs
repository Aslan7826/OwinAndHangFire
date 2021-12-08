using Hangfire;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OwinAndHangFire
{
    public partial class Form1 : Form
    {
        private int play;
        public Form1()
        {
            InitializeComponent();
            play = 0;
            ShowTextBox();
        }
        private void ShowTextBox()
        {
            this.label1.Text = play.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Action action = () => { textBox1.Text = play.ToString() + '\n'; };
            HangFireGo("Good", ()=>action()  );
        }



        private void HangFireGo(string name, Expression<Action> action)
        {
            ShowTextBox();
            var cron = $"0/{this.textBox2.Text} * * * * ? ";
            RecurringJob.AddOrUpdate($"{name}-DoJob{play}", action, cron);
            play++;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            HangFireGo("bad", () => Error());
        }

        public void Error()
        {
            throw new Exception("reloadtext");
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
