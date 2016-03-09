angular.module('app.shared').directive('appMenuPrincipal', appMenuPrincipalDirective);

function appMenuPrincipalDirective() {
    return {
        restrict: 'E',
        controller: 'MenuPrincipalCtrl as vm',
        templateUrl: 'app/shared/views/menu-principal.html'
    };
}