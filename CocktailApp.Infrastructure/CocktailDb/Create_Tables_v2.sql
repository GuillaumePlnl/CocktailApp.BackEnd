

drop table if exists Drink;

drop table if exists Category;
drop table if exists Glass;
drop table if exists Alcoholic;

drop table if exists Measure;

drop table if exists Ingredient;
drop table if exists Glass;

use [COCKTAILDB.MDF]
delete from Drink;
delete from Category;
delete from Glass;
delete from Alcoholic;
delete from Measure;
delete from Ingredient;
delete from Glass;


--drop database if exists		[C:\USERS\ADMINISTRATEUR\DESKTOP\GUILLAUME\2021-10-08_COCKTAILSAPP2\SOLUTION1\COCKTAILAPP.INFRASTRUCTURE\DB\COCKTAILDB.MDF]


--create database				[C:\USERS\ADMINISTRATEUR\DESKTOP\GUILLAUME\2021-10-08_COCKTAILSAPP2\SOLUTION1\COCKTAILAPP.INFRASTRUCTURE\DB\COCKTAILDB.MDF]
use							[C:\DbsLocalAdmin\CocktailAppDb]


create table Category(	PK_Id uniqueidentifier not null primary key, 
						CategoryName nvarchar(max));

create table Glass(		PK_Id uniqueidentifier not null primary key, 
						GlassName nvarchar(max));

create table Alcoholic(	PK_Id uniqueidentifier not null primary key, 
						AlcoholicName nvarchar(max));

create table Drink(			PK_Id uniqueidentifier not null primary key, 
							DrinkName nvarchar(200),
							UrlPicture nvarchar(200),
							Instruction nvarchar(max),
							IdSource nvarchar(200),
							FK_Category uniqueidentifier not null,
							FK_Glass uniqueidentifier not null,
							FK_Alcoholic uniqueidentifier not null,
							FOREIGN KEY (FK_Category) REFERENCES Category(Pk_Id),
							FOREIGN KEY (FK_Glass) REFERENCES Glass(Pk_Id),
							FOREIGN KEY (FK_Alcoholic) REFERENCES Alcoholic(Pk_Id));

create table Ingredient		(PK_Id uniqueidentifier not null primary key, 
							IngredientName nvarchar(200));

create table Measure(	PK_Id uniqueidentifier not null primary key,
						Quantity nvarchar(200),
						FK_IdIngredient uniqueidentifier not null,
						FK_IdDrink uniqueidentifier not null,

						--FOREIGN KEY (FK_IdDrink) REFERENCES Drink(Pk_Id),
						--FOREIGN KEY (FK_IdIngredient) REFERENCES Ingredient(Pk_Id),
						
						--FOREIGN KEY (FK_IdDrink) REFERENCES Drink(Pk_Id),
						--FOREIGN KEY (FK_IdIngredient) REFERENCES Ingredient(Pk_Id),

						--PRIMARY KEY(FK_IdIngredient, FK_IdDrink)
						);


