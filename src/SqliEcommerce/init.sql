USE master;
GO
CREATE LOGIN zakaria WITH PASSWORD = 'zakaria@123!';
GO
USE OrderDb;  -- Replace with your actual database name
GO
CREATE USER zakaria FOR LOGIN zakaria;
GO
GRANT SELECT, INSERT, UPDATE, DELETE ON OrderDb TO zakaria;  -- Adjust permissions as needed
GO
