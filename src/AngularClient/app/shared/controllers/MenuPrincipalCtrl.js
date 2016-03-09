angular.module('app.shared').controller('MenuPrincipalCtrl', MenuPrincipalCtrl);

function MenuPrincipalCtrl() {

    var vm = this;

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
        },
    ]

}