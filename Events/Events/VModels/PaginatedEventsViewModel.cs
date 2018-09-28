using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.VModels
{
    public class PaginatedEventsViewModel<TEntity>
        where TEntity : class
    {
        public int PageSize { get; private set; }
        public int PageIndex { get; private set; }
        public long Count { get; private set; }
        public IEnumerable<TEntity> Data { get; set; }
        public PaginatedEventsViewModel(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
        {
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.Count = count;
            this.Data = data;
        }
    }
}
