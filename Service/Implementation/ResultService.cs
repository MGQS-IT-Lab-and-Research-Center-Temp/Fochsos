using AutoMapper;
using Fochso.Entities;
using Fochso.Models;
using Fochso.Models.Class;
using Fochso.Models.Result;
using Fochso.Models.Student;
using Fochso.Repository.Implementations;
using Fochso.Repository.Interface;
using Fochso.Repository.Interfaces;
using Fochso.Service.Interface;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace Fochso.Service.Implementation
{
    public class ResultService :IResultService
    {
        private readonly IResultRepository _resultRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ResultService(IResultRepository resultRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _resultRepository = resultRepository;
        }

        public BaseResponseModel CreateResult(CreateResultViewModel createResult)
        {
            var response = new BaseResponseModel();
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;

            if (createResult == null)
            {
                response.Message = "Result field is required";
                return response;
            }
            var students = _unitOfWork.Students.GetStudentByName(createResult.Name);

            var results = _mapper.Map<Result>(createResult);
              results.Name = createResult.Name;
              results.StudentId = students.Id;
              results.Class = students.Class;
              results.DateCreated = DateTime.Now;
              results.CreatedBy = createdBy;
            
         
            try
            {
                _unitOfWork.Results.Create(results);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Result created successfully.";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Failed to create result at this time: {ex.Message}";
                return response;
            }
        }

        public BaseResponseModel DeleteResult(int resultId)
        {

            var response = new BaseResponseModel();
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var isResultExist = _unitOfWork.Results.Exists(s => s.Id == resultId);
            if (!isResultExist)
            {
                response.Message = "result does not exist!";
            }
            var results = _unitOfWork.Results.Get(resultId);

            try
            {
                _unitOfWork.Results.Remove(results);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Result successfully deleted.";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Result delete failed: {ex.Message}";
                return response;
            }
        }

        public ResultsResponseModel GetAllResult()
        {
            var response = new ResultsResponseModel();
            try
            {
                var results = _unitOfWork.Results.GetAll();

                if (results is null)
                {
                    response.Status = false;
                    response.Message = "No result found";
                    return response;
                }
                response.Data =_mapper.Map<List<ResultViewModel>>(results);
                response.Status = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            response.Status = true;
            response.Message = "Success";
            return response;
        }

        public ResultResponseModel GetResult(int resultId)
        {

            var response = new ResultResponseModel();

            Expression<Func<Result, bool>> expression = s =>
                                              (s.Id == resultId);
            var isResultExist = _unitOfWork.Results.Exists(expression);

            if (!isResultExist)
            {
                response.Status = false;
                response.Message = $"Student with id {resultId} does not exist.";
                return response;
            }

            var result = _unitOfWork.Results.Get(resultId);
            response.Status = true;
            response.Message = "Success";
            response.Data = _mapper.Map<ResultViewModel>(result);

            return response;
        }

        public IEnumerable<Result> GetResultsByStudentId(int studentId)
        {
            return _resultRepository.GetResultsByStudentId(studentId);
        }

        public BaseResponseModel UpdateResult(int resultId, UpdateResultViewModel updateResult)
        {
            var response = new BaseResponseModel();
            string modifiedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var isResultExist = _unitOfWork.Classes.Exists(expression => expression.Id == resultId);

            if (!isResultExist)
            {
                response.Message = "Result does not exist";
                return response;
            }
            var results = _unitOfWork.Results.Get(resultId);
           _mapper.Map<List<UpdateResultViewModel>>(results);
            try
            {
                _unitOfWork.Results.Update(results);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Result successfully updated";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
