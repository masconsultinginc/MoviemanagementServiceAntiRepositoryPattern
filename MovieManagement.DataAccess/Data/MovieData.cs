using Microsoft.EntityFrameworkCore;
using MovieManagement.Domain;

namespace MovieManagement.DataAccess;

public class MovieData
{
    private readonly MovieDbContext _context;

    public MovieData(
        MovieDbContext context
    )
    {
        _context = context;
    }

    public async Task<bool> AddMovie(Movie movie)
    {
        await _context.Movies.AddAsync(movie);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<Movie> FindMovie(Guid id)
    {
        return await _context.Movies
        .AsNoTracking()
        .SingleOrDefaultAsync(m => m.Id == id);
    }

    public async Task<IEnumerable<Movie>> GetAllMovies()
    {
        return await _context.Movies.AsNoTracking().ToListAsync();
    }

    public async Task<bool> UpdateMovie(Movie movie)
    {
        _context.Entry(movie).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveMovie(Guid id)
    {
        _context.Movies.Remove(_context.Movies.Find(id));
        await _context.SaveChangesAsync();

        return true;
    }
}
