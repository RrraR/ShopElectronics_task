create table Brands
(
    Id   int identity
        constraint PK_Brands
            primary key,
    Name nvarchar(50) not null
)
go

create table Categories
(
    Id          int identity
        constraint PK_Categories
            primary key,
    CategoryKey nvarchar(50) not null
)
go

create table OrderStatus
(
    Id     int identity
        constraint PK_OrderStatus
            primary key,
    Status nvarchar(50) not null
)
go

create table ProductCategory
(
    Id            int identity
        constraint PK_ProductCategory
            primary key,
    Name          nvarchar(50) not null,
    CategoryKeyId int
        constraint DF_ProductCategory_CategoryKey default 1 not null
        constraint FK_ProductCategory_Categories
            references Categories,
    ImageUrl      nvarchar(max)
        constraint DF_ProductCategory_ImageUrl default 'data:image/jpeg;base64,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' not null
)
go

create table Product
(
    Id          int identity
        constraint PK_Product
            primary key,
    Name        nvarchar(30)   not null,
    Description nvarchar(150)  not null,
    ImageURL    nvarchar(max)  not null,
    Price       decimal(18, 2) not null,
    Qty         int            not null,
    CategoryId  int            not null
        constraint FK_Product_ProductCategory
            references ProductCategory,
    BrandId     int default 1  not null
        constraint FK_Product_Brands
            references Brands
)
go

create table [User]
(
    Id       int identity
        constraint PK_User
            primary key,
    UserName nvarchar(20)             not null,
    Password nvarchar(30) default '1' not null
)
go

create table Cart
(
    Id     int identity
        constraint PK_Cart
            primary key,
    UserId int not null
        constraint FK_Cart_User
            references [User]
)
go

create table CartItem
(
    Id        int identity
        constraint PK_CartItem
            primary key,
    CartId    int not null
        constraint FK_CartItem_Cart
            references Cart,
    ProductId int not null
        constraint FK_CartItem_Product
            references Product,
    Qwt       int not null
)
go

create table Orders
(
    Id            int identity
        constraint PK_Orders
            primary key,
    UserId        int not null
        constraint FK_Orders_User
            references [User],
    OrderStatusId int not null
        constraint FK_Orders_OrderStatus
            references OrderStatus
)
go

create table OrderItems
(
    Id        int identity
        constraint PK_OrderItems
            primary key,
    ProductId int not null
        constraint FK_OrderItems_Product
            references Product,
    Qwt       int not null,
    OrderId   int not null
        constraint FK_OrderItems_Orders
            references Orders
)
go

