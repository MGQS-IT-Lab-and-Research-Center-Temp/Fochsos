using Fochso.Context;
using Fochso.Entities;
using Fochso.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Fochso.Repository.Implementations
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(FochsoContext context) : base(context)
        {
        }

		public Student GetStudent(Expression<Func<Student, bool>> expression)
		{
			var students = _context.Students
			.Include(c => c.ClassClass)
			 .SingleOrDefault(expression);

			return students;
		}

		public List<Student> GetStudents()
		{
			var students = _context.Students
			.Include(s => s.Name)
			.Include(s => s.Id)
			.Include(s => s.ClassClass)
			.ThenInclude(s => s.Id)
            .Include(s => s.ClassClass)
            .ThenInclude(s => s.Name)
			.ToList();

			return students;
		}

		public List<Student> GetStudents(Expression<Func<Student, bool>> expression)
		{
			var students = _context.Students
			  .Where(expression)
			  .Include(s => s.Name)
			  .Include(s => s.Class)
			  .Include(s => s.Id)
			  .Include(s => s.ClassClass)
			  .ThenInclude(s => s.Id)
              .Include(s => s.ClassClass)
              .ThenInclude(s => s.Name)
              .ToList();

			return students;
		}

		public List<Student> GetStudentsByClassId(int id)
		{
			var studentclass = _context.Students
				.Include(sc => sc.ClassClass)
				.Include(sc => sc.Class)
				.Include(s => s.Name)
				.Include(s => s.Id)
				.Where(s => s.Id.Equals(id))
				.ToList();

			return studentclass;
		}

        public Student GetStudentByName(string studentName)
        {
            return _context.Students.FirstOrDefault(c => c.Name == studentName);
        }
    }
}
