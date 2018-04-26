using EmployeesSalary.Data.Managers.IManagers;
using System.Threading;

namespace EmployeesSalary.Data.Managers
{
    public class CacheManager : ICacheManager
    {
        private static long totalSalary = 0;

        public void UpdateTotalSalary(long salary)
        {
            Interlocked.Add(ref totalSalary, salary);
        }

        public long GetTotalSalary()
        {
            return Interlocked.Read(ref totalSalary);
        }
    }
}
