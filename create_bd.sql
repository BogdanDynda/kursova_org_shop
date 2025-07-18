create database if not exists print_shop
character set utf8mb4;
use print_shop;

CREATE TABLE Employee (
    employee_id INT AUTO_INCREMENT PRIMARY KEY,
    last_name VARCHAR(100) NOT NULL,
    login VARCHAR(50) UNIQUE NOT NULL,
    password_ VARCHAR(100) NOT NULL,
    position_ VARCHAR(50) NOT NULL
);
CREATE TABLE Discount (
    discount_id INT AUTO_INCREMENT PRIMARY KEY,
    discount_code VARCHAR(20) UNIQUE NOT NULL,
    percentage INT NOT NULL
);
CREATE TABLE Manufacturer (
    manufacturer_id INT AUTO_INCREMENT PRIMARY KEY,
    name_ VARCHAR(100) NOT NULL,
    country VARCHAR(100) NOT NULL
);
CREATE TABLE Product (
    product_id INT AUTO_INCREMENT PRIMARY KEY,
    name_ VARCHAR(100) NOT NULL,
    description_ TEXT,
    stock_quantity INT NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    manufacturer_id INT NOT NULL,
    discount_id INT,
    FOREIGN KEY (manufacturer_id) REFERENCES Manufacturer(manufacturer_id),
    FOREIGN KEY (discount_id) REFERENCES Discount(discount_id)
);
CREATE TABLE Order_ (
    order_id INT AUTO_INCREMENT PRIMARY KEY,
    order_datetime DATETIME NOT NULL,
    employee_id INT NOT NULL,
    FOREIGN KEY (employee_id) REFERENCES Employee(employee_id)
);
CREATE TABLE Order_Items (
    list_item_id INT AUTO_INCREMENT PRIMARY KEY,
    order_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (order_id) REFERENCES Order_(order_id),
    FOREIGN KEY (product_id) REFERENCES Product(product_id)
);

drop TABLE order_items;
drop TABLE Order_;
drop TABLE Employee;
drop TABLE Product;
drop TABLE Discount;
drop TABLE Manufacturer;