using EmployeeManagement.Contracts.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Contracts.Interfaces
{
    public interface IEmployeeManagementService
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(int id);
        void Insert(Employee entity);
        void Update(Employee entity);
        string Delete(int id);

    }
}
