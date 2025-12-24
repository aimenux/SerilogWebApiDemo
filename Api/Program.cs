using Api.Extensions;

namespace Api;

public static class Program
{
    public static async Task Main(string[] args)
    {
        using var host = CreateHostBuilder(args).Build(); 
        await host.RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webHostBuilder =>
            {
                webHostBuilder.UseStartup<Startup>();
                webHostBuilder.CaptureStartupErrors(true);
                webHostBuilder.UseSetting(WebHostDefaults.DetailedErrorsKey, "true");
            })
            .UseRandomConfigSerilog();
}