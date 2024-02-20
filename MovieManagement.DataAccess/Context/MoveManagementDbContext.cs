using Microsoft.EntityFrameworkCore;
using MovieManagement.Domain;

namespace MovieManagement.DataAccess;

public class MoveManagementDbContext : DbContext
{
    public MoveManagementDbContext(DbContextOptions<MoveManagementDbContext> options) : base(options)
    {

    }

    public virtual DbSet<Actor> Actors { get; set; }
    public virtual DbSet<Movie> Movies { get; set; }
    public virtual DbSet<Genre> Genres { get; set; }
    public virtual DbSet<Biography> Biographies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Guid Actor1Id = Guid.NewGuid();
        Guid Actor2Id = Guid.NewGuid();
        Guid Actor3Id = Guid.NewGuid();

        Guid Movie1Id = Guid.NewGuid();
        Guid Movie2Id = Guid.NewGuid();
        Guid Movie3Id = Guid.NewGuid();
        Guid Movie4Id = Guid.NewGuid();

        modelBuilder.Entity<Actor>().HasData(
            new Actor { Id = Actor1Id, FirstName = "Chuck", LastName = "Norris" },
            new Actor { Id = Actor2Id, FirstName = "Jane", LastName = "Doe" },
            new Actor { Id = Actor3Id, FirstName = "Van", LastName = "Damme" }
        );

        modelBuilder.Entity<Movie>().HasData(
            new Movie { Id = Movie1Id, Name = "Wakanda Forever", Description = "Box Office Open Soon", ActorId = Actor1Id },
            new Movie { Id = Movie2Id, Name = "Wakanda Forever", Description = "Box Office Open Soon", ActorId = Actor2Id },
            new Movie { Id = Movie3Id, Name = "Spiderman", Description = "Sky Scrapper be warned", ActorId = Actor1Id },
            new Movie { Id = Movie4Id, Name = "Matrix", Description = "Blue or Red Pill", ActorId = Actor3Id }
        );


    }
}
