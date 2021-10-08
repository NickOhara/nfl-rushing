using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using nfl_rushing.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace nfl_rushing
{
    public class Startup
    {
        private readonly string BIG_RUSH = "rushing_big.json";
        private readonly string LITTLE_RUSH = "rushing.json";
        private readonly string dbName = "NFLRushing.db";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var jsonFile = Configuration.GetValue<int>("s") == 1 ? BIG_RUSH : LITTLE_RUSH;
            if (File.Exists(dbName))
            {
                File.Delete(dbName);
            }
            using (var client = new DatabaseContext())
            {
                client.Database.EnsureCreated();
                if (!client.PlayerStats.Any())
                {
                    var serializeOptions = new JsonSerializerOptions();
                    serializeOptions.Converters.Add(new StringConverter());
                    serializeOptions.Converters.Add(new NumberConverter());
                    var playerStats = JsonSerializer.Deserialize<List<PlayerStats>>(File.ReadAllText(jsonFile), serializeOptions);

                    client.PlayerStats.AddRange(playerStats);
                    client.SaveChanges();
                }
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddEntityFrameworkSqlite().AddDbContext<DatabaseContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
