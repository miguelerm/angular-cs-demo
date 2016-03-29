angular.module('app.core').directive('appPaginaEncabezado', appPaginaEncabezado);

function appPaginaEncabezado() {
    return {
        restrict: 'E',
        scope: {
            titulo: '@'
        },
        templateUrl: 'app/core/views/pagina-encabezado.html'
    };
}