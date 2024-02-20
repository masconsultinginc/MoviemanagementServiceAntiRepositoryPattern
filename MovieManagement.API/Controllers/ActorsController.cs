using Microsoft.AspNetCore.Mvc;
using MovieManagement.Domain;

namespace MovieManagement.API;

[ApiController]
[Route("api/v1/[controller]")]
public class ActorsController : ControllerBase
{
    private readonly ILogger<ActorsController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ActorsController(
        ILogger<ActorsController> logger,
        IUnitOfWork unitOfWork
    )
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<IActionResult> CreateActor(Actor actor)
    {
        if (ModelState.IsValid)
        {
            // This may not be needed???
            //actor.Id = Guid.NewGuid();

            await _unitOfWork.Actors.Add(actor);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction("GetActor", new { actor.Id }, actor);
        }

        return new JsonResult("Something went wrong!") { StatusCode = 500 };
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetActor(Guid id)
    {
        var actor = await _unitOfWork.Actors.GetById(id);

        if (actor == null)
            return NotFound(); // 404 HTTP Status code

        return Ok(actor);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var actors = await _unitOfWork.Actors.All();
        return Ok(actors);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateActor(Guid id, Actor actor)
    {
        if (id != actor.Id)
            return BadRequest();

        await _unitOfWork.Actors.Upsert(actor);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActor(Guid id)
    {
        var actor = await _unitOfWork.Actors.GetById(id);

        if (actor == null)
            return BadRequest();

        await _unitOfWork.Actors.Delete(id);
        await _unitOfWork.CompleteAsync();

        return Ok(actor);
    }

    [HttpGet("movies")]
    public async Task<IActionResult> GetWithMovies()
    {
        var actors = await _unitOfWork.Actors.GetActorsWithMovies();
        return Ok(actors);
    }

}
