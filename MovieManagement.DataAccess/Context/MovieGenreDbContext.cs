using Microsoft.EntityFrameworkCore;
using MovieManagement.Domain;

namespace MovieManagement.DataAccess;

public class MovieGenreDbContext : DbContext
{
    public MovieGenreDbContext(DbContextOptions<MovieGenreDbContext> options) : base(options)
    { }

    public virtual DbSet<Genre> Genres { get; set; }
    public virtual DbSet<Movie> Movies { get; set; }
}
