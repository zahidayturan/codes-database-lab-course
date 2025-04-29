CREATE DATABASE Foy4 ON PRIMARY 
( 
NAME= Foy4, 
FILENAME = '<path>\Foy4_data.mdf', 
SIZE = 16MB, 
MAXSIZE = unlimited, 
FILEGROWTH = 15% 
) 
LOG ON 
( 
NAME= Foy4_log, 
FILENAME = '<path>\Foy4_log.ldf', 
SIZE = 14MB, 
MAXSIZE = unlimited, 
FILEGROWTH = 10% 
)