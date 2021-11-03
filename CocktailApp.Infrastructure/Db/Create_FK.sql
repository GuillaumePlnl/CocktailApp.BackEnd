use [C:\USERS\ADMINISTRATEUR\DESKTOP\GUILLAUME\2021-10-08_COCKTAILSAPP2\SOLUTION1\COCKTAILAPP.INFRASTRUCTURE\DB\COCKTAILDB.MDF]

-- Cle composite sur table de 
alter table Drink
add constraint FK_Category
foreign key (PK_Id) references Category (Pk_Id);

alter table Drink
add constraint FK_Glass
foreign key (PK_Id) references Glass (Pk_Id);

alter table Drink
add constraint FK_Alcoholic
foreign key (PK_Id) references Alcoholic (Pk_Id);


-- Cles composites sur Table Measure entre ingredient et drink
--alter table Measure
--add constraint FK_IdIngredient
--foreign key (FK_IdIngredient) references Ingredient (Pk_Id);

--alter table Measure
--add constraint FK_IdDrink
--foreign key (FK_IdDrink) references Drink (Pk_Id);


