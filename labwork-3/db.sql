CREATE DATABASE Foy3 ON PRIMARY 
( 
NAME= Foy3, 
FILENAME = '<path>\Foy3_data.mdf', 
SIZE = 16MB, 
MAXSIZE = unlimited, 
FILEGROWTH = 15% 
) 
LOG ON 
( 
NAME= Foy3_log, 
FILENAME = '<path>\Foy3_log.ldf', 
SIZE = 14MB, 
MAXSIZE = unlimited, 
FILEGROWTH = 10% 
)