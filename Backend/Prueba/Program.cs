
using Microsoft.EntityFrameworkCore;
using Prueba.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configuración de la base de datos
builder.Services.AddDbContext<TodoContext>(
  opt => opt.UseSqlServer("name=DefaultConnection"));

// Configuración de CORS
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowSpecificOrigins", policy =>
  {
    policy.WithOrigins("http://localhost:4200") // URL del frontend
          .AllowAnyHeader()
          .AllowAnyMethod();
  });
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

// Usa CORS antes de cualquier middleware relacionado con autorización
app.UseCors("AllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
