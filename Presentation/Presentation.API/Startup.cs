namespace Presentation.API
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using PaymentGateway.Application.Services.Implementations;
    using PaymentGateway.Application.Services.Interfaces;
    using PaymentGateway.Data.Repositories;
    using PaymentGateway.Domain.Notifications;
    using PaymentGateway.Domain.Repositories;
    using PaymentGateway.Domain.Service.Implementations;
    using PaymentGateway.Domain.Service.Interfaces;
    using PaymentGateway.Domain.Validations;
    using PaymentGateway.Domain.ValueObjects;

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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Presentation.API", Version = "v1" });
            });

            services.AddScoped<IAuthorizationAppService, AuthorizationAppService>();
            services.AddScoped<IPaymentCaptureAppService, PaymentCaptureAppService>();

            services.AddScoped<IPaymentAuthorizationService, PaymentAuthorizationService>();
            services.AddScoped<IPaymentCaptureService, PaymentCaptureService>();

            services.AddScoped<INotificationHandler, NotificationHandler>();
            services.AddScoped<IValidator, Validator>();

            services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<IPaymentAuthorizationRepository, PaymentAuthorizationRepository>();
            services.AddSingleton<IPaymentCaptureRepository, PaymentCaptureRepository>();

            services.AddSingleton<IMemoryCache, MemoryCache>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Presentation.API v1"));
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
