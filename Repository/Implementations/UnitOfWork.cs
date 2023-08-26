using Fochso.Context;
using Fochso.Repository.Interface;
using Fochso.Repository.Interfaces;

namespace Fochso.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FochsoContext _context;
        private bool _disposed = false;
        public IRoleRepository Roles { get; }
        public IUserRepository Users { get; }
        public IStudentRepository Students { get; }
        public ITeacherRepository Teachers { get; }
        public IClassRepository Classes { get; }
        public INewsRepository Newses { get; }

        

        public UnitOfWork(
            FochsoContext context,
            IRoleRepository roleRepository,
            IUserRepository userRepository,
            IStudentRepository studentRepository,
            ITeacherRepository teacherRepository,
            IClassRepository classRepository,
            INewsRepository newsRepository)
         
        {
            _context = context;
            Roles = roleRepository;
            Users = userRepository;
            Students = studentRepository;
            Teachers = teacherRepository;
            Classes = classRepository;
            Newses = newsRepository;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
