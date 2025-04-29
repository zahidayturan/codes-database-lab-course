CREATE VIEW daily_sales_orders AS
SELECT *
FROM Sales_Order
WHERE S_order_date = CAST(GETDATE() AS DATE);
