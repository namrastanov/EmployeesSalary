﻿using EmployeesSalary.Data.Entities;
using System;

namespace EmployeesSalary.Data.Repositories.IRepositories
{
    interface IEmployeeRepository: IGenericRepository<Employee, Guid>
    {
    }
}
