using CleanArchitectureSample.Core.ProjectAggregate;
using CleanArchitectureSample.Core.ProjectAggregate.Specifications;
using CleanArchitectureSample.SharedKernel.Interfaces;
using CleanArchitectureSample.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureSample.Web.Controllers;

[Route("[controller]")]
public class ProjectController : Controller
{
  private readonly IRepository<Project> _projectRepository;

  public ProjectController(IRepository<Project> projectRepository)
  {
    _projectRepository = projectRepository;
  }


  // GET: api/Projects
  [HttpGet]
  public async Task<IActionResult> List()
  {
    var projectDTOs = (await _projectRepository.ListAsync())
        .Select(project => new ProjectDTO
        (
            id: project.Id,
            name: project.Name
        ))
        .ToList();

    return Ok(projectDTOs);
  }

  // GET: api/Projects
  [HttpGet("{id:int}")]
  public async Task<IActionResult> GetById(int id)
  {
    var projectSpec = new ProjectByIdWithItemsSpec(id);
    var project = await _projectRepository.FirstOrDefaultAsync(projectSpec);
    if (project == null)
    {
      return NotFound();
    }

    var result = new ProjectDTO
    (
        id: project.Id,
        name: project.Name,
        items: new List<ToDoItemDTO>
        (
            project.Items.Select(i => ToDoItemDTO.FromToDoItem(i)).ToList()
        )
    );

    return Ok(result);
  }

  // POST: api/Projects
  [HttpPost]
  public async Task<IActionResult> Post([FromBody] CreateProjectDTO request)
  {
    var newProject = new Project(request.Name, PriorityStatus.Backlog);

    var createdProject = await _projectRepository.AddAsync(newProject);

    var result = new ProjectDTO
    (
        id: createdProject.Id,
        name: createdProject.Name
    );
    return Ok(result);
  }

  // PATCH: api/Projects/{projectId}/complete/{itemId}
  [HttpPatch("{projectId:int}/complete/{itemId}")]
  public async Task<IActionResult> Complete(int projectId, int itemId)
  {
    var projectSpec = new ProjectByIdWithItemsSpec(projectId);
    var project = await _projectRepository.FirstOrDefaultAsync(projectSpec);
    if (project == null) return NotFound("No such project");

    var toDoItem = project.Items.FirstOrDefault(item => item.Id == itemId);
    if (toDoItem == null) return NotFound("No such item.");

    toDoItem.MarkComplete();
    await _projectRepository.UpdateAsync(project);

    return Ok();
  }
}
