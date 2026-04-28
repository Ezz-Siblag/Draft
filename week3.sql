CREATE DATABASE DropZoneDB;
USE DropZoneDB;

CREATE TABLE Sellers (
    SellerID INT PRIMARY KEY AUTO_INCREMENT,
    SellerName VARCHAR(50),
    DateAdded VARCHAR(20)
);

CREATE TABLE Items (
    ItemID INT PRIMARY KEY AUTO_INCREMENT,
    SellerID INT,
    BuyerName VARCHAR(50),
    DateDropped VARCHAR(20),
    price DECIMAL(10,2),
    holdingFee INT,
    itemStatus VARCHAR(30),
    placed VARCHAR(50),
    dateCashOut VARCHAR(20),
    FOREIGN KEY (SellerID) REFERENCES Sellers(SellerID)
);

INSERT INTO Sellers (SellerName, DateAdded) VALUES
("MOK", "February 13, 2026");

INSERT INTO Sellers (SellerName, DateAdded) VALUES
("Cassie", "February 31, 2026"),
("Slick Pick", "March 1, 2026");

SElect * from Sellers;

INSERT INTO Items (SellerID, BuyerName, DateDropped, Price, holdingFee,
 itemstatus, placed) VALUES
 (1, "Ezz", "March 30, 2026", 300, 10, "InDA", "Rack A");
 
 SELECT * FROM ITEMS;
 
 Delete  from Items where ItemID = 2;