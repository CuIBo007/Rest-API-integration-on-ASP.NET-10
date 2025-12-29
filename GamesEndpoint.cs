
using GameStore.API.Dtos;

namespace GameStore.API;

public static class GamesEndpoint
{
    const string GetGameEndpointName = "GetGame";
  public static readonly List<GameDto> games = [
    new GameDto(
        1, 
        "The Witcher 3: Wild Hunt",
        "RPG",
        59.99m,
        new DateOnly(2015, 05, 19)
         ),

    new GameDto(
        2, 
        "Cyberpunk 2077", 
        "RPG", 
        49.99m, 
        new DateOnly(2020, 12, 10)),

    new GameDto(
        3, 
        "Hades", 
        "Roguelike", 
        24.99m, 
        new DateOnly(2020, 09, 17)
        )
];

public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");
        //GET request /games.
group.MapGet("/", () => games);

//GET request /games/{id}.
group.MapGet("/{id}", (int id) => //this line maps the get request with id parameter
{
    var game = games.Find(game => game.ID == id); //this line finds the game with the specified id
    return game is  null? Results.NotFound(): Results.Ok(game); //this line returns 404 if game is not found, otherwise returns 200 with the game data
})
.WithName(GetGameEndpointName); //this line names the endpoint for later reference


//Post request /games

group.MapPost("/",(CreateGameDto newGame) =>
{
    GameDto game = new(
        games.Count +1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game);
    return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.ID}, game);
});


//Put request /games
group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
{
    var index = games.FindIndex(game => game.ID == id);

    if (index == -1)
    {
        return Results.NotFound();   
    }
    games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    );
    return Results.NoContent();

});

//Delete request /games
      group.MapDelete("/{id}",(int id)=>
     {

          games.RemoveAll(game => game.ID == id);
    // var index =games.FindIndex(game => game.ID ==id);
    // games.RemoveAt(index);
         return Results.NoContent();
     });
    }


}