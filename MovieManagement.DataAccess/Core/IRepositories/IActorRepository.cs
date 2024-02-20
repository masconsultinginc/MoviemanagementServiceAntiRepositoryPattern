namespace MovieManagement.Domain;

public interface IActorRepository : IGenericRepository<Actor>
{
    Task<IEnumerable<Actor>> GetActorsWithMovies();
}
