using BE1109;
using DataAccess.Computer.DBContext;
using DataAccess.Computer.IServices;
using DataAccess.Computer.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddDbContext<MyShopDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("ConnStr_Appsetting")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IProductServices, ProductServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Hello world!");
//});

//app.UseMiddleware<SimpleMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
