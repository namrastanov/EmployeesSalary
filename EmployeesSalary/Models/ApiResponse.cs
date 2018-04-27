namespace EmployeesSalary.Models
{

    public class ApiResponse<T>
    {
        public T Data { get; set; }

        public string Message { get; set; }

        public string ReturnUrl { get; set; }

        public bool Success { get; set; }
    }
}
