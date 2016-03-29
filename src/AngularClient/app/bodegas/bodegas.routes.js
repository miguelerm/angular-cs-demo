angular.module('app.bodegas').config(configRoutes);

function configRoutes($stateProvider) {

    $stateProvider
        .state('bodegas', {
        	url: "/bodegas",
        	template: '<div ui-view></div>',
        	abstract: true
        })
        .state('bodegas.listar', {
        	url: "",
        	templateUrl: 'app/bodegas/views/bodegas-listar.html',
        	controller: 'BodegasListarCtrl as vm'
        })
        .state('bodegas.crear', {
        	url: "/crear",
        	templateUrl: 'app/bodegas/views/bodegas-crear.html',
        	controller: 'BodegasCrearCtrl as vm'
        })
        .state('bodegas.editar', {
        	url: "/:id/editar",
        	templateUrl: 'app/bodegas/views/bodegas-editar.html',
        	controller: 'BodegasCreateCtrl as vm'
        })
        .state('bodegas.reportes', {
            url: "/reportes",
            template: '<div ui-view></div>',
            abstract: true
        })
        .state('bodegas.reportes.top-10', {
            url: "/top10",
        })
        .state('bodegas.reportes.catalogo', {
            url: "/catalogo",
        });

}