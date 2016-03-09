angular.module('app.core').config(configRoutes);

function configRoutes($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.otherwise("/");

    $stateProvider
        .state('core', {
            url: "",
            template: '<div ui-view></div>',
            abstract: true
        })
        .state('core.home', {
            url: "/",
            template: '<div></div>'
        })
        .state('core.iniciar-sesion', {
            url: "/iniciar-sesion",
            templateUrl: 'app/core/views/iniciar-sesion.html',
            controller: 'IniciarSesionCtrl as vm',
            data: {
            	allowAnonymous: true
            }
        })
        .state('core.validar-token', {
            url: "/validar-token",
        	templateUrl: 'app/core/views/validar-token.html',
        	controller: 'ValidarTokenCtrl as vm',
        	data: {
        		allowAnonymous: true
        	}
        });

}