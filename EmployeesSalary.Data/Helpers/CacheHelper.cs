using System.Threading;

namespace EmployeesSalary.Data.Helpers
{
    public class CacheHelper
    {
        private static long totalSalary = 0;

        public static void UpdateTotalSalary(long salary)
        {
            Interlocked.Add(ref totalSalary, salary);
        }

        public static long GetTotalSalary()
        {
            return Thread.VolatileRead(ref totalSalary);
        }
    }
}
