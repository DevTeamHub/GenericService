using System.Linq;

namespace DevTeam.GenericService.Pagination
{
    public class SearchResult<TEntity>
    {
        public IQueryable<TEntity> Query { get; set; }
        public int Count { get; set; }
    }
}
