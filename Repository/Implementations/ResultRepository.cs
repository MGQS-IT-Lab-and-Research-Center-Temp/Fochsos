using Fochso.Context;
using Fochso.Entities;
using Fochso.Repository.Interface;

namespace Fochso.Repository.Implementations
{
    public class ResultRepository : BaseRepository<Result>, IResultRepository
    {


        public ResultRepository(FochsoContext context) : base(context)
        {
        }

        public IEnumerable<Result> GetResultsByStudentId(int studentId)
        {
            // Implement the logic to query the database for results
            return _context.Results.Where(r => r.Student.Id == studentId).ToList();
        }
    }
}
