namespace GameStore.API.Dtos;

// DTO (Data Transfer Object) is a lightweight object designed to carry data between different layers of an application or between a client and a server.

public record GameDto
(
    int ID,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
