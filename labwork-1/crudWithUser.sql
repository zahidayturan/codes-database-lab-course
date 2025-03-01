EXECUTE AS USER = 'Föy1User';
INSERT INTO Employees (EmployeeID, FullName, BirthDate, Salary, IsActive) 
VALUES (6, 'Test Kullanıcı', '1992-08-14', 5000, 1);
REVERT;

EXECUTE AS USER = 'Föy1User';
DELETE FROM Employees WHERE EmployeeID = 6;
REVERT;

EXECUTE AS USER = 'Föy1User';
UPDATE Employees SET Salary = 8000 WHERE EmployeeID = 2;
REVERT;