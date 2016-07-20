angular.module('readyChefApp')
    .config(Config);

Config.$inject = ['$stateProvider'];
function Config($stateProvider) {
    $stateProvider.state('home', {
        url: '/home',
        controller: 'HomeController',
        controllerAs: 'hc',
        templateUrl: 'Home.cshtml'
    });
    $stateProvider.state('browseRecipes', {
        url: '/browseRecipes',
        templateUrl: 'browseRecipes.cshtml',
        controller: 'BrowseRecipesController',
        controllerAs: 'br'
    });
    $stateProvider.state('createRecipe', {
        url: '/createRecipe',
        templateUrl: 'createRecipe.cshtml',
        controller: 'CreateRecipeController',
        controllerAs: 'cr',
    });
};