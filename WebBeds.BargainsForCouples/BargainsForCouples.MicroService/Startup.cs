using System;
using System.Linq;
using AutoMapper;
using BargainsForCouples.MicroService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Extensions.Http;
namespace BargainsForCouples.MicroService
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
            services.AddControllers();
            services.Configure<BargainsForCouplesSettings>(Configuration);
            services.AddHttpClient<IBargainsForCouplesApiClient, BargainsForCouplesApiClient>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                //Implemented Polly to provides resilience and transient-fault handling capabilities
                .AddPolicyHandler((x) =>
                {
                    Random jitterer = new Random();
                    var retryWithJitterPolicy = HttpPolicyExtensions
                        .HandleTransientHttpError()
                        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                        .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) + TimeSpan.FromMilliseconds(jitterer.Next(0, 100))
                        );
                    return retryWithJitterPolicy;
                })
                .AddPolicyHandler((x) =>
                {
                    return HttpPolicyExtensions
                        .HandleTransientHttpError()
                            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
                });

            services.AddTransient<IBargainService, BargainService>();
            services.AddAutoMapper(typeof(Startup));

            services.AddDistributedRedisCache(options =>
            {
                string redisHostName = Configuration.GetSection("RedisConfig").GetValue<string>("RedisHostName");
                options.Configuration = System.Net.Dns.GetHostAddressesAsync(redisHostName).Result.FirstOrDefault().ToString();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
