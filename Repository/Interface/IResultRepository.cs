using Fochso.Entities;

namespace Fochso.Repository.Interface
{
    public interface IResultRepository :IRepository<Result>
    {
        IEnumerable<Result> GetResultsByStudentId(int studentId);
    }
}
