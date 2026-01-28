using AutoMapper;
using Ecom.Api.Mapping;
using Ecom.Api.Middleware;
using Ecom.Infrastucture;
using Microsoft.Extensions.DependencyInjection;
namespace Ecom.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddMemoryCache();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.InfrasturctureConfiguration(builder.Configuration);
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<CategoryMapping>();
                cfg.AddProfile<ProductMapping>();
            });


            var app = builder.Build();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}