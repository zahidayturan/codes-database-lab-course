USE Foy4

CREATE TABLE Employee (
    EmpID INT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
	Surname VARCHAR(50) NOT NULL,
    Age INT CHECK (Age >= 18),
    Email VARCHAR(100) UNIQUE
);
