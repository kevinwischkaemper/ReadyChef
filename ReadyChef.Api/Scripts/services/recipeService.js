angular.module('readyChefApp')
    .factory('recipeService',recipeService);
recipeService.$inject=['recipe','$http','recipeIngredient'];
function recipeService(recipe, $http, recipeIngredient) {
    var vm = this;
    
    function saveRecipe(recipe) {
        $http.post('/api/Recipe/Add', recipe);
    }

    function getRecipe(name) {
        return $http.get('/api/Recipe/Get/' + name)
                .then(function (response) {
                    return response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
    }

    return{
        saveRecipe: saveRecipe,
        getRecipe: getRecipe
    };
};