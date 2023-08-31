using Fochso.Context;
using Fochso.Entities;
using Fochso.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Fochso.Repository.Implementations
{
	public class ClassRepository : BaseRepository<Class> , IClassRepository
	{
		public ClassRepository(FochsoContext fochsoContext) : base(fochsoContext) 
		{

		}

		public Class GetClass(Expression<Func<Class, bool>> expression)
		{
			var classes = _context.Classes
						.Include(c => c.Name)
						.Include(c => c.Students)
						 .Include(u => u.Id)
						 .SingleOrDefault(expression);

			return classes;
		}

		public List<Class> GetClasses()
		{
			var classes = _context.Classes
						//.Include(s => s.Name)
						//.Include(s => s.Id)
						.Include(s => s.Students)
						.ToList();

			return classes;
		}

		public List<Class> GetClasses(Expression<Func<Class, bool>> expression)
		{
			var classes = _context.Classes
						  .Where(expression)
						  //.Include(s => s.Name)
						  .Include(s => s.Students)
						  //.Include(s => s.Id)
						  .ToList();

			return classes;
		}

        public Class GetClassByName(string className)
        {
            return _context.Classes.FirstOrDefault(c => c.Name == className);
        }

    }
}
