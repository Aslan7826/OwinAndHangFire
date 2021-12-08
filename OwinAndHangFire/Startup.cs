using Hangfire;
using Hangfire.Console;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OwinAndHangFire
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            var config = new HttpConfiguration();
            HangfireConfig.Register(app);
            config.Routes.MapHttpRoute("DefaultApi",
                                       "api/{controller}/{id}",
                                       new { id = RouteParameter.Optional }
                                      );

            app.UseWelcomePage("/");
            app.UseWebApi(config);
            app.UseErrorPage();

            //hangfire的設定
            var jobServerOptions = new BackgroundJobServerOptions()
            {
                //排成器最低多久詢問一次
                SchedulePollingInterval = new TimeSpan(0, 0, 2),
            };                                               
            app.UseHangfireServer(jobServerOptions);
            //hangfire Dashborad
            var dashboardOptions = new DashboardOptions
            {
                Authorization = new[]
                {
                    new DashboardAuthorizationFilter()
                }
            };
            app.UseHangfireDashboard("/hangfire", dashboardOptions);

        }

    }
    internal class HangfireConfig
    {
        public static void Register(IAppBuilder app)
        {
            GlobalConfiguration.Configuration
                               .UseSqlServerStorage("Server=.;Database=HangFire;User Id=sa;Password=1qaz@WSX;")
                               .UseNLogLogProvider()
                               .UseConsole();
            GlobalJobFilters.Filters.Add( new AutomaticRetryAttribute 
                    {
                        Attempts = 2, 
                        DelaysInSeconds = new[] { 10 , 10 }, 
                        OnAttemptsExceeded = AttemptsExceededAction.Fail 
                    });

            app.UseHangfireDashboard("/hangfire");
            app.UseHangfireServer();
        }
    }
}
