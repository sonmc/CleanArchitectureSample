using Ardalis.Result;
using CleanArchitectureSample.Core.ProjectAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace CleanArchitectureSample.Core.Interfaces;

public interface IToDoItemSearchService
{
  Task<Result<ToDoItem>> GetNextIncompleteItemAsync(int projectId);
  Task<Result<List<ToDoItem>>> GetAllIncompleteItemsAsync(int projectId, string searchString);
}
