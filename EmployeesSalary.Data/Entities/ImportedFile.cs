using EmployeesSalary.Data.Entities.IEntities;
using EmployeesSalary.Data.Types;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesSalary.Data.Entities
{
    public class ImportedFile : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreated { get; set; }

        public string FileName { get; set; }

        public FileImportStatuses Status { get; set; }
    }
}
