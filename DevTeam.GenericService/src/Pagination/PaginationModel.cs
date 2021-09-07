using System.Collections.Generic;

namespace DevTeam.GenericService.Pagination
{
    public class PaginationModel<TModel>
    {
        public List<TModel> List { get; set; }
        public int Count { get; set; }
    }
}
