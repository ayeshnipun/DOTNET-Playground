using Playground.API.Endpoints;
using Playground.Application;
using Playground.Infrastructure;
using Playground.Persistance;

var builder = WebApplication.CreateBuilder(args);

// DI
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure();

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapTokenEndpoints();

app.Run();
