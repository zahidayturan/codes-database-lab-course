CREATE DATABASE NHL_DB ON PRIMARY 
( 
NAME= NHL_DB, 
FILENAME = '<path>\NHL_DB_data.mdf', 
SIZE = 16MB, 
MAXSIZE = unlimited, 
FILEGROWTH = 15% 
) 
LOG ON 
( 
NAME= NHL_DB_LOG, 
FILENAME = '<path>\NHL_DB_LOG.ldf', 
SIZE = 14MB, 
MAXSIZE = unlimited, 
FILEGROWTH = 10% 
)
