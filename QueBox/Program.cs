using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QueBox.Contexts;
using System.Data;
using QueBox.Models;
using QueBox.Repository;
using QueBox.Query;
using static System.Runtime.InteropServices.JavaScript.JSType;
using QueBox.Query.Interfaces;
using QueBox.Repository.Interfaces;
using QueBox.Repository.Implements;


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
            builder.Services.AddTransient<IUsuarioQueries, UsuarioQueries>();
            builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

            // builder.Services.AddTransient<IAnimal, Perro>();  <- Codigo para los servicios
            builder.Services.AddTransient<ICapaQueries, CapaQueries>();
            builder.Services.AddTransient<ICapaRepository, CapaRepository>();

            // builder.Services.AddTransient<IAnimal, Perro>();  <- Codigo para los servicios
            builder.Services.AddTransient<IDisenoQueries, DisenoQueries>();
            builder.Services.AddTransient<IDisenoRepository, DisenoRepository>();

            // builder.Services.AddTransient<IAnimal, Perro>();  <- Codigo para los servicios
            builder.Services.AddTransient<IImagenDecorativaQueries, ImagenDecorativaQueries>();
            builder.Services.AddTransient<IImagenDecorativaRepository, ImagenDecorativaRepository>();

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
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
