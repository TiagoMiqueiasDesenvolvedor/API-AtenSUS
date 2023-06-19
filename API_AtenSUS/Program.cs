using API_AtenSUS.Models;
using API_AtenSUS.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<PacientesDatabaseSettings>(
    builder.Configuration.GetSection("DevNetStoreDatabase")); // Objeto pego do appsettings.json

builder.Services.AddSingleton<PacientesServices>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API AtenSUS", Version = "v1" });
});

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors((g) => g.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader());

    app.UseSwagger();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors((g) => g.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

app.MapControllers();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API AtenSUS v1");
});

app.Run();
