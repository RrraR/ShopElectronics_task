create table Categories
(
    Id          int identity
        constraint PK_Categories
            primary key,
    CategoryKey nvarchar(50) not null
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
        constraint DF_ProductCategory_ImageUrl not null
)
go

create table Product
(
    Id          int identity
        constraint PK_Product
            primary key,
    Name        nvarchar(30)                   not null,
    Description nvarchar(150)                  not null,
    ImageURL    nvarchar(max)                  not null,
    Price       decimal(18, 2)                 not null,
    Qty         int                            not null,
    CategoryId  int                            not null
        constraint FK_Product_ProductCategory
            references ProductCategory,
    BrandName   nvarchar(20)                   not null
)
go

create table [User]
(
    Id       int identity
        constraint PK_User
            primary key,
    UserName nvarchar(20)             not null,
    Password nvarchar(30)             not null
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
    Qty       int not null
)
go