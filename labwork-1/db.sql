CREATE DATABASE Föy1 ON PRIMARY 
( 
NAME= Föy1, 
FILENAME = '<path>\Föy1_data.mdf', 
SIZE = 16MB, 
MAXSIZE = unlimited, 
FILEGROWTH = 15% 
) 
LOG ON 
( 
NAME= Föy1_log, 
FILENAME = '<path>\Föy1_log.ldf', 
SIZE = 14MB, 
MAXSIZE = unlimited, 
FILEGROWTH = 10% 
)