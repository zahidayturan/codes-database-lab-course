USE Foy4

-- client_master tablosunu oluşturma
CREATE TABLE client_master (
    client_no VARCHAR(6) PRIMARY KEY,
    name VARCHAR(20),
    address1 VARCHAR(30),
    address2 VARCHAR(30),
    city VARCHAR(15),
    state VARCHAR(15),
    pincode NUMERIC(6),
    bal_due DECIMAL(10,2)
);

-- Product_master tablosunu oluşturma
CREATE TABLE Product_master (
    Product_no VARCHAR(6) PRIMARY KEY CHECK (Product_no LIKE 'P%'),
    Description VARCHAR(25),
    Profit_percent NUMERIC(6, 2),
    Unit_measure VARCHAR(10),
    Qty_on_hand NUMERIC(8),
    Reorder_lvlnumber NUMERIC(8),
    Sell_price NUMERIC(8, 2) NOT NULL CHECK (Sell_price >= 0),
    Cost_price NUMERIC(8, 2) NOT NULL CHECK (Cost_price >= 0)
);

-- Salesman_Master tablosunu oluşturma
CREATE TABLE Salesman_Master (
    Salesman_no VARCHAR(6) PRIMARY KEY CHECK (Salesman_no LIKE 'S%'),
    Sal_name VARCHAR(20) NOT NULL,
    Address VARCHAR(20),
    City VARCHAR(20),
	State VARCHAR(20),
    Pincode NUMERIC(6),
    Sal_amt DECIMAL(8, 2) NOT NULL CHECK (Sal_amt >= 0),
    Tgt_to_get DECIMAL(6, 2) NOT NULL CHECK (Tgt_to_get >= 0),
    Ytd_sales DECIMAL(6, 2) NOT NULL CHECK (Ytd_sales >= 0),
    Remarks VARCHAR(30)
);

-- Sales_Order tablosunu oluşturma
CREATE TABLE Sales_Order (
    S_order_no VARCHAR(6) PRIMARY KEY CHECK (S_order_no LIKE '0%'),
    S_order_date DATE,
    Client_no VARCHAR(6),
    Dely_add VARCHAR(25),
    Salesman_no VARCHAR(6),
    Dely_type CHAR(1) DEFAULT 'F' CHECK (Dely_type IN ('P', 'F')),
    Billed_yn CHAR(1) CHECK (Billed_yn IN ('Y', 'N')),
    Dely_date DATE,
    Order_status VARCHAR(10) CHECK (Order_status IN ('in process', 'fulfilled', 'back order', 'canceled')),
    CONSTRAINT fk_client_no FOREIGN KEY (Client_no) REFERENCES client_master(client_no),
    CONSTRAINT fk_salesman_no FOREIGN KEY (Salesman_no) REFERENCES Salesman_Master(Salesman_no),
    CONSTRAINT chk_dely_date CHECK (Dely_date >= S_order_date)
);

-- Sales_Order_Details tablosunu oluşturma
CREATE TABLE Sales_Order_Details (
    S_order_no VARCHAR(6),
    Product_no VARCHAR(6),
    Qty_order NUMERIC(8),
    Qty_disp NUMERIC(8),
    Product_rate DECIMAL(10, 2),
    PRIMARY KEY (S_order_no, Product_no),
    CONSTRAINT fk_s_order_no FOREIGN KEY (S_order_no) REFERENCES Sales_Order(S_order_no),
    CONSTRAINT fk_product_no FOREIGN KEY (Product_no) REFERENCES Product_master(Product_no)
);

