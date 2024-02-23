using Microsoft.AspNetCore.Mvc;
using MovieManagement.DataAccess;
using MovieManagement.Domain;

namespace MovieManagement.API;

[ApiController]
[Route("api/v1/[controller]")]
public class ActorsController : ControllerBase
{
    private readonly ILogger<ActorsController> _logger;
    private readonly ActorData _repo;

    public ActorsController(
        ILogger<ActorsController> logger,
        ActorData actorData
    )
    {
        _logger = logger;
        _repo = actorData;
    }

    [HttpPost]
    public async Task<IActionResult> CreateActor(Actor actor)
    {
        if (ModelState.IsValid)
        {
            await _repo.AddActor(actor);

            return CreatedAtAction("GetActor", new { actor.Id }, actor);
        }

        return new JsonResult("Something went wrong!") { StatusCode = 500 };
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetActor(Guid id)
    {
        var actor = await _repo.FindActor(id);

        if (actor == null)
            return NotFound(); // 404 HTTP Status code

        return Ok(actor);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var actors = await _repo.GetAllActors();
        return Ok(actors);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateActor(Actor actor)
    {
        await _repo.UpdateActor(actor);

        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActor(Guid id)
    {
        var actor = await _repo.FindActor(id);

        if (actor == null)
            return BadRequest();

        await _repo.RemoveActor(id);

        return Ok(actor);
    }

    [HttpGet("movies")]
    public async Task<IActionResult> GetWithMovies()
    {
        var actors = await _repo.GetActorWithMovies();
        return Ok(actors);
    }

}
