using System;
using System.Collections.Generic;

namespace EmployeesSalary.Data.Models
{
    public sealed class PagedCollection<T> : IPagedCollection<T>
    {
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public IEnumerable<T> Items { get; private set; }
        public int TotalCount { get; private set; }

        public PagedCollection(IEnumerable<T> items, int totalCount, int pageSize)
        {
            PageSize = pageSize;
            TotalPages = totalCount / pageSize;

            if (TotalPages * pageSize < totalCount)
                TotalPages++;

            Items = items ?? throw new ArgumentNullException(nameof(items));
            TotalCount = totalCount;
        }
    }
}
