using Microsoft.EntityFrameworkCore;

using TwilioApi.DBContexts;
using TwilioApi.Repository;
using TwilioApi.Repository.Interfaces;
using TwilioApi.Utilities;
 using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TwilioDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyCon")));

builder.Services.AddCors(options => {
    options.AddPolicy("NuevaPolitica", app => {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ITwilioRepository, TwilioRepository>();

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
