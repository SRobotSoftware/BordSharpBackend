using API.Domain.Repositories;
using API.Domain.Services;
using API.Persistence.Contexts;
using API.Persistence.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql("Host=bord-db.cvgteocttiwq.us-west-2.rds.amazonaws.com;Port=5432;Database=bord-db;Username=postgres;Password=alongandcomplicatedpassword;");
                //options.UseInMemoryDatabase("bord-api-in-memory");
            });

            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IBoardRepository, BoardRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IBoardService, BoardService>();
            services.AddScoped<ITaskService, TaskService>();

            services.AddControllers().AddNewtonsoftJson();

            services.AddAutoMapper(typeof(Startup));
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
