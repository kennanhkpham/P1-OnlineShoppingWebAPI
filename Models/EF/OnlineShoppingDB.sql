create database OnlineShoppingDB
use OnlineShoppingDB

create table Product(
	pID int primary key identity(1,1),
	pName varchar(50) not null,
	pCategory varchar(50) not null, 
	pPrice decimal(10,2) not null, 
	pQty int not null, 
	pInStock bit not null
);

create table Orders (
    oID INT PRIMARY KEY IDENTITY(1,1),
    customerID INT, 
    OrderDate DATE DEFAULT GETDATE(),
    totalAmount DECIMAL(10,2),
    status VARCHAR(50) DEFAULT 'Submitted'
);

insert into Product(pName,pCategory,pPrice,pQty,pInStock)
values 
('Tickle Me Elmo', 'Toys', 19.99, 20, 1),
('LEGO Star Wars X-Wing', 'Toys', 49.99, 15, 1),
('Barbie Dreamhouse', 'Toys', 129.99, 5, 1),
('Monopoly Board Game', 'Toys', 24.99, 30, 1),
('Nerf Blaster Elite', 'Toys', 14.95, 25, 1),
('Wireless Headphones', 'Electronics', 89.99, 40, 1),
('Smartphone 128GB', 'Electronics', 699.00, 12, 1),
('4K Ultra HD Smart TV', 'Electronics', 349.99, 8, 1),
('Bluetooth Pocket Speaker', 'Electronics', 29.99, 50, 1),
('Mechanical Keyboard', 'Electronics', 74.50, 0, 0),
('Classic Denim Jacket', 'Apparel', 59.99, 18, 1),
('Running Sneakers', 'Apparel', 85.00, 22, 1),
('Cotton Crewneck T-Shirt', 'Apparel', 14.99, 100, 1),
('Leather Bi-Fold Wallet', 'Apparel', 24.95, 35, 1),
('Stainless Steel Coffee Maker', 'Home & Kitchen', 45.99, 14, 1),
('Memory Foam Pillow', 'Home & Kitchen', 34.99, 28, 1),
('Air Fryer 5-Quart', 'Home & Kitchen', 89.95, 10, 1),
('Scented Soy Candle', 'Home & Kitchen', 12.50, 60, 1),
('The Great Gatsby Paperback', 'Books', 9.99, 45, 1),
('Hardcover Daily Planner', 'Stationery', 16.50, 0, 0),
('Gel Pens 12-Pack', 'Stationery', 8.99, 75, 1);


DELETE FROM Product 

select * from Product

drop table Orders

drop table Product