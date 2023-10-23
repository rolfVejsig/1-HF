using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourProject.Models;

namespace YourNamespace
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
            // Configure your context with the connection string
            services.AddDbContext<PluklisteContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("PluklisteContext")));

            // Add other services as needed
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Configure middleware and other settings here
        }
    }
}
