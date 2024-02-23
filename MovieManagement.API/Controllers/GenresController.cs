using Microsoft.AspNetCore.Mvc;
using MovieManagement.DataAccess;
using MovieManagement.Domain;

namespace MovieManagement.API;

[ApiController]
[Route("api/v1/[controller]")]
public class GenresController : ControllerBase
{
    private readonly ILogger<GenresController> _logger;
    private readonly GenreData _repo;

    public GenresController(
        ILogger<GenresController> logger,
        GenreData genreData
    )
    {
        _logger = logger;
        _repo = genreData;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGenre(Genre genre)
    {
        if (ModelState.IsValid)
        {
            await _repo.AddGenre(genre);

            return CreatedAtAction("GetGenre", new { genre.Id }, genre);
        }

        return new JsonResult("Something went wrong!") { StatusCode = 500 };
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGenre(Guid id)
    {
        var genre = await _repo.FindGenre(id);

        if (genre == null)
            return NotFound(); // 404 HTTP Status code

        return Ok(genre);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var genres = await _repo.GetAllGenres();

        return Ok(genres);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateGenre(Genre genre)
    {
        await _repo.UpdateGenre(genre);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(Guid id)
    {
        var genre = await _repo.FindGenre(id);

        if (genre == null)
            return BadRequest();

        await _repo.RemoveGenre(id);

        return Ok(genre);
    }
}
