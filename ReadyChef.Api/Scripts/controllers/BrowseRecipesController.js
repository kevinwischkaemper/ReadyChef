angular.module('readyChefApp')
    .controller('BrowseRecipesController',BrowseRecipesController);
BrowseRecipesController.$inject=['recipeService'];
function BrowseRecipesController(recipeService){
    var vm = this;
    vm.recipe = new recipe();
    vm.recipeShow = false;
    vm.recipeNotFoundShow = false;
    vm.getRecipes = getRecipes;
    vm.searchText = "";
    vm.listOfRecipes = [];
    vm.searchType = "Name";
    vm.searchByName = searchByName;
    vm.searchByIngredient = searchByIngredient;
    vm.selectedRecipe = null;
    vm.selectRecipe = selectRecipe;
    vm.getAllRecipes = getAllRecipes;
    vm.weeklyPlanSortableConfig = {
        group: {
            name: 'recipes',
            pull: 'clone',
            put: false
        }
    }

    function searchByName() {
        vm.searchType = "Name";
    };
    function searchByIngredient() {
        vm.searchType = "Ingredient";
    };
    function getAllRecipes() {
        vm.listOfRecipes = [];
        vm.recipeNotFoundShow = false;
        vm.recipeShow = false;
        recipeService.getAllRecipes()
            .then(function (recipes) {
                if (recipes) {
                    vm.listOfRecipes = recipes;
                    vm.recipeShow = true;
                }
                else {
                    vm.recipeNotFoundShow = true;
                }
            });
    }
    function selectRecipe(index) {
        if (vm.selectedRecipe)
            $('.recipe' + vm.selectedRecipe.id).removeClass('selectedRecipe');
        vm.selectedRecipe = vm.listOfRecipes[index];
        $('.recipe' + vm.selectedRecipe.id).addClass('selectedRecipe');
    }
    function getRecipes() {
        if (vm.searchType == "Name")
            return getRecipesByName();
        else
            return getrecipesByIngredient();
    };

    function getrecipesByIngredient() {
        if (isNullOrWhitespace(vm.searchText)) return;
        vm.listOfRecipes = [];
        vm.recipeNotFoundShow = false;
        vm.recipeShow = false;
        recipeService.getRecipesByIngredient(vm.searchText)
            .then(function (recipes) {
                if (recipes) {
                    vm.listOfRecipes = recipes;
                    vm.recipeShow = true;
                }
                else {
                    vm.recipeNotFoundShow = true;
                }
            });
    };
    function getRecipesByName() {
        if (isNullOrWhitespace(vm.searchText)) return;
        vm.listOfRecipes = [];
        vm.recipeNotFoundShow = false;
        vm.recipeShow = false;
        recipeService.getRecipesByName(vm.searchText)
            .then(function (recipes) {
                if (recipes) {
                    vm.listOfRecipes = recipes;
                    vm.recipeShow = true;
                }
                else {
                    vm.recipeNotFoundShow = true;
                }
            });
    };

    function isNullOrWhitespace(input) {

        if (typeof input === 'undefined' || input == null) return true;

        return input.replace(/\s/g, '').length < 1;
    };

    function mouseDrags(sprite, spriteContainer) {
        // All of the mouse event sequences look like this:
        // seq([ {pageX: 22, pageY: 3423, offsetX: 14, offsetY: 22} ,,, ])
        var spriteMouseDowns = Observable.fromEvent(sprite, "mousedown"),
            spriteContainerMouseMoves = Observable.fromEvent(spriteContainer, "mousemove"),
            spriteContainerMouseUps = Observable.fromEvent(spriteContainer, "mouseup"),
            // Create a sequence that looks like this:
            // seq([ {pageX: 22, pageY:4080 },,,{pageX: 24, pageY: 4082},,, ])
            spriteMouseDrags =
                // For every mouse down event on the sprite...
                spriteMouseDowns.
                    concatMap(function(contactPoint) {
                        // ...retrieve all the mouse move events on the sprite container...
                        return spriteContainerMouseMoves.
                            // ...until a mouse up event occurs.
                            takeUntil(spriteContainerMouseUps).
                            map(function(movePoint) {
                                return {
                                    pageX: movePoint.pageX - contactPoint.offsetX,
                                    pageY: movePoint.pageY - contactPoint.offsetY
                                };
                            });
                    });

        // For each mouse drag event, move the sprite to the absolute page position.
        spriteMouseDrags.forEach(function(dragPoint) {
            sprite.style.left = dragPoint.pageX + "px";
            sprite.style.top = dragPoint.pageY + "px";
        });
    }
};    