using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieManagement.Domain;

namespace MovieManagement.DataAccess;

public class ActorRepository : GenericRepository<Actor>, IActorRepository
{
    public ActorRepository(
        MovieManagementDbContext context,
        ILogger logger
    ) : base(context, logger)
    {

    }

    public override async Task<IEnumerable<Actor>> All()
    {
        try
        {
            return await dbSet.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} All method error", typeof(ActorRepository));
            return new List<Actor>();
        }
    }

    public override async Task<bool> Upsert(Actor entity)
    {
        try
        {
            var existingActor = await dbSet.Where(a => a.Id == entity.Id)
                .FirstOrDefaultAsync();

            if (existingActor == null)
                return await Add(entity);

            existingActor.FirstName = entity.FirstName;
            existingActor.LastName = entity.LastName;

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Upsert method error", typeof(ActorRepository));
            return false;
        }
    }

    public override async Task<bool> Delete(Guid id)
    {
        try
        {
            var existingActor = await dbSet.Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            if (existingActor != null)
            {
                dbSet.Remove(existingActor);
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Delete method error", typeof(ActorRepository));
            return false;
        }
    }

    public async Task<IEnumerable<Actor>> GetActorsWithMovies()
    {
        var actorsWithMovies = await _context.Actors.Include(u => u.Movies).ToListAsync();
        return actorsWithMovies;
    }
}
