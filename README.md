# Product Management System

A web-based Product Management application built with **ASP.NET Core MVC** and **Dapper** (Micro-ORM). This project demonstrates full **CRUD (Create, Read, Update, Delete)** operations using **SQL Server** as the database.

## 🚀 Features

* **View Products:** Display a list of products with details (Name, Price, Description, Created Date).
* **Add Product:** Form to insert new product data into the database.
* **Edit Product:** Update existing product details.
* **Delete Product:** Remove a product from the database.
* **Data Access:** Uses **Dapper** for high-performance SQL execution.
* **UI/UX:** Styled with **Bootstrap 5** for a responsive and clean design.

## 🛠️ Tech Stack

* **Framework:** ASP.NET Core MVC (.NET 10.0)
* **Language:** C#
* **ORM:** Dapper
* **Database:** Microsoft SQL Server Express (2025)
* **Frontend:** HTML5, CSHTML, Bootstrap 5
* **IDE:** Visual Studio 2026

## ⚙️ Setup & Installation Guide

Follow these steps to run the project locally.

### 1. Database Setup
Open **SQL Server Management Studio (SSMS)** and run the following script to create the database and table:

```sql
-- Create Database
CREATE DATABASE ProductDB;
GO

USE ProductDB;
GO

-- Create Table
CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    Price DECIMAL(18, 2) NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE()
);

-- (Optional) Insert Sample Data
INSERT INTO Products (Name, Description, Price) 
VALUES ('iPhone 15', 'Latest Apple smartphone', 1200.00),
       ('Samsung Galaxy S24', 'Android flagship', 1100.00);# ProductManagementApp
```
### 2. Configure Connection String
Open the `appsettings.json` file in the project solution and ensure the connection string matches your SQL Server instance:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=ProductDB;Trusted_Connection=True;TrustServerCertificate=True;"
}

```

*(Note: If your SQL Server instance name is different from `localhost\SQLEXPRESS`, please update the Server part accordingly.)*

### 3. Run the Application

1. Open `ProductManagementApp.sln` in **Visual Studio**.
2. Restore NuGet packages (it should happen automatically).
3. Press **F5** or click the **Run** button (Green Play Icon).
4. Navigate to the Product page (e.g., `https://localhost:xxxx/Product`) to test the CRUD operations.