angular.module('readyChefApp')
    .service('ingredient', ingredient);
ingredient.$inject = [];

function ingredient() {
    function newIngredient(name, id) {
        this.name = name;
        this.id = id;
    };
    return newIngredient;
}