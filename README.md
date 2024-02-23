# MoviemanagementServiceAntiRepositoryPattern

## AntiRepository Pattern

Inspired by **PluralSight** Julie Lerman using Domain Driven Design (DDD).
This training video provided insight into using __Async/Await__ with __Task__ using __IEnumerable__ for more performative 
CRUD request/response.  Alowing for the removal of the Repository/UnitOfWork Patterns.  Using dbContext
and dbSet as the built-in repository and unit-of-work.  This anti-pattern allowes for more finer control
over the basic CRUD operations (which is fine for repository/unitofwork patterns).  Using __LINQ__ and __AsNoTacking__ promotes
using DTO's for limiting client responses. (filtering, pagination, complex graphs, etc.)

All development was done in C# .net 8.

**Entiy Framework 6 in the Enterprise** by Julie Lerman