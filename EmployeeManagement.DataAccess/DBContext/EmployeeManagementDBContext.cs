using EmployeeManagement.Contracts.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.DataAccess.DBContext
{
    public partial class EmployeeManagementDBContext : DbContext
    {
        public EmployeeManagementDBContext()
        {
        }

        public EmployeeManagementDBContext(DbContextOptions<EmployeeManagementDBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Employee> Employees { get; set; }

    }
}
