using Microsoft.EntityFrameworkCore;
using MovieManagement.Domain;

namespace MovieManagement.DataAccess;

public class GenreData
{
    private readonly MovieGenreDbContext _context;

    public GenreData(
        MovieGenreDbContext context
    )
    {
        _context = context;
    }

    public async Task<bool> AddGenre(Genre genre)
    {
        await _context.Genres.AddAsync(genre);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<Genre> FindGenre(Guid id)
    {
        return await _context.Genres
            .AsNoTracking()
            .SingleOrDefaultAsync(g => g.Id == id);
    }

    public async Task<IEnumerable<Genre>> GetAllGenres()
    {
        return await _context.Genres.AsNoTracking().ToListAsync();
    }

    public async Task<bool> UpdateGenre(Genre genre)
    {
        _context.Entry(genre).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveGenre(Guid id)
    {
        _context.Genres.Remove(_context.Genres.Find(id));
        await _context.SaveChangesAsync();

        return true;
    }
}
