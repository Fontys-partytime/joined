using Microsoft.EntityFrameworkCore;
using Partytime.Common.MassTransit;
using Partytime.Common.Settings;
using Partytime.Joined.Service.Entities;
using Partytime.Joined.Service.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Get service settings
var serviceSettings = builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

// Use the common code and initialize what was here before
builder.Services.AddMassTransitWithRabbitMq();

builder.Services.AddControllers();

builder.Services.AddDbContext<JoinedContext>(opt =>
    opt
    .UseNpgsql(builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionString"), providerOptions => providerOptions.EnableRetryOnFailure())
    .UseSnakeCaseNamingConvention());
builder.Services.AddScoped<IJoinedRepository, JoinedRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{ THIS IS COMMMENTED UNTILL THE END OF THE DEVELOPMENT PHASE SO YOU CAN TEST SWAGGER ON DOCKER
app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
