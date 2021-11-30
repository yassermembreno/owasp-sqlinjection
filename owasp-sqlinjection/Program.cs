using System.Configuration;
using owas_sqlinjection.Infrastructure.DBContexts;
using owas_sqlinjection.Infrastructure.Repositories;
using owasp_sqlinjection.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Add(new ServiceDescriptor(typeof(IDBContext), new MySqlDBContext(builder.Configuration.GetConnectionString("Default"))));
builder.Services.AddScoped<ICustomerRepository, UnsecureCustomerRepository>();
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

