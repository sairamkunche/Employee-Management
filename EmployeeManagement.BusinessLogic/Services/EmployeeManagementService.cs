using EmployeeManagement.BusinessLogic.CustomExceptions;
using EmployeeManagement.Contracts.Interfaces;
using EmployeeManagement.Contracts.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeManagement.BusinessLogic.Services
{
    public class EmployeeManagementService : IEmployeeManagementService
    {
        private readonly IRepository<Employee> _repository;
        public EmployeeManagementService(IRepository<Employee> repository)
        {
            if (repository != null)
            {
                this._repository = repository;
            }
        }

        public string Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                return "ItemDeleted";
            }
            catch (ItemNotFoundException ex)
            {
                return "ItemNotFound";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public IEnumerable<Employee> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public Employee GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Insert(Employee entity)
        {
            _repository.Insert(entity);
        }

        public void Update(Employee entity)
        {
            _repository.Update(entity);
        }
    }
}
