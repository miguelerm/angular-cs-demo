angular.module('app.shared').directive('appPaginaEncabezado', appPaginaEncabezado);

function appPaginaEncabezado() {
    return {
        restrict: 'E',
        scope: {
            titulo: '@'
        },
        templateUrl: 'app/shared/views/pagina-encabezado.html'
    };
}