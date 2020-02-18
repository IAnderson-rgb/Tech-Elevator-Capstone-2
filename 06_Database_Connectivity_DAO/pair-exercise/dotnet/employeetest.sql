SELECT * FROM project;

SELECT * FROM employee;

select * FROM project_employee;
select * FROM project_employee WHERE project_id =2 AND employee_id =4;

BEGIN TRANSACTION
INSERT INTO project (name, from_date, to_date) VALUES('cleverName', '2020-12-12', '2020-11-11');

ROLLBACK TRANSACTION

SELECT MAX(project_id) FROM project



UPDATE department SET name = 'Depart1' WHERE name = 'Tech';
SELECT * FROM department;

SELECT * FROM employee WHERE first_name LIKE '%Flo%' OR last_name LIKE '%Coty%';

SELECT * FROM employee LEFT JOIN project_employee ON employee.employee_id = project_employee.employee_id WHERE project_employee.project_id IS NULL