angular.module('readyChefApp')
    .factory('ingredientFactory',ingredientFactory);
ingredientFactory.$inject=['$http','ingredient'];
function ingredientFactory($http, ingredient) {
            
    function makeNewIngredient(name) {
        var vm = this;
        vm.name = name;
        return $http.post('/api/ingredient/add', '"' + name + '"')
        .then(function (response) {
            vm.id = response.data;
            return new ingredient(vm.name, vm.id);
        })
        .catch(function (response) {
            console.log(response);
        });
    };
    return {
        makeNewIngredient : makeNewIngredient
    };
};