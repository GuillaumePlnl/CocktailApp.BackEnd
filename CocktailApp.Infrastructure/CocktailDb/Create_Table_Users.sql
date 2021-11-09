use [COCKTAILDB.MDF]

drop table Users;


create table Users(	PK_Id int not null primary key, 
							FirstName nvarchar(200),
							LastName nvarchar(200),
							UserType int,

							UserName nvarchar(200),
							Password nvarchar(200));

INSERT INTO Users (PK_Id, FirstName, LastName, UserType, UserName, Password)
 VALUES
 (1, 'Rébecca', 'Armand', 1, 'Rebec', 'sunflower'),
 (2, 'Hilaire', 'Heloy', 1, 'Hil', 'sunflower'),
 (3, 'Marielle', 'Denee', 2, 'Mari', 'sunflower'),
 (4, 'Justin', 'Bridou', 4, 'Just', 'sunflower');
