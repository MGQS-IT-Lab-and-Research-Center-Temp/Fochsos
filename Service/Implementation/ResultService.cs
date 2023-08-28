using AutoMapper;
using Fochso.Entities;
using Fochso.Models;
using Fochso.Models.Result;
using Fochso.Repository.Implementations;
using Fochso.Repository.Interface;
using Fochso.Repository.Interfaces;
using Fochso.Service.Interface;
using Microsoft.AspNetCore.Http;

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

            var results = _mapper.Map<Result>(createResult);
         
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
                response.Message = $"Failed to create class at this time: {ex.Message}";
                return response;
            }
        }

        public BaseResponseModel DeleteResult(int studentId)
        {
            throw new NotImplementedException();
        }

        public ResultsResponseModel GetAllResult()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Result> GetResultsByStudentId(int studentId)
        {
            return _resultRepository.GetResultsByStudentId(studentId);
        }

        public BaseResponseModel UpdateResult(int studentId, UpdateResultViewModel updateResult)
        {
            throw new NotImplementedException();
        }
    }
}
