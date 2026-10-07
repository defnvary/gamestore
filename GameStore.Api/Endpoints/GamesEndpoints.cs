namespace GameStore.Api.Endpoints;

using GameStore.Api.Dtos;

public static class GamesEndpoints
{
    const string GetGameEndPointName = "GetGame";

    private static readonly List<GameDto> games = [
        new (1, "Street Fighter II", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
        new (2, "Final Fantasy VII Rebirth", "RPG", 69.99M, new DateOnly(2024, 2, 29)),
        new (3, "Astro Bot", "Platformer", 22.23M, new DateOnly(2022, 1, 19)),
    ];

    public static void MapGamesEndpoints(this WebApplication app) {
        var group = app.MapGroup("/games");

        // GET /games
        group.MapGet("/", () => games);


        // GET /games/id
        group.MapGet("/{id}", (int id) => {
            var game = games.Find(game => game.Id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        }).WithName(GetGameEndPointName);

        // POST /games
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(games.Count + 1, newGame.Name, newGame.Genre, newGame.Price, newGame.ReleaseDate);

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
        });

        // PUT /games/1
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var idx = games.FindIndex(game => game.Id == id);

            if (idx == -1) return Results.NotFound();

            games[idx] = new GameDto(id, updatedGame.Name, updatedGame.Genre, updatedGame.Price, updatedGame.ReleaseDate);

            return Results.NoContent();
        });

        // DELETE /games
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });
    }
}
