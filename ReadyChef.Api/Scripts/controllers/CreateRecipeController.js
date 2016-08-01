angular.module('readyChefApp')
    .controller('CreateRecipeController',CreateRecipeController);
CreateRecipeController.$inject=['recipe','recipeService','$scope'];
function CreateRecipeController(recipe, recipeService, $scope) {
    var vm = this;
    vm.ingredientToAdd = "";
    vm.ingredientToAddAmount = 0;
    vm.ingredientToAddUnits = "";
    vm.currentRecipe = new recipe();
    vm.saveRecipe = saveRecipe;
    vm.addIngredient = addIngredient;
    vm.removeIngredient = removeIngredient;
    vm.showRemoveIcons = [];
    vm.removeIcon = removeIcon;
    vm.addIcon = addIcon;
  

    function removeIngredient(index) {
        vm.currentRecipe.recipeIngredients.splice(index, 1);
        vm.showRemoveIcons.splice(index,1);
    }

    function addIngredient() {
        if (isNullOrWhitespace(vm.ingredientToAdd)) return;
        if (!$scope.addIngredientForm.$valid) return;
        vm.currentRecipe.addRecipeIngredient(vm.ingredientToAdd,vm.ingredientToAddAmount,vm.ingredientToAddUnits);
        window.toastr.success('Ingredient added.');
        vm.showRemoveIcons.push(false)
    }

    function removeIcon(index) {
        vm.showRemoveIcons[index] = false;
    };
    function addIcon(index) {
        vm.showRemoveIcons[index] = true;
    };

    function saveRecipe(){
        if (vm.currentRecipe.recipeIngredients.length === 0){
            alert('Must add at least one ingredient.');
            return;
        }
        else recipeService.saveRecipe(vm.currentRecipe);
        window.toastr.success('Recipe saved successfully.')
    };
    function isNullOrWhitespace(input) {

        if (typeof input === 'undefined' || input == null) return true;

        return input.replace(/\s/g, '').length < 1;
    };
};