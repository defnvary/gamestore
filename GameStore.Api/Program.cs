using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var app = builder.Build();

app.MapGet("/", () => "Goodbye, cruel world!");

app.MapGamesEndpoints();

app.Run();
