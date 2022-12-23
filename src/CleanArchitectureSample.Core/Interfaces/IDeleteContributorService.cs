using Ardalis.Result;

namespace CleanArchitectureSample.Core.Interfaces;

public interface IDeleteContributorService
{
    public Task<Result> DeleteContributor(int contributorId);
}
