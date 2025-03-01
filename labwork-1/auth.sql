create login Föy1User with password = '123', default_database = Föy1

USE Föy1;

CREATE USER Föy1User FOR LOGIN Föy1User;

-- GRANT SELECT, INSERT, DELETE, UPDATE ON Employees TO Föy1User;

GRANT SELECT, INSERT, DELETE, UPDATE ON DATABASE::Föy1 TO Föy1User;

REVOKE ALL ON DATABASE::Föy1 FROM FöyUser1;

DROP USER Föy1User;

-- USE master;

-- DROP LOGIN Föy1User;