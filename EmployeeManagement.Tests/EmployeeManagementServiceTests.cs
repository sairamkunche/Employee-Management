using EmployeeManagement.BusinessLogic.CustomExceptions;
using EmployeeManagement.BusinessLogic.Services;
using EmployeeManagement.Contracts.Interfaces;
using EmployeeManagement.Contracts.Models.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit.Sdk;

namespace EmployeeManagement.Tests
{
    [TestClass]
    public class EmployeeManagementServiceTest
    {
        private readonly Mock<IRepository<Employee>> repository = new Mock<IRepository<Employee>>();
        private readonly IEmployeeManagementService _employeeManagementService;
        private List<Employee> employeeInMemoryDatabase;

        public EmployeeManagementServiceTest()
        {           
            repository.Setup(x => x.GetById(It.IsAny<int>()))
           .Returns((int i) => employeeInMemoryDatabase.Single(emp => emp.EmployeeID == i));
            repository.Setup(x => x.GetAll())
           .Returns(() => employeeInMemoryDatabase);
            repository.Setup(x => x.Insert(It.IsAny<Employee>()))
            .Callback((Employee emp) => employeeInMemoryDatabase.Add(emp));
            repository.Setup(x => x.Update(It.IsAny<Employee>()))
           .Callback((Employee employee) =>
            { var toBeUpdated = employeeInMemoryDatabase.Single(emp => emp.EmployeeID == employee.EmployeeID);
                toBeUpdated.EmployeeName = employee.EmployeeName;
                toBeUpdated.PhoneNumber = employee.PhoneNumber;
                toBeUpdated.Skill = employee.Skill;
                toBeUpdated.YearsExperience = employee.YearsExperience;
            });
            repository.Setup(x => x.Delete(It.IsAny<int>()))
            .Callback((int i) =>
            {  
                var tobeDeleted = employeeInMemoryDatabase.Find(emp => emp.EmployeeID == i);
                if (tobeDeleted == null) {
                    throw new ItemNotFoundException();
                }
                else
                {
                    employeeInMemoryDatabase.Remove(tobeDeleted);
                }
            }  );
            _employeeManagementService = new EmployeeManagementService(repository.Object);
        }
        [TestMethod]
        public void TestGetById()
        {
            employeeInMemoryDatabase = new List<Employee>
           {
               new Employee() {EmployeeID = 1, EmployeeName = "Sairam", PhoneNumber="9789878987",Skill="Java", YearsExperience=2},
               new Employee() {EmployeeID = 2, EmployeeName = "kunche", PhoneNumber="9789876797",Skill="C#", YearsExperience=3},
               new Employee() {EmployeeID = 3, EmployeeName = "venkata", PhoneNumber="9789676797",Skill="Python", YearsExperience=4}
           };
            var emp = _employeeManagementService.GetById(3);
            Assert.IsNotNull(emp);
            Assert.AreEqual(emp.EmployeeID, 3);
            Assert.AreEqual(emp.EmployeeName, "venkata");
        }
        [TestMethod]
        public void TestGetAll()
        {
            employeeInMemoryDatabase = new List<Employee>
           {
               new Employee() {EmployeeID = 1, EmployeeName = "Sairam", PhoneNumber="9789878987",Skill="Java", YearsExperience=2},
               new Employee() {EmployeeID = 2, EmployeeName = "kunche", PhoneNumber="9789876797",Skill="C#", YearsExperience=3},
               new Employee() {EmployeeID = 3, EmployeeName = "venkata", PhoneNumber="9789676797",Skill="Python", YearsExperience=4}
           };
            var employees = _employeeManagementService.GetAll();
            Assert.IsNotNull(employees);
            Assert.AreEqual(employees.Count(), 3);
        }

        [TestMethod]
        public void TestInsert()
        {
            employeeInMemoryDatabase = new List<Employee>
           {
               new Employee() {EmployeeID = 1, EmployeeName = "Sairam", PhoneNumber="9789878987",Skill="Java", YearsExperience=2},
               new Employee() {EmployeeID = 2, EmployeeName = "kunche", PhoneNumber="9789876797",Skill="C#", YearsExperience=3},
               new Employee() {EmployeeID = 3, EmployeeName = "venkata", PhoneNumber="9789676797",Skill="Python", YearsExperience=4}
           };
           _employeeManagementService.Insert(new Employee() { EmployeeID = 4, EmployeeName = "john", PhoneNumber = "9789676797", Skill = "Python", YearsExperience = 6 });
            var addedEmp = employeeInMemoryDatabase.Where(p=>p.EmployeeID == 4).FirstOrDefault();
            Assert.IsNotNull(addedEmp);
            Assert.AreEqual(addedEmp.EmployeeName, "john");
        }
        [TestMethod]
        public void TestUpdate()
        {
            employeeInMemoryDatabase = new List<Employee>
           {
               new Employee() {EmployeeID = 1, EmployeeName = "Sairam", PhoneNumber="9789878987",Skill="Java", YearsExperience=2},
               new Employee() {EmployeeID = 2, EmployeeName = "kunche", PhoneNumber="9789876797",Skill="C#", YearsExperience=3},
               new Employee() {EmployeeID = 3, EmployeeName = "venkata", PhoneNumber="9789676797",Skill="Python", YearsExperience=4}
           };
            var tobeUpdated = new Employee() { EmployeeID = 3, EmployeeName = "john", PhoneNumber = "9789676797", Skill = "Python", YearsExperience = 6 };
            _employeeManagementService.Update(tobeUpdated);
            var updatedEmp = employeeInMemoryDatabase.Single(p => p.EmployeeID == 3);
            Assert.AreEqual(updatedEmp.EmployeeName, "john");
        }
        [TestMethod]
        public void TestDelete()
        {
            employeeInMemoryDatabase = new List<Employee>
           {
               new Employee() {EmployeeID = 1, EmployeeName = "Sairam", PhoneNumber="9789878987",Skill="Java", YearsExperience=2},
               new Employee() {EmployeeID = 2, EmployeeName = "kunche", PhoneNumber="9789876797",Skill="C#", YearsExperience=3},
               new Employee() {EmployeeID = 3, EmployeeName = "venkata", PhoneNumber="9789676797",Skill="Python", YearsExperience=4}
           };
            var deleteEmployee = _employeeManagementService.Delete(1);
            Assert.AreEqual(deleteEmployee, "ItemDeleted");
            deleteEmployee = _employeeManagementService.Delete(4);
            Assert.AreEqual(deleteEmployee, "ItemNotFound");
        }

    }
}
