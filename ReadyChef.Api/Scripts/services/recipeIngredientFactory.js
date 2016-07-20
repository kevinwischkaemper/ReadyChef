angular.module('readyChefApp')
    .service('recipeIngredientFactory', recipeIngredientFactory);
recipeIngredientFactory.$inject=['ingredientFactory','recipeIngredient'];
function recipeIngredientFactory(ingredientFactory, recipeIngredient) {
    
    function makeNewRecipeIngredient(ingredientName, amount, amountUnits) {
        var vm = this;
        vm.amount = amount;
        vm.amountUnits = amountUnits;
        return ingredientFactory.makeNewIngredient(ingredientName)
                    .then(function (ingredient) {
                        return new recipeIngredient(ingredient, vm.amount, vm.amountUnits);
                    });
    };

    return {
        makeNewRecipeIngredient: makeNewRecipeIngredient
    };
};