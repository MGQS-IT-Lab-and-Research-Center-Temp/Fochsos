using Fochso.Context;
using Fochso.Entities;
using Fochso.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace Fochso.Repository.Implementations
{
    public class NewsRepository : BaseRepository<News>, INewsRepository 
    {
        public NewsRepository(FochsoContext fochsoContext) : base(fochsoContext)
        {

        }

        public News GetNews(Expression<Func<News, bool>> expression)
        {
            var news = _context.Newses
                        .Include(c => c.Title)
                        .Include(c => c.Description)
                         .Include(u => u.Id)
                         .SingleOrDefault(expression);
            return news;
        }

        public List<News> GetNewses()
        {
            var news = _context.Newses
                        .Include(s => s.Title)
                        .Include(s => s.Id)
                        .Include(s => s.Description)
                        .ToList();
            return news;
        }

        public List<News> GetNewses(Expression<Func<News, bool>> expression)
        {
            var news = _context.Newses
                                  .Where(expression)
                                  .Include(s => s.Title)
                                  .Include(s => s.Description)
                                  .Include(s => s.Id)
                                  .ToList();
            return news;
        }
    }
}
