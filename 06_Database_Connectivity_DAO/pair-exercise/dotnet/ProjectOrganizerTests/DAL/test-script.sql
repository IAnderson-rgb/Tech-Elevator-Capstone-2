-- Delete all of the data
DELETE FROM project_employee;
--UPDATE country SET capital = NULL;
DELETE FROM employee;
DELETE FROM department;
DELETE FROM project;

-- Insert a fake department
INSERT INTO department (name)
VALUES ('TestDept');
DECLARE @newDeptId int = (SELECT @@IDENTITY);

-- Insert a fake employee
INSERT INTO employee (department_id, first_name, last_name,job_title,birth_date, gender, hire_date)
VALUES (@newDeptId, 'John', 'Smith','Tester', '2000-02-18','M','2020-01-01');
DECLARE @newEmployeeId int = (SELECT @@IDENTITY);

-- Insert a fake project
INSERT INTO project (name,from_date,to_date)
VALUES ('TestProject', '2019-12-12', '2020-01-01');
DECLARE @newProjectId int = (SELECT @@IDENTITY);

-- Return the id of the fake city
INSERT INTO project_employee(project_id,employee_id)
VALUES (@newProjectId,@newEmployeeId);