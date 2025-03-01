USE Föy1;

-- Employees tablosuna veri ekleme
INSERT INTO Employees (EmployeeID, FullName, BirthDate, Salary, IsActive) VALUES
(1, N'Ahmet Yılmaz', '1985-06-15', 7500.00, 1),
(2, N'Ayşe Demir', '1990-09-20', 6200.50, 1),
(3, N'Mehmet Kaya', '1978-04-10', 9800.75, 0),
(4, N'Elif Şahin', '1995-11-25', 5400.00, 1),
(5, N'Burak Çelik', '1982-02-18', 8700.00, 0);

-- Departments tablosuna veri ekleme
INSERT INTO Departments (DepartmentID, DepartmentName, Budget, CreatedDate, IsRemote) VALUES
(1, 'IT', 150000, '2023-01-10', 'N'),
(2, 'HR', 80000, '2022-06-15', 'Y'),
(3, 'Finance', 200000, '2021-09-05', 'N'),
(4, 'Marketing', 120000, '2023-03-22', 'Y'),
(5, 'Sales', 95000, '2020-12-30', 'N');

-- Products tablosuna veri ekleme
INSERT INTO Products (ProductID, ProductName, Price, ReleaseDate, InStock) VALUES
(1, N'Laptop', 1500.99, '2022-05-10', 1),
(2, N'Mouse', 25.50, '2021-12-15', 1),
(3, N'Keyboard', 45.75, '2020-09-20', 1),
(4, N'Monitor', 300.99, '2023-02-01', 0),
(5, N'Printer', 120.00, '2019-07-14', 1);

-- Customers tablosuna veri ekleme
INSERT INTO Customers (CustomerID, CustomerName, Email, DateRegistered, CreditLimit, IsPreferred) VALUES
(1, 'Ali Can', 'ali.can@example.com', '2021-05-20', 5000, 1),
(2, 'Zeynep Kurt', 'zeynep.kurt@example.com', '2022-11-11', 3000, 0),
(3, 'Emre Aydın', 'emre.aydin@example.com', '2023-06-05', 7000, 1),
(4, 'Hakan Demir', 'hakan.demir@example.com', '2019-09-25', 2000, 0),
(5, 'Merve Yıldız', 'merve.yildiz@example.com', '2020-01-18', 6000, 1);

-- Orders tablosuna veri ekleme
INSERT INTO Orders (OrderID, OrderDate, CustomerID, TotalAmount, IsShipped) VALUES
(1, '2024-01-05', 1, 1525.99, 1),
(2, '2023-12-20', 2, 300.00, 0),
(3, '2023-11-15', 3, 700.50, 1),
(4, '2024-02-10', 4, 1200.75, 0),
(5, '2023-10-30', 5, 950.25, 1);
