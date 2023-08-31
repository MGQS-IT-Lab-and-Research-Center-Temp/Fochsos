using AutoMapper;
using Fochso.Entities;
using Fochso.Models.Result;
using Fochso.Models.Role;
using Fochso.Models.Student;
using Fochso.Models.Teacher;
using Fochso.Models.User;
using Fochso.Models.Class;
using Fochso.Models.News;


    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
        // Result mapping
            CreateMap<Result,CreateResultViewModel>().ReverseMap(); // Example mapping
            CreateMap<Result, ResultViewModel>().ReverseMap(); // Example mapping
            CreateMap<Result, UpdateResultViewModel>().ReverseMap();    // Define other mappings here

            CreateMap<News,NewsViewModel>().ReverseMap();    // Define other mappings here
            CreateMap<Role, RoleViewModel>().ReverseMap();    // Define other mappings here
            CreateMap<Student, StudentViewModel>().ReverseMap();    // Define other mappings here
            CreateMap<Teacher, TeacherViewModel>().ReverseMap();    // Define other mappings here
            CreateMap<User, UserViewModel>().ReverseMap();    // Define other mappings here
        }
    }

