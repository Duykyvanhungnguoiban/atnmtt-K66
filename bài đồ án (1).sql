CCREATE DATABASE MyDatabase;
GO
USE MyDatabase;
GO
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    EmailAddress VARCHAR(255),
    PasswordHash VARBINARY(256),
    Salt VARBINARY(32)
);
GO

-- Thêm dữ liệu vào bảng Khách hàng
INSERT INTO Customers (CustomerID, FirstName, LastName, EmailAddress, PasswordHash, Salt)
VALUES 
(
    1, 
    'John', 
    'Smith', 
    'john.smith@example.com', 
    HASHBYTES('SHA2_512', 'P@ssw0rd' + CAST(NEWID() AS NVARCHAR(36))), 
    CAST(NEWID() AS VARBINARY(32))
), 
(
    2, 
    'Jane', 
    'Doe', 
    'jane.doe@example.com', 
    HASHBYTES('SHA2_512', 'Secret123' + CAST(NEWID() AS NVARCHAR(36))), 
    CAST(NEWID() AS VARBINARY(32))
);

-- Truy vấn để xác thực thông tin đăng nhập của khách hàng
DECLARE @EmailAddress VARCHAR(255) = 'john.smith@example.com';
DECLARE @Password VARCHAR(100) = 'P@ssw0rd';

SELECT * FROM Customers 
WHERE EmailAddress = @EmailAddress AND PasswordHash = HASHBYTES('SHA2_512', @Password + CAST(Salt AS NVARCHAR(36)))