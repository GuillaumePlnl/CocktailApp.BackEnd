use [COCKTAILDB.MDF]

SELECT quantity, IngredientName, FK_IdDrink, DrinkName
FROM MEASURE
INNER JOIN Ingredient 
on Measure.FK_IdIngredient = Ingredient.PK_Id
INNER JOIN Drink 
on Measure.FK_IdDrink = Drink.PK_Id
group by FK_IdDrink
order by DrinkName


SELECT quantity, IngredientName, FK_IdDrink
FROM MEASURE
full JOIN Ingredient 
on Measure.FK_IdDrink = Ingredient.PK_Id

SELECT quantity, IngredientName, FK_IdDrink
FROM MEASURE
right JOIN Ingredient 
on Measure.FK_IdDrink = Ingredient.PK_Id

SELECT quantity, IngredientName, FK_IdDrink
FROM MEASURE
left JOIN Ingredient 
on Measure.FK_IdDrink = Ingredient.PK_Id

SELECT quantity, IngredientName, FK_IdDrink
FROM MEASURE
inner JOIN Ingredient 
on Measure.FK_IdDrink = Ingredient.PK_Id