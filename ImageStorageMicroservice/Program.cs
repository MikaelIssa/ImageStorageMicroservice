using ImageStorageMicroservice;
using ImageStorageMicroservice.Services;

public class Program
{
    // Anv�nds f�r att konfigurera och starta v�rdprocessen f�r din ASP.NET Core-applikation. Det inneh�ller allt som beh�vs f�r att k�ra din applikation.
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
