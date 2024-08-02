using Core.Application.UseCases;
using Core.Domain.Interfaces;
using Infrastructure;
using Infrastructure.implementation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers();
builder.Services.AddDbContext<DbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<GetAccountBalance>();
builder.Services.AddScoped<GetAccountDetails>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddHostedService<RabbitMqConsumer>();

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

var rabbitMqConsumer = app.Services.GetRequiredService<RabbitMqConsumer>();
app.Run();
