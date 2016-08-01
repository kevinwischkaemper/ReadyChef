angular.module('readyChefApp')
    .factory('ingredientFactory',ingredientFactory);
ingredientFactory.$inject=['$http'];
function ingredientFactory($http) {
            
    function makeNewIngredient(name) {
        return $http.post('/api/ingredient/add', '"' + name + '"')
        .then(function (response) {
            return {name:name,id:response.data};
        })
        .catch(function (response) {
            console.log(response);
        });
    };
    return {
        makeNewIngredient : makeNewIngredient
    };
};