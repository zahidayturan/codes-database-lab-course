USE Foy4

-- Bağımlı tabloları önce sil
DROP TABLE IF EXISTS Sales_Order_Details;
DROP TABLE IF EXISTS Sales_Order;

-- Bağımsız tabloları sil
DROP TABLE IF EXISTS client_master;
DROP TABLE IF EXISTS Product_master;
DROP TABLE IF EXISTS Salesman_Master;

