using DevExpress.DashboardAspNetCore;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess;
using DevExpress.DataAccess.Excel;
using DevExpress.DataAccess.Sql;
using Microsoft.Extensions.FileProviders;

namespace AspNetCoreDashboard {
    public static class DashboardUtils {
        public static DashboardConfigurator CreateDashboardConfigurator(IConfiguration configuration, IFileProvider fileProvider, IServiceProvider serviceProvider) {
            DashboardConfigurator configurator = new DashboardConfigurator();
            configurator.SetConnectionStringsProvider(new DashboardConnectionStringsProvider(configuration));

            DashboardFileStorage dashboardFileStorage = new DashboardFileStorage(fileProvider.GetFileInfo("Data/Dashboards").PhysicalPath);
            configurator.SetDashboardStorage(dashboardFileStorage);

            var contextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
            
            configurator.CustomParameters += (s, e) => {
                var value = Convert.ToInt32(contextAccessor?.HttpContext?.Request.Headers["ProductId"].FirstOrDefault());
                e.Parameters.Add(new DashboardParameter("ProductIdParameter", typeof(int), value));
            };

            return configurator;
        }
    }
}