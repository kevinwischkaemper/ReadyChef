'use strict';

angular.module('readyChefApp', ['ui.router','ng-sortable']);

angular.module('readyChefApp').directive('ngEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngEnter);
                });
                event.preventDefault();
            }
        });
    };
});

angular.module('readyChefApp').directive('bindToHeight', function ($window) {

    return {
        restrict: 'A',

        link: function (scope, elem, attrs) {
            var attributes = scope.$eval(attrs['bindToHeight']);
            var targetElem = angular.element(document.querySelector(attributes[1]));

            // Watch for changes
            scope.$watch(function () {
                return targetElem.height();
            },
            function (newValue, oldValue) {
                if (newValue != oldValue) {
                    if (newValue > attributes[2]) {
                        elem.css(attributes[0], newValue);
                    }
                    else {
                        elem.css(attributes[0], 477);
                    }
                }
            });
        }
    };
});