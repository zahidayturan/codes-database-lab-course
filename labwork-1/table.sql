USE Föy1;

-- Employees tablosu
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY,
    FullName NVARCHAR(100),
    BirthDate DATE,
    Salary DECIMAL(10,2),
    IsActive BIT NOT NULL
);

-- Departments tablosu
CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY,
    DepartmentName VARCHAR(100),
    Budget MONEY,
    CreatedDate DATETIME,
    IsRemote CHAR(1) NOT NULL
);

-- Products tablosu
CREATE TABLE Products (
    ProductID INT PRIMARY KEY,
    ProductName NVARCHAR(100),
    Price FLOAT,
    ReleaseDate DATETIME2,
    InStock BIT NOT NULL
);

-- Customers tablosu
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY,
    CustomerName VARCHAR(100),
    Email NVARCHAR(100),
    DateRegistered DATE,
    CreditLimit MONEY,
    IsPreferred BIT NOT NULL
);

-- Orders tablosu
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY,
    OrderDate DATETIME,
    CustomerID INT,
    TotalAmount DECIMAL(10,2),
    IsShipped BIT NOT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);