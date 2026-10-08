using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var connString = "Data Source=GameStore.db";
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.MapGet("/", () => "Goodbye, cruel world!");

app.MapGamesEndpoints();

app.MigrateDb();

app.Run();
