angular.module('readyChefApp')
    .controller('CreateRecipeController',CreateRecipeController);
CreateRecipeController.$inject=['recipe','recipeService'];
function CreateRecipeController(recipe,recipeService){
    var vm = this;
    vm.ingredientToAdd;
    vm.currentRecipe = new recipe();
    vm.saveRecipe = saveRecipe;

    function saveRecipe(){
        if (vm.currentRecipe.recipeIngredients.length === 0){
            alert('Must add at least one ingredient.');
            return;
        }
        else recipeService.saveRecipe(vm.currentRecipe); 
    };
};