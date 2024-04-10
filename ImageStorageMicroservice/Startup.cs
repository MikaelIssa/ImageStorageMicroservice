using ImageStorageMicroservice.Data;
using ImageStorageMicroservice.Services;
using Microsoft.EntityFrameworkCore;

namespace ImageStorageMicroservice
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Lägg till Entity Framework DbContext som en tjänst
            services.AddDbContext<ImageDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Lägg till andra tjänster och konfigurationer här
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /* Här konfigureras middleware-kedjan för att hantera HTTP-begäranden. 
             * Beroende på om applikationen körs i utvecklings- eller produktionsläge, används olika typer av felhantering och säkerhetsfunktioner. 
             * Slutpunkterna för rutning sätts också upp här för att bestämma hur inkommande HTTP-begäranden ska hanteras och vilken kontroller och åtgärder som ska köras.
             * I detta fall finns det en standardruta som leder till HomeController's Index-åtgärd om ingen annan matchning hittas.
             */
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
