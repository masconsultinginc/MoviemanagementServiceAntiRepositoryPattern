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

    [HttpGet("id")]
    public async Task<IActionResult> GetActor(Guid id)
    {
        var actor = await _unitOfWork.Actors.GetById(id);

        if (actor == null)
            return NotFound(); // 404 HTTP Status code

        return Ok(actor);
    }
}
