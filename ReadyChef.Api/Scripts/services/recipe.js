angular.module('readyChefApp')
    .service('recipe',recipe);
recipe.$inject=['recipeIngredientFactory'];
function recipe(recipeIngredientFactory){

    function makeNewRecipe(){
        var vm = this;
        vm.name = "";
        vm.recipeIngredients = [];
        vm.readyTime = "";
        vm.addRecipeIngredient = addRecipeIngredient;
        vm.id = "";

        function addRecipeIngredient(ingredientName, amount, amountUnits) {
            recipeIngredientFactory.makeNewRecipeIngredient(ingredientName, amount, amountUnits)
                .then(function (recipeIngredient) {
                    vm.recipeIngredients.push(recipeIngredient);
                });
        };
    };

    return makeNewRecipe;
};