using FluentValidation;
using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KooliProjekt.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Connection string
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // Controllers
            builder.Services.AddControllers();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // MediatR & Behaviors
            var applicationAssembly = typeof(ErrorHandlingBehavior<,>).Assembly;
            builder.Services.AddValidatorsFromAssembly(applicationAssembly);
            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(applicationAssembly);
                config.AddOpenBehavior(typeof(ErrorHandlingBehavior<,>));
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(TransactionalBehavior<,>));
            });

            var app = builder.Build();

            // Swagger UI dev
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();

            // Seed DB ja migratsioonid
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();

#if DEBUG
                var seeder = new SeedData(dbContext);
                seeder.Generate();
#endif
            }

            app.Run();
        }
    }
}   