angular.module('readyChefApp')
    .factory('recipeService',recipeService);
recipeService.$inject=['recipe','$http'];
function recipeService(recipe, $http) {
    var vm = this;
    

    function saveRecipe(recipe) {
        $http.post('/api/Recipe/Add', recipe);
    }

    function getRecipesByName(name) {
        return $http.get('/api/Recipe/GetByName/' + name)
                .then(function (response) {
                    return response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
    }

    function getRecipesByIngredient(ingredient) {
        return $http.get('/api/Recipe/GetByIngredient/' + ingredient)
                .then(function (response) {
                    return response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
    }

    function getAllRecipes() {
        return $http.get('/api/Recipe/getall')
                .then(function (response) {
                    return response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
    };

    return {
        saveRecipe: saveRecipe,
        getRecipesByName: getRecipesByName,
        getRecipesByIngredient: getRecipesByIngredient,
        getAllRecipes: getAllRecipes
    };
};