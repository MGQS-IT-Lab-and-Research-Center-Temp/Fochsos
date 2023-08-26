using Fochso.Repository.Interface;

namespace Fochso.Repository.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRoleRepository Roles { get; }
    IUserRepository Users { get; }
    IStudentRepository Students { get; }
    ITeacherRepository Teachers { get; }
    IClassRepository Classes { get; }
    INewsRepository Newses { get; }
    int SaveChanges();
}