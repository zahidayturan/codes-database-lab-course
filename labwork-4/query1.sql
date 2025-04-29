CREATE VIEW high_tgt_salesmen AS
SELECT * 
FROM Salesman_Master 
WHERE Tgt_to_get > 200;
