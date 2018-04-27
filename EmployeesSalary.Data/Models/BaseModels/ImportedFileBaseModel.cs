using EmployeesSalary.Data.Types;

namespace EmployeesSalary.Data.Models.BaseModels
{
    public class ImportedFileBaseModel
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public FileImportStatuses Status { get; set; }
    }
}
