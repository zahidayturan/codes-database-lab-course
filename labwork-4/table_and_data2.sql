USE Foy4

-- Tabloyu oluşturma
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

-- Verileri ekleme
INSERT INTO client_master (client_no, name, city, pincode, state, bal_due)
VALUES 
('0001', 'Ivan', 'Bombay', 400054, 'Maharashtra', 15000),
('0002', 'Vandana', 'Madras', 780001, 'Tamilnadu', 0),
('0003', 'Pramada', 'Bombay', 400057, 'Maharashtra', 5000),
('0004', 'Basu', 'Bombay', 400056, 'Maharashtra', 0),
('0005', 'Ravi', 'Delhi', 100001, NULL, 2000),
('0006', 'Rukmini', 'Bombay', 400050, 'Maharashtra', 0);


-- Product_Master tablosunu oluşturma
CREATE TABLE product_master (
    product_no VARCHAR(6) PRIMARY KEY CHECK (product_no LIKE 'P%'),
    description VARCHAR(25),
    profit_percent NUMERIC(6, 2),
    unit_measure VARCHAR(10),
    qty_on_hand NUMERIC(8),
    reorder_lvl NUMERIC(8),
    sell_price NUMERIC(8, 2) NOT NULL CHECK (sell_price >= 0),
    cost_price NUMERIC(8, 2) NOT NULL CHECK (cost_price >= 0)
);

-- Product_Master tablosuna verileri ekleme
INSERT INTO product_master (product_no, description, profit_percent, unit_measure, qty_on_hand, reorder_lvl, sell_price, cost_price)
VALUES 
('P00001', '1.44Floppies', 5, 'piece', 100, 20, 525, 500),
('P03433', 'Monitors', 6, 'piece', 10, 3, 12000, 11200),
('P06734', 'Mouse', 5, 'piece', 20, 5, 1050, 500),
('P07865', '1.22 Floppies', 5, 'piece', 100, 20, 525, 500),
('P07868', 'Keyboards', 2, 'piece', 10, 3, 3150, 3050),
('P07885', '540 HDD', 4, 'piece', 10, 3, 8400, 8000),
('P07965', '1.44 Drive', 5, 'piece', 10, 3, 1050, 1000),
('P07975', '1.22 Drive', 5, 'piece', 2, 3, 1050, 1000);


-- Salesman_Master tablosunu oluşturma
CREATE TABLE salesman_master (
    salesman_no VARCHAR(6) PRIMARY KEY CHECK (salesman_no LIKE 'S%'),
    salesman_name VARCHAR(20) NOT NULL,
    address VARCHAR(20),
    city VARCHAR(20),
    pincode NUMERIC(6),
    state VARCHAR(20),
    sal_amt DECIMAL(8, 2) NOT NULL CHECK (sal_amt >= 0),
    tgt_to_get DECIMAL(6, 2) NOT NULL CHECK (tgt_to_get >= 0),
    ytd_sales DECIMAL(6, 2) NOT NULL CHECK (ytd_sales >= 0),
    remarks VARCHAR(30)
);

-- Salesman_Master tablosuna verileri ekleme
INSERT INTO salesman_master (salesman_no, salesman_name, address, city, pincode, state, sal_amt, tgt_to_get, ytd_sales, remarks)
VALUES 
('S00001', 'Kiran', 'A/14 worli', 'Bombay', 400002, 'Mah', 3000, 100, 50, 'Good'),
('S00002', 'Manish', '65,nariman', 'Bombay', 400001, 'Mah', 3000, 200, 100, 'Good'),
('S00003', 'Ravi', 'P-7 Bandra', 'Bombay', 400032, 'Mah', 3000, 200, 100, 'Good'),
('S00004', 'Ashish', 'A/5 Juhu', 'Bombay', 400044, 'Mah', 3500, 200, 150, 'Good');

-- Sales_Order tablosunu oluşturma
CREATE TABLE sales_order (
    s_order_no VARCHAR(6) PRIMARY KEY CHECK (s_order_no LIKE '0%'),
    s_order_date DATE,
    client_no VARCHAR(6),
    dely_add VARCHAR(25),
    salesman_no VARCHAR(6),
    dely_type CHAR(1) DEFAULT 'F' CHECK (dely_type IN ('P', 'F')),
    billed_yn CHAR(1) DEFAULT 'F' CHECK (billed_yn IN ('Y', 'N')),
    dely_date DATE,
    order_status VARCHAR(10) CHECK (order_status IN ('in process', 'fulfilled', 'back order', 'canceled')),
    CONSTRAINT fk_client_no FOREIGN KEY (client_no) REFERENCES client_master(client_no),
    CONSTRAINT fk_salesman_no FOREIGN KEY (salesman_no) REFERENCES salesman_master(salesman_no),
    CONSTRAINT chk_dely_date CHECK (dely_date >= s_order_date)
);

-- Sales_Order tablosuna verileri ekleme
INSERT INTO sales_order (s_order_no, s_order_date, client_no, dely_add, salesman_no, dely_type, billed_yn, dely_date, order_status)
VALUES 
('019001', '1996-01-12', '0001', NULL, 'S00001', 'F', 'N', '1996-01-20', 'in process'),
('019002', '1996-01-25', '0002', NULL, 'S00002', 'P', 'N', '1996-01-27', 'canceled'),
('016865', '1996-02-18', '0003', NULL, 'S00003', 'F', 'Y', '1996-02-20', 'fulfilled'),
('019003', '1996-04-03', '0001', NULL, 'S00001', 'F', 'Y', '1996-04-07', 'fulfilled'),
('046866', '1996-05-20', '0004', NULL, 'S00002', 'P', 'N', '1996-05-22', 'canceled'),
('010008', '1996-05-24', '0005', NULL, 'S00004', 'F', 'N', '1996-05-26', 'in process');

-- Sales_Order_Details tablosunu oluşturma
CREATE TABLE sales_order_details (
    s_order_no VARCHAR(6),
    product_no VARCHAR(6),
    qty_order NUMERIC(8),
    qty_disp NUMERIC(8),
    product_rate DECIMAL(10, 2),
    PRIMARY KEY (s_order_no, product_no),
    CONSTRAINT fk_s_order_no FOREIGN KEY (s_order_no) REFERENCES sales_order(s_order_no),
    CONSTRAINT fk_product_no FOREIGN KEY (product_no) REFERENCES product_master(product_no)
);

-- Sales_Order_Details tablosuna verileri ekleme
INSERT INTO sales_order_details (s_order_no, product_no, qty_order, qty_disp, product_rate)
VALUES 
('019001', 'P00001', 4, 4, 525),
('019001', 'P07965', 2, 1, 8400),
('019001', 'P07885', 2, 1, 5250),
('019002', 'P00001', 10, 0, 525),
('046865', 'P07868', 3, 3, 3150),
('046865', 'P07885', 10, 10, 5250),
('019003', 'P00001', 4, 4, 1050),
('019003', 'P03453', 2, 2, 1050),
('046866', 'P06734', 1, 1, 12000),
('046866', 'P07965', 1, 0, 8400),
('010008', 'P07975', 1, 0, 1050),
('010008', 'P00001', 10, 5, 525);