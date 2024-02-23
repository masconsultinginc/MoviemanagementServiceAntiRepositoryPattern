using Microsoft.EntityFrameworkCore;
using MovieManagement.Domain;

namespace MovieManagement.DataAccess;

public class ActorData
{
    private readonly MovieManagementDbContext _context;
    public ActorData(
     MovieManagementDbContext context
    )
    {
        _context = context;
    }
    public async Task<IEnumerable<Actor>> GetAllActors()
    {
        return await _context.Actors.AsNoTracking().ToListAsync();
    }

    public async Task<Actor> FindActor(Guid? id)
    {
        return _context.Actors
            .AsNoTracking()
            .SingleOrDefault(a => a.Id == id);
    }

    public async Task<bool> AddActor(Actor actor)
    {
        await _context.Actors.AddAsync(actor);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateActor(Actor actor)
    {
        _context.Entry(actor).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveActor(Guid id)
    {
        _context.Actors.Remove(_context.Actors.Find(id));
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Actor>> GetActorWithMovies()
    {
        var actorsWithMovies = await _context.Actors.Include(a => a.Movies).ToListAsync();
        return actorsWithMovies;
    }
}
