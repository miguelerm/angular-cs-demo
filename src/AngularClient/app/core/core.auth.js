angular.module('app.core').config(configureAuth).run(redirectToLogin);

function configureAuth($stateProvider, config) {

    var manager = new OidcTokenManager(config.tokenManager);

    $stateProvider.decorator('parent', function (state, parent) {
        if (!state.abstract && (!state.data || !state.data.allowAnonymous)) {
            state.resolve = state.resolve || {};
            state.resolve.user = ObtenerUsuarioAutenticado;
        }

        return parent(state);
    });

    ObtenerUsuarioAutenticado.$inject = ['$q'];

    function ObtenerUsuarioAutenticado($q) {
        var deferred = $q.defer();

        var usr = manager.profile;

        if (usr && angular.isObject(usr)) {
            deferred.resolve(usr);
        } else {
            deferred.reject("AUTH_REQUIRED");
        }

        return deferred.promise;
    }

}

function redirectToLogin($rootScope, $state) {

    $rootScope.$on("$stateChangeError", onStateChangeError);

    function onStateChangeError(event, toState, toParams, fromState, fromParams, error) {
        if (error === "AUTH_REQUIRED") {
            $state.go('core.iniciar-sesion');
        }
    }
}