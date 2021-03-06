﻿using EmployeesSalary.Data.Entities;
using EmployeesSalary.Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeesSalary.Data.Repositories
{
    public class EmployeeRepository: GenericRepository<Employee, Guid>, IEmployeeRepository
    {
        public EmployeeRepository(DbContext dbContext): base(dbContext)
        {

        }
    }
}
