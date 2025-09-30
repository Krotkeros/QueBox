
using MiApi.Contexts;
using MiApi.Query.Implements;
using MiApi.Query.Interfaces;
using MiApi.Repository.Implements;
using MiApi.Repository.Interfaces;
using MiApi.Servicios.Implements;
using MiApi.Servicios.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace MiApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            builder.Services.AddTransient<IAnimal, Perro>();
            builder.Services.AddTransient<IPersonaQueries, PersonaQueries>();
            builder.Services.AddTransient<IPersonaRepository, PersonaRepository>();

            builder.Services.AddDbContext<ApiContext>(
                opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

            builder.Services.AddScoped<IDbConnection>(options =>
            {
                return new SqlConnection(builder.Configuration.GetConnectionString("SqlConnection"));
            });

            builder.Services.AddSwaggerGen(options =>
            {
                string archivo = "MiApi.xml";
                string path = Path.Combine(AppContext.BaseDirectory, archivo);
                options.IncludeXmlComments(path);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
