

drop table if exists Drink;

drop table if exists Category;
drop table if exists Glass;
drop table if exists Alcoholic;

drop table if exists Measure;

drop table if exists Ingredient;
drop table if exists Glass;


--drop database if exists		[C:\USERS\ADMINISTRATEUR\DESKTOP\GUILLAUME\2021-10-08_COCKTAILSAPP2\SOLUTION1\COCKTAILAPP.INFRASTRUCTURE\DB\COCKTAILDB.MDF]
--create database				[C:\USERS\ADMINISTRATEUR\DESKTOP\GUILLAUME\2021-10-08_COCKTAILSAPP2\SOLUTION1\COCKTAILAPP.INFRASTRUCTURE\DB\COCKTAILDB.MDF]
--use							[C:\USERS\ADMINISTRATEUR\DESKTOP\GUILLAUME\2021-10-08_COCKTAILSAPP2\SOLUTION1\COCKTAILAPP.INFRASTRUCTURE\DB\COCKTAILDB.MDF]


create table Category(PK_Id uniqueidentifier not null primary key, CategoryName nvarchar(max));

create table Glass(PK_Id uniqueidentifier not null primary key, GlassName nvarchar(max));

create table Alcoholic(PK_Id uniqueidentifier not null primary key, AlcoholicName nvarchar(max));

create table Measure(		Quantity int,
							FK_IdIngredient uniqueidentifier,
							FK_IdDrink uniqueidentifier,
							PRIMARY KEY (FK_IdIngredient, FK_IdDrink));

create table Drink(			PK_Id uniqueidentifier not null primary key, 
							DrinkName nvarchar(200),
							UrlPicture nvarchar(200),
							Instruction nvarchar(max),
							IdSource nvarchar(200),
							FK_Category nvarchar(max),
							FK_Glass nvarchar(max),
							FK_Alcoholic nvarchar(max)
							);

create table Ingredient		(PK_Id uniqueidentifier not null primary key, IngredientName nvarchar(200));
