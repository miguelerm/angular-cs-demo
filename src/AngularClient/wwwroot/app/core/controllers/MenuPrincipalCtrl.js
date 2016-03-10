angular.module('app.core').controller('MenuPrincipalCtrl', MenuPrincipalCtrl);

function MenuPrincipalCtrl($rootScope, tokenManager) {


    var vm = this;
    vm.opciones = [];

    init();

    function init() {

        if (tokenManager.expired || !tokenManager.profile) {
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