using CodeChallenge.Abstractions;
using CodeChallenge.Configuration;
using CodeChallenge.Configuration.Extensions;
using CodeChallenge.Domain;
using CodeChallenge.Repositories;
using CodeChallenge.Services;
using CodeChallenge.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

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
            services.AddMvc();
            services.AddSwaggerGen();
            services.AddSingleton<IHttpContextAccessor>(httpContextAccessor);
            services
            .AddControllers()
            .AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.ApplyDefaultConfig();
            });

            services.AddSingleton(appSettings);
            services.AddCosmosDbClient(appSettings);
            services.AddFluentValidationAutoValidation();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IDepthChartService, DepthChartService>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IDepthChartRepository, DepthChartRepository>();
            services.AddScoped<IValidator<Player>, PlayerValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
              
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
