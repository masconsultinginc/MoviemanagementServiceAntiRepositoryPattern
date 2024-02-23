using Microsoft.EntityFrameworkCore;
using MovieManagement.Domain;

namespace MovieManagement.DataAccess;

public class MovieDbContext : DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
    { }

    public virtual DbSet<Genre> Genres { get; set; }
    public virtual DbSet<Movie> Movies { get; set; }
    public virtual DbSet<Actor> Actors { get; set; }
}
