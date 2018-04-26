using System.Collections.Generic;

namespace EmployeesSalary.Data.Models
{
    public interface IPagedCollection<out T>
    {
        int TotalCount { get; }

        int TotalPages { get; }

        int PageSize { get; }

        IEnumerable<T> Items { get; }
    }
}
