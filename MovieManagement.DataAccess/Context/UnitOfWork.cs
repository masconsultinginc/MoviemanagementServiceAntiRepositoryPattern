using Microsoft.Extensions.Logging;
using MovieManagement.Domain;

namespace MovieManagement.DataAccess;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly MovieManagementDbContext _context;
    private readonly ILogger _logger;

    public IActorRepository Actors { get; private set; }

    public UnitOfWork(
        MovieManagementDbContext context,
        ILoggerFactory loggerFactory
    )
    {
        _context = context;
        _logger = loggerFactory.CreateLogger("logs");

        Actors = new ActorRepository(_context, _logger);
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    // public async Task Dispose()
    // {
    //     await _context.DisposeAsync();
    // }
}
