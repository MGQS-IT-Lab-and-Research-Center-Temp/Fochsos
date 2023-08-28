using Fochso.Entities;
using Fochso.Models.Class;
using Fochso.Models;
using Fochso.Models.Result;

namespace Fochso.Service.Interface
{
    public interface IResultService 
    {

        BaseResponseModel CreateResult(CreateResultViewModel createResult);
        BaseResponseModel DeleteResult(int studentId);
        BaseResponseModel UpdateResult(int studentId, UpdateResultViewModel updateResult);
        ResultsResponseModel GetAllResult();
        IEnumerable<Result> GetResultsByStudentId(int studentId);
    }
}
