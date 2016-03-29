angular.module('app.core').controller('MenuPrincipalCtrl', MenuPrincipalCtrl);

function MenuPrincipalCtrl($rootScope, $http, $log, tokenManager) {

    var vm = this;
    vm.opciones = [];

    init();

    function init() {

        if (tokenManager.expired || !tokenManager.profile) {
            $rootScope.$on('USER_LOGGED_IN', obtenerOpcionesDelMenu)
        } else {
            obtenerOpcionesDelMenu();
        }
    }

    function obtenerOpcionesDelMenu() {
        $http.get('/api/opcionesdemenu').then(cargarOpcionesDelMenu, mostrarError);
    }

    function mostrarError() {
        $log.error("Ocurrió un error: ", arguments);
    }

    function cargarOpcionesDelMenu(response) {
        vm.opciones = response.data;
    }

}