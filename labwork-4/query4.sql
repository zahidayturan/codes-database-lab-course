SELECT 
    c.name AS client_name,
    p.Description AS product_name
FROM Sales_Order o
JOIN client_master c ON o.Client_no = c.client_no
JOIN Sales_Order_Details od ON o.S_order_no = od.S_order_no
JOIN Product_master p ON od.Product_no = p.Product_no
WHERE o.S_order_date < DATEADD(DAY, -10, CAST(GETDATE() AS DATE));
