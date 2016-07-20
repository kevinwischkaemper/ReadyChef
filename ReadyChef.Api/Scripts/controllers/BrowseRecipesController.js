angular.module('readyChefApp')
    .controller('BrowseRecipesController',BrowseRecipesController);
BrowseRecipesController.$inject=['recipeService'];
function BrowseRecipesController(recipeService){
    var vm = this;
    vm.recipe = new recipe();
    vm.recipeShow = false;
    vm.getRecipe = getRecipe;
    vm.recipeName = "";

    function getRecipe() {
        if (isNullOrWhitespace(vm.recipeName)) return;
        recipeService.getRecipe(vm.recipeName)
            .then(function (recipe) {
                vm.recipe = recipe;

                vm.recipeShow = true;

            });
    };

    function isNullOrWhitespace( input ) {

        if (typeof input === 'undefined' || input == null) return true;

        return input.replace(/\s/g, '').length < 1;
    }
};    