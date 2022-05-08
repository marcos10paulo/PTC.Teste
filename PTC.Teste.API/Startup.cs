using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PTC.Teste.Api.Helpers;
using PTC.Teste.API.Consumers;
using PTC.Teste.Common.Interface;
using PTC.Teste.Common.Service;
using PTC.Teste.Dto.Configuration;

namespace PTC.Teste.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            

            services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            services.AddScoped<Api.Helpers.IAuthenticationService, Api.Helpers.AuthenticationService>();

            services.AddHostedService<EnviarEmailConsumer>();

            services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMqConfig"));
            services.Configure<RemetenteConfiguration>(Configuration.GetSection("RemetenteConfig"));

            services.AddTransient<IEnviarEmail, EnviarEmail>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PTC.Teste.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API PTC.Teste - v1"));
            }

            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
