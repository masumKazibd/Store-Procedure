CREATE DATABASE spDB
GO
USE spDB
GO

CREATE TABLE Customers
(
	customerId INT IDENTITY PRIMARY KEY,
	name VARCHAR(100) NOT NULL,
	country VARCHAR(50) NOT NULL
)
GO

select name from sys.servers
EXEC sp_addlinkedserver @server = 'HUNTERXBD\SQLEXPRESS'

INSERT INTO Customers
SELECT TOP 10 FirstName,Country FROM [HUNTERXBD\SQLEXPRESS].[Northwind].[dbo].[Employees]
GO
SELECT * FROM Customers
GO

--for Search
CREATE PROC spSearchCustomer @name VARCHAR(50)
AS
BEGIN 
	SELECT * FROM Customers WHERE name LIKE '%'+@name+'%'
END
GO
--SP fOR Insert
CREATE PROC spCustomerInsert @name VARCHAR(50),
							 @country VARCHAR(50)
AS
BEGIN 
	INSERT INTO Customers VALUES(@name,@country)
END
GO
--Test
EXEC spCustomerInsert 'Khairuzzaman Rakib','India'
GO
--sp For Delete
CREATE PROC spDeleteCustomer @customerId INT
AS
BEGIN
	DELETE FROM Customers WHERE customerId=@customerId
END
GO
--sp for Update
CREATE PROC spUpdateCustomer @customerId INT,
							 @name VARCHAR(50),
							 @country VARCHAR(50)
AS
BEGIN
	UPDATE Customers 
	SET name=@name,country=@country 
	WHERE customerId=@customerId
END
GO

