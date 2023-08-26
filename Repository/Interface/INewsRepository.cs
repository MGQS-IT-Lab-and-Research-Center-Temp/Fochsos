using Fochso.Entities;
using System.Linq.Expressions;

namespace Fochso.Repository.Interface
{
    public interface INewsRepository : IRepository<News>
    {
        List<News> GetNewses();
        List<News> GetNewses(Expression<Func<News, bool>> expression);
        News GetNews(Expression<Func<News, bool>> expression);
    }
}
