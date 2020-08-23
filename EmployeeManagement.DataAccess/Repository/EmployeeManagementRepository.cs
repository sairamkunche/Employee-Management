using EmployeeManagement.BusinessLogic.CustomExceptions;
using EmployeeManagement.Contracts.Interfaces;
using EmployeeManagement.Contracts.Models.Entities;
using EmployeeManagement.DataAccess.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.DataAccess.Repository
{
    public class EmployeeManagementRepository : IRepository<Employee>
    {
        private readonly EmployeeManagementDBContext _context;
        public EmployeeManagementRepository(EmployeeManagementDBContext context)
        {
            this._context = context;

        }
        public void Delete(int id)
        {
            Employee entityToDelete = _context.Employees.Find(id);
            if(entityToDelete != null)
            {
                _context.Employees.Remove(entityToDelete);
                _context.SaveChanges();
            }
            else
            {
                throw new ItemNotFoundException();
            }
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees;
        }

        public Employee GetById(int id)
        {
            return _context.Employees.Find(id);
        }

        public void Insert(Employee entity)
        {
            _context.Employees.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Employee entity)
        {
            _context.Employees.Update(entity);
             _context.SaveChanges();
        }
    }
}
