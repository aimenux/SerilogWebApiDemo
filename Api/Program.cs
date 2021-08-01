using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Api.Extensions;

namespace Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webHostBuilder =>
                {
                    webHostBuilder.UseStartup<Startup>();
                    webHostBuilder.CaptureStartupErrors(true);
                    webHostBuilder.UseSetting(WebHostDefaults.DetailedErrorsKey, "true");
                })
                .UseRandomConfigSerilog();
    }
}
