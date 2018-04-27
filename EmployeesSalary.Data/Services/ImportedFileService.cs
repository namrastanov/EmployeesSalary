using EmployeesSalary.Data.Entities;
using EmployeesSalary.Data.Services.IServices;
using EmployeesSalary.Data.Types;
using EmployeesSalary.Data.UnitOfWork;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace EmployeesSalary.Data.Services
{
    public class ImportedFileService : IImportedFileService
    {
        private IUnitOfWork _unitOfWork;

        public ImportedFileService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddFileAsync(IFormFile file)
        {
            var draftFile = await _unitOfWork.ImportedFileRepository.InsertAsync(
                new ImportedFile
                {
                    FileName = file.FileName,
                    Status = FileImportStatuses.Started,
                    DateCreated = DateTime.Now
                });

            await _unitOfWork.CommitAsync();

            return draftFile.Id;
        }

        public async Task<FileImportStatuses> CheckFileStatusAsync(int fileId)
        {
            var file = await _unitOfWork.ImportedFileRepository.GetByIdAsync(fileId);

            return file.Status;
        }

        public async Task SetFileStatusAsync(int id, FileImportStatuses status)
        {
            var entity = new ImportedFile
            {
                Id = id,
                Status = status
            };

            _unitOfWork.ImportedFileRepository.ApplyCurrentValues(entity, e => e.Status);

            await _unitOfWork.CommitAsync();
        }

        public void Dispose()
        {
            if (_unitOfWork != null)
            {
                _unitOfWork.Dispose();
                _unitOfWork = null;
            }
        }
    }
}
