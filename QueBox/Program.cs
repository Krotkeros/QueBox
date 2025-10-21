using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QueBox.Contexts;
using QueBox.Query.Implements;
using QueBox.Query.Interfaces;
using QueBox.Repository.Implements;
using QueBox.Repository.Interfaces;
using QueBox.Services.Implements;
using QueBox.Services.Interfaces;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace QueBox
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            // builder.Services.AddTransient<IAnimal, Perro>();  <- Codigo para los servicios
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
                string archivo = "QueBox.xml";
                string path = Path.Combine(AppContext.BaseDirectory, archivo);
                options.IncludeXmlComments(path);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
