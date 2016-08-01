angular.module('readyChefApp')
    .controller('HomeController',HomeController);
HomeController.$inject=['$scope'];
function HomeController($scope) {
    var vm = this;
    vm.showSignIn = true;
    vm.showRegister = false;

    vm.clickSignInTab = function () {
        if (vm.showSignIn) return;
        vm.showSignIn = true;
        vm.showRegister = false;
        $('#registerTab').removeClass('active');
        $('#signInTab').addClass('active');
    }
    vm.clickRegisterTab = function () {
        if (vm.showRegister) return;
        vm.showRegister = true;
        vm.showSignIn = false;
        $('#signInTab').removeClass('active');
        $('#registerTab').addClass('active');
    }
};    