using Microsoft.EntityFrameworkCore;
using WebApiNetCoreRedis.Infra.Persistence;
using WebApiNetCoreRedis.Infra.Caching;

var builder = WebApplication.CreateBuilder(args);

//InMemory Database aqui
builder.Services.AddDbContext<ToDoListDbContext>(o => o.UseInMemoryDatabase("ToDoListDb"));

//Aponto para minha imagem Docker
builder.Services.AddStackExchangeRedisCache(o =>
{
    o.InstanceName = "instace";
    o.Configuration = "localhost:6379";
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
