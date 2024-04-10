using ImageStorageMicroservice;
using ImageStorageMicroservice.Services;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>()
                      .UseUrls("https://localhost:5284")
                      .UseWebRoot("C:\\Utvecklare\\ImageStorageMicroservice\\wwwroot");
        });
}
