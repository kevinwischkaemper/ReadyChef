angular.module('readyChefApp')
    .service('recipeIngredientFactory', recipeIngredientFactory);
recipeIngredientFactory.$inject=['ingredientFactory'];
function recipeIngredientFactory(ingredientFactory, recipeIngredient) {
    
    function makeNewRecipeIngredient(ingredientName, amount, amountUnits) {
        return ingredientFactory.makeNewIngredient(ingredientName)
                    .then(function (ingredient) {
                        return {amount:amount,amountUnits:amountUnits, ingredient:ingredient}
                    });
    };

    return {
        makeNewRecipeIngredient: makeNewRecipeIngredient
    };
};