using Microsoft.AspNetCore.Mvc;
using MovieManagement.DataAccess;
using MovieManagement.Domain;

namespace MovieManagement.API;

[ApiController]
[Route("api/v1/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly ILogger<MoviesController> _logger;
    private readonly MovieData _repo;

    public MoviesController(
        ILogger<MoviesController> logger,
        MovieData movieData
    )
    {
        _logger = logger;
        _repo = movieData;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMovie(Movie movie)
    {
        if (ModelState.IsValid)
        {
            await _repo.AddMovie(movie);

            return CreatedAtAction("GetMovie", new { movie.Id }, movie);
        }

        return new JsonResult("Something went wrong!") { StatusCode = 500 };
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovie(Guid id)
    {
        var movie = await _repo.FindMovie(id);

        if (movie == null)
            return NotFound();

        return Ok(movie);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var movies = await _repo.GetAllMovies();

        return Ok(movies);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateMovie(Movie movie)
    {
        await _repo.UpdateMovie(movie);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(Guid id)
    {
        var movie = await _repo.FindMovie(id);

        if (movie == null)
            return BadRequest();

        await _repo.RemoveMovie(id);

        return Ok(movie);
    }
}
