
using ContactsApi.Data;
using Microsoft.AspNetCore.OpenApi;
using ContactsApi.Controllers;

namespace ContactsApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Wstrzykiwanie kontekstu ContactContext do aplikacji
            builder.Services.AddDbContext<ContactContext>();   
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Metoda mapuj�ca punkty ko�cowe
            app.MapContactModelEndpoints();

           

            app.Run();
        }
    }
}