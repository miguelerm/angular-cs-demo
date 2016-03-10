angular.module('app.core').controller('MenuPrincipalCtrl', MenuPrincipalCtrl);

function MenuPrincipalCtrl($rootScope, config) {


    var vm = this;
    var manager = new OidcTokenManager(config.tokenManager);

    vm.opciones = [];

    init();

    function init() {

        if (manager.expired) {
            $rootScope.$on('USER_LOGGED_IN', cargarMenu)
        } else {
            cargarMenu();
        }
    }

    function cargarMenu() {
        vm.opciones = [
            {
                titulo: 'Bodegas',
                url: '#/bodegas'
            },
            {
                titulo: 'Reportes',
                opciones: [
                    {
                        titulo: 'Top 10',
                        url: '#/bodegas/reportes/top-10'
                    },
                    {
                        titulo: 'Catalogo',
                        url: '#/bodegas/reportes/catalogo'
                    }
                ]
            }
        ]
    }

}