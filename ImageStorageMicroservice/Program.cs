using ImageStorageMicroservice;
using ImageStorageMicroservice.Services;

public class Program
{
    // Används för att konfigurera och starta värdprocessen för din ASP.NET Core-applikation. Det innehåller allt som behövs för att köra din applikation.
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
