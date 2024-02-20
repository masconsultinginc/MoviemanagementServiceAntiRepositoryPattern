namespace MovieManagement.Domain;

public interface IUnitOfWork
{
    IActorRepository Actors { get; }

    Task CompleteAsync();
}
