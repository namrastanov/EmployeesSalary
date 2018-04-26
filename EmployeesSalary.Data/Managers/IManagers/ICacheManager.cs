namespace EmployeesSalary.Data.Managers.IManagers
{
    public interface ICacheManager
    {
        void UpdateTotalSalary(long salary);

        long GetTotalSalary();
    }
}
