CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Email NVARCHAR(100),
    City NVARCHAR(50)
);

INSERT INTO Customers (CustomerID, FirstName, LastName, Email, City) VALUES
(1, 'Ahmet', 'Yılmaz', 'ahmet.yilmaz@example.com', 'İstanbul'),
(2, 'Mehmet', 'Demir', 'mehmet.demir@example.com', 'Ankara'),
(3, 'Ayşe', 'Kara', 'ayse.kara@example.com', 'İzmir'),
(4, 'Ahmet', 'Yılmaz', 'ahmet.yilmaz2@example.com', 'İstanbul'), -- Aynı ad-soyadlı başka bir kişi
(5, 'Zeynep', 'Aydın', 'zeynep.aydin@example.com', 'Bursa');


-- FirstName ve LastName alanlarına non-clustered index ekliyoruz
CREATE NONCLUSTERED INDEX IDX_Customers_Name
ON Customers (FirstName, LastName);

-- City alanına da ayrı bir index ekliyoruz
CREATE NONCLUSTERED INDEX IDX_Customers_City
ON Customers (City);

-- İsim bazlı arama (IDX_Customers_Name indeksini kullanır)
SELECT CustomerID, FirstName, LastName
FROM Customers
WHERE FirstName = 'Ahmet' AND LastName = 'Yılmaz';

-- Şehre göre filtreleme (IDX_Customers_City indeksini kullanır)
SELECT *
FROM Customers
WHERE City = 'İstanbul';