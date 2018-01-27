using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UtilizePim.Config;
using UtilizePim.Services;
using UtilizePim.Services.ElasticSearch;

namespace UtilizePim
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<ElasticConfig>(Configuration.GetSection("ElasticConfig"));

            services.AddSingleton<IElasticSearchClientManager, ElasticSearchClientManager>();
            
            services.AddTransient<IProductQueryService, ProductQueryServiceElastic>();
            services.AddTransient<IProductWriteService, ProductWriteServiceElastic>();
            
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, 
            IElasticSearchClientManager clientManager, IOptions<ElasticConfig> elasticConfig)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            var index = elasticConfig.Value.IndexName;
            if (!clientManager.GetClient().IndexExists(index).Exists)
                clientManager.GetClient().CreateIndex(index);

        }
    }
}