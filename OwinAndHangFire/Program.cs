using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OwinAndHangFire
{
    static class Program
    {

        private static IDisposable s_webApp;
        private const string HOST_ADDRESS = "http://localhost:8001";
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            s_webApp = WebApp.Start<Startup>(HOST_ADDRESS);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
