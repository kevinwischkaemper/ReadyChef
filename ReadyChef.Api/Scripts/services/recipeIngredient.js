angular.module('readyChefApp')
    .service('recipeIngredient', recipeIngredient);
recipeIngredient.$inject = [];
function recipeIngredient() {
    function newRecipeIngredient(ingredient, amount, amountUnits) {
        this.ingredient = ingredient;
        this.amount = amount;
        this.amountUnits = amountUnits;
    };
    return newRecipeIngredient;
};