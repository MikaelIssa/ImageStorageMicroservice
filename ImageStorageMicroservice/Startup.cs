using ImageStorageMicroservice.Data;
using ImageStorageMicroservice.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models; // Importera nödvändiga namespaces

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

            // Lägg till autentiseringstjänster
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                // Konfigurera JwtBearer-options här, t.ex. hur du validerar JWT-token, tokenutgångstid etc.
            });

            // Lägg till behörighetskontrolltjänster
            services.AddAuthorization();

            // Lägg till controllers-tjänster
            services.AddControllers();

            // Lägg till Swagger-tjänsten
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Image Storage Microservice API", Version = "v1" });
            });
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

            app.UseAuthentication(); // Lägg till autentisering middleware
            app.UseAuthorization();

            // Aktivera Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Image Storage Microservice API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

//{
//    public class Startup
//    {
//        public Startup(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        public IConfiguration Configuration { get; }

//        public void ConfigureServices(IServiceCollection services)
//        {
//            // Lägg till Entity Framework DbContext som en tjänst
//            services.AddDbContext<ImageDbContext>(options =>
//                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

//            // Lägg till andra tjänster och konfigurationer här
//        }

//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            /* Här konfigureras middleware-kedjan för att hantera HTTP-begäranden. 
//             * Beroende på om applikationen körs i utvecklings- eller produktionsläge, används olika typer av felhantering och säkerhetsfunktioner. 
//             * Slutpunkterna för rutning sätts också upp här för att bestämma hur inkommande HTTP-begäranden ska hanteras och vilken kontroller och åtgärder som ska köras.
//             * I detta fall finns det en standardruta som leder till HomeController's Index-åtgärd om ingen annan matchning hittas.
//             */
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }
//            else
//            {
//                app.UseExceptionHandler("/Home/Error");
//                app.UseHsts();
//            }

//            app.UseHttpsRedirection();
//            app.UseStaticFiles();

//            app.UseRouting();

//            app.UseAuthorization();

//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapControllerRoute(
//                    name: "default",
//                    pattern: "{controller=Home}/{action=Index}/{id?}");
//            });
//        }
//    }
//}
