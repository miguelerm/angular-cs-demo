angular.module('app.core').directive('appMenuPrincipal', appMenuPrincipalDirective);

function appMenuPrincipalDirective() {
    return {
        restrict: 'E',
        controller: 'MenuPrincipalCtrl as vm',
        templateUrl: 'app/core/views/menu-principal.html'
    };
}