# ShopElectronics_task

## About project
Projects acts as a electronics shop.
 User does not need to register or login to add items to cart however login is needed to place an order.

## Confuguration

#### Frontend 
Frontend of the project is witten in react.  

In the project directory, you can run:

```bash
  npm start
```

Runs the app in the development mode.\
Open [http://localhost:3000](http://localhost:3000) to view it in your browser.

#### Backend
Backend is witten in C# using Web.Api and does not requre any additional settings

This project uses MSSQL and MSSQL managment studio 18
1. Change database settings in [appsettings.json](https://github.com/RrraR/ShopElectronics_task/blob/master/ShopElectronics/appsettings.json)

```csharp
  "ConnectionStrings": {"Default": "your string here"}
```
2. Run [CreateDatabase.sql](https://github.com/RrraR/ShopElectronics_task/blob/master/Database/CreateDatabase.sql) file to create database
3. Run [CreateTables.sql](https://github.com/RrraR/ShopElectronics_task/blob/master/Database/CreateTables.sql) to create tables.
4. Run [FillTables.sql](https://github.com/RrraR/ShopElectronics_task/blob/master/Database/FillTables.sql) if you want to populete datadase automaticaly.

![Database structure](DatabaseStructure.png?raw=true "Database Structure")


## Architecture

1. Project uses Repository-Service pattern

2. Project contains 4 layers:
 - [Frontend UI](https://github.com/RrraR/ShopElectronics_task/tree/master/shopelectronics.ui) - displays pages to user
 - [Api](https://github.com/RrraR/ShopElectronics_task/tree/master/ShopElectronics) - controllers in this layer manage communications between Services and Frontend
 - [service layer](https://github.com/RrraR/ShopElectronics_task/tree/master/ShopElectronics.Services) - main function is to access repositories to manipulate data
 - [data access](https://github.com/RrraR/ShopElectronics_task/tree/master/ShopElectronics.Data)(repository layer) - has access to database and is used to get required data

 3. Entity Framework Core is used to communicate with database

 4. [Models](https://github.com/RrraR/ShopElectronics_task/tree/master/ShopElectronics.Services/Models) are used to pass data between controller and Frontend


## Project Features

#### Without login/registration
- add products to cart
- view shop categories, search/filter products in the shop
- on checkout if regestration is succesfull items that are in your cart will be added to the Database
- on checkout if login is succesfull items that are in your cart will be added to the Database and if user already had items in the cart before login all the items will be displayed


#### With login/registration
- add products to cart
- view shop categories, search/filter products in the shop
- on checkout order is created
- user can view all theis orders in [OrderHistoryPage](https://github.com/RrraR/ShopElectronics_task/blob/master/shopelectronics.ui/src/Pages/OrderHistoryPage.js)


#### Admin
- admin can do all the things listed above 
- admin can view [AdminPage](https://github.com/RrraR/ShopElectronics_task/blob/master/shopelectronics.ui/src/Pages/AdminPage.js) on it admin can see all orders in the shop and change order status
