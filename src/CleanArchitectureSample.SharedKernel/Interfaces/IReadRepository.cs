using Ardalis.Specification;

namespace CleanArchitectureSample.SharedKernel.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
