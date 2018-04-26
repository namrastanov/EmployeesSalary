using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesSalary.Data.Entities.IEntities
{
    public interface IEntity<TId>
        where TId : struct, IEquatable<TId>
    {
        TId Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        DateTime DateCreated { get; set; }
    }
}
