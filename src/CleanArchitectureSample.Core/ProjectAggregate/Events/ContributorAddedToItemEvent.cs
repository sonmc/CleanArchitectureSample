using CleanArchitectureSample.Core.ContributorAggregate;
using CleanArchitectureSample.SharedKernel;

namespace CleanArchitectureSample.Core.ProjectAggregate.Events;

public class ContributorAddedToItemEvent : DomainEventBase
{
  public int ContributorId { get; set; }
  public ToDoItem Item { get; set; }

  public ContributorAddedToItemEvent(ToDoItem item, int contributorId)
  {
    Item = item;
    ContributorId = contributorId;
  }
}
