using Hangfire;
using Hangfire.Storage;
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
        }
        private void ShowTextBox()
        {
            this.label1.Text =  play.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HangFireGo("Good", "RunGood");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            HangFireGo("bad", "Error");
        }
        public void RunGood()
        {
            Console.WriteLine($"{play}....");
        }
        public void Error()
        {
            throw new Exception("reloadtext");
        }
        private void HangFireGo(string name, string methodName)
        {

            var myType = this.GetType();
            var myMethodName = myType.GetMethod(methodName);
            var myAction = Expression.Lambda<Action>(Expression.Call(Expression.New(myType), myMethodName));
            play++;
            ShowTextBox();
            var cron = $"0 0/{this.textBox2.Text} * * * ? ";
            RecurringJob.AddOrUpdate($"{name}-{play}", myAction, cron);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var recurringJob in connection.GetRecurringJobs())
                {
                    RecurringJob.RemoveIfExists(recurringJob.Id);
                }
            }
        }
    }
}
