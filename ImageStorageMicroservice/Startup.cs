using ImageStorageMicroservice.Services;

namespace ImageStorageMicroservice
{
    public class Startup
    {
        /* används för att konfigurera hur din ASP.NET Core-applikation ska startas upp och fungera. Här läggs tjänster och middleware till,
           och det definierar hur din applikation ska hantera inkommande begäranden
        */
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // Lägg till ytterligare tjänster här, t.ex. databaskopplingar, loggning, etc.
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
