angular.module('readyChefApp')
    .controller('MyPlanController',MyPlanController);
MyPlanController.$inject=[];
function MyPlanController() {
    var vm = this;
    vm.weekDays = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
    vm.weeklyPlan = vm.weekDays.map(function (day) {
        return createDefaultRecipe(day);
    });
    vm.consolidatedGroceryList = [];
    vm.weeklyPlanSortableConfig = {
        group: 'recipes',
        sort: false,
        ghostClass: 'ghost',
        onEnd: function (evt) {
            vm.refreshDefaultRecipe();
        },
        onStart: function (evt) {
            vm.yes = "hey";
        }
    };
    vm.refreshDefaultRecipe = refreshDefaultRecipe;
    vm.toggleRecipeDetails = function (dayName) {
        var index = vm.weekDays.indexOf(dayName);
        vm.weeklyPlan[index].showDetails = !vm.weeklyPlan[index].showDetails;
        if (vm.weeklyPlan[index].showDetails) {
            vm.weeklyPlan[index].showHide = "Hide";
            changeGlyphiconArrow($('#detailsIcon' + dayName), "up", "down");
        }
        else {
            vm.weeklyPlan[index].showHide = "Show";
            changeGlyphiconArrow($('#detailsIcon' + dayName), "down", "up");
        } 
    };
    vm.showPlan = true;
    vm.showGroceryList = false;
    vm.clickPlanTab = function () {
        if (vm.showPlan) return;
        vm.showPlan = true;
        vm.showGroceryList = false;
        $('#groceryTab').removeClass('active');
        $('#planTab').addClass('active');

    }
    vm.clickGroceryTab = function () {
        if (vm.showGroceryList) return;
        vm.showPlan = false;
        vm.showGroceryList = true;
        $('#planTab').removeClass('active');
        $('#groceryTab').addClass('active');
        vm.consolidatedGroceryList = consolidateGroceryItems();
    }
    vm.removeRecipe = function (day) {
        var index = vm.weekDays.indexOf(day);
        vm.weeklyPlan[index] = createDefaultRecipe(day);
    }

    function consolidateGroceryItems() {
        var rawGroceryList = vm.weeklyPlan.filter(function (day) {
            return day.recipes[0].hasOwnProperty("recipeIngredients")
        }).map(function (day) {
            return day.recipes[0].recipeIngredients
        });
        rawGroceryList = [].concat.apply([], rawGroceryList);
        let nameList = [];
        let groceryList = [];
        rawGroceryList.forEach(function (recipeIngredient) {
            var index = nameList.indexOf(recipeIngredient.ingredient.name);
            if (index == -1) {
                nameList.push(recipeIngredient.ingredient.name);
                groceryList.push({ name: recipeIngredient.ingredient.name, amount: recipeIngredient.amount, amountUnits: recipeIngredient.amountUnits })
            }
            else {
                groceryList[index].amount += recipeIngredient.amount;
            };
        });
        return groceryList;
    }

    function changeGlyphiconArrow(element, addDirection, removeDirection) {
        if (removeDirection)
            element.removeClass("glyphicon glyphicon-arrow-" + removeDirection);
        element.addClass("glyphicon glyphicon-arrow-" + addDirection);
    }
    function refreshDefaultRecipe() {
        for (var i = 0; i < vm.weekDays.length; i++) {
            if (vm.weeklyPlan[i].recipes.length == 0) {
                vm.weeklyPlan[i] = createDefaultRecipe(vm.weekDays[i]);
            }
        }
    }
    function createDefaultRecipe(day) {
        return { dayName: day, recipes: [{ name: 'Drag a recipe from the right' }], showHide: "Show", showDetails: false };
    }
}
