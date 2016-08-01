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
    $stateProvider.state('browseRecipes.viewAll', {
        views: {
            "": {
                url: '/browseRecipes/viewAll',
                templateUrl: 'viewAll.cshtml',
                controller: 'BrowseRecipesController',
                controllerAs: 'br'
            }
        }
    });
    $stateProvider.state('browseRecipes.viewSingle', {
        views: {
            "": {
                url: '/browseRecipes/viewSingle',
                templateUrl: 'viewSingle.cshtml',
                controller: 'BrowseRecipesController',
                controllerAs: 'br'
            }
        }
    });
    $stateProvider.state('myPlan', {
        url: '/myPlan',
        abstract: true,
        templateUrl: 'myPlan.cshtml',
        controller: 'MyPlanController',
        controllerAs: 'mp'           
    })
    .state('myPlan.views', {
        views: {
            'weeklyPlan': {
                templateUrl: 'weeklyPlan.cshtml',
                controller: 'MyPlanController',
                controllerAs: 'mp'
            },
            'searchRecipes': {
                templateUrl: 'searchRecipes.cshtml',
                controller: 'BrowseRecipesController',
                controllerAs: 'br'
            }
        }
    });
};
