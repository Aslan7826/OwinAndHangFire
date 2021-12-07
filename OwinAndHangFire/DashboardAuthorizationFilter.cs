using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinAndHangFire
{
    public class DashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        //回傳 true 同等不授權
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}
