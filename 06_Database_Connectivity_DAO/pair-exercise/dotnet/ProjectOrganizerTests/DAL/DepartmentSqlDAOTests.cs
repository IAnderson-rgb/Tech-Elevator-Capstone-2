using System;
using System.Collections.Generic;
using System.Text;
using ProjectOrganizer.DAL;
using ProjectOrganizer.Models;
using ProjectOrganizerTests;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectOrganizerTests.DAL;

namespace ProjectOrganizerTests.DAL
{
    [TestClass]
    public class DepartmentSqlDAOTests: ProjectOrganizerDAOTests
    {
        
        [TestMethod]
        public void GetDepartments_ShouldReturnRightNumberOfDepartments() {
            //Arrange
            const int numberOfDeptsAddedForTests = 1;

            DepartmentSqlDAO dao = new DepartmentSqlDAO(ConnectionString);
            
            //Act
            IList<Department> departments = dao.GetDepartments();

            //Assert
            Assert.AreEqual(numberOfDeptsAddedForTests, departments.Count, "GetDepartments doesn't return correct number for one department");
        }

        [DataTestMethod]
        [DataRow("DeptTest2")]
        public void CreateDepartment_ShouldCreateNewDept(string newName)
        {
            //Arrange
            Department newDept = new Department();
            newDept.Name = newName;
            Department newDept2 = new Department();
            newDept2.Name = newName+'2';


            DepartmentSqlDAO dao = new DepartmentSqlDAO(ConnectionString);


            //Act
            int deptId = dao.CreateDepartment(newDept);
            int deptId2 = dao.CreateDepartment(newDept2);

            //Assert
            Assert.AreEqual(3,dao.GetDepartments().Count, "Department ID not being returned correctly");

        }

        [TestMethod]
        public void UpdateDeparmtment_ShouldChangeDepartmentName() {
            //Arrange
            DepartmentSqlDAO dao = new DepartmentSqlDAO(ConnectionString);
            Department newDepartment = dao.GetDepartments()[0];
            newDepartment.Name = "TestName";
            

            //Act
            dao.UpdateDepartment(newDepartment);
            IList<Department> departments = dao.GetDepartments();

            //Assert
            //departments[0] is the only thing in the database during testing set by sql file
            Assert.AreEqual("TestName", departments[0].Name);
        }

    }
}
