using CodeChallenge.Abstractions;
using CodeChallenge.Configuration;
using CodeChallenge.Configuration.Extensions;
using CodeChallenge.Repositories;
using CodeChallenge.Services;

namespace CodeChallenge
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly IAppSettings appSettings = new AppSettings();

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
            configuration.GetSection("ServiceConfiguration").Bind(appSettings);
        }

        // This method gets called by the runtime. Use this method to add serices to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var httpContextAccessor = new HttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor>(httpContextAccessor);
            services
            .AddControllers()
            .AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.ApplyDefaultConfig();
            });

            services.AddSingleton(appSettings);
            services.AddCosmosDbClient(appSettings);
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IDepthChartService, DepthChartService>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IDepthChartRepository, DepthChartRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
