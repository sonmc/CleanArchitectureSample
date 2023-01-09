using System.Threading.Tasks;
using Ardalis.Result;
using CleanArchitectureSample.Core.ContributorAggregate;
using CleanArchitectureSample.Core.ContributorAggregate.Events;
using CleanArchitectureSample.Core.Interfaces;
using CleanArchitectureSample.SharedKernel.Interfaces;
using MediatR;

namespace CleanArchitectureSample.Core.Services;

public class DeleteContributorService : IDeleteContributorService
{
  private readonly IRepository<Contributor> _repository;
  private readonly IMediator _mediator;

  public DeleteContributorService(IRepository<Contributor> repository, IMediator mediator)
  {
    _repository = repository;
    _mediator = mediator;
  }

  public async Task<Result> DeleteContributor(int contributorId)
  {
    var aggregateToDelete = await _repository.GetByIdAsync(contributorId);
    if (aggregateToDelete == null) return Result.NotFound();

    await _repository.DeleteAsync(aggregateToDelete);
    var domainEvent = new ContributorDeletedEvent(contributorId);
    await _mediator.Publish(domainEvent);
    return Result.Success();
  }
}
