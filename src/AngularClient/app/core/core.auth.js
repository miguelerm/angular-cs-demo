angular.module('app.core').factory('httpRequestInterceptor', httpRequestInterceptorFactory).config(configureAuth).run(redirectToLogin).run(configureSessionChecker);

function httpRequestInterceptorFactory(tokenManager) {
    return {
        request: function ($config) {
            if (tokenManager.access_token) {
                $config.headers['Authorization'] = 'Bearer ' + tokenManager.access_token;
            }
            return $config;
        }
    };
}

function configureAuth($stateProvider, $httpProvider, tokenManager) {

    $httpProvider.interceptors.push('httpRequestInterceptor');

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

        var usr = tokenManager.profile;

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
            var returnState = JSON.stringify({ name: toState.name, params: toParams });
            sessionStorage.setItem('return-state', returnState);
            $state.go('core.iniciar-sesion');
        }
    }

}

function configureSessionChecker($rootScope, tokenManager, config) {
    if (tokenManager.expired || !tokenManager.profile) {
        $rootScope.$on('USER_LOGGED_IN', checkSessionState.bind(null, tokenManager, config, 'user logged in'));
    } else {
        checkSessionState(tokenManager, config)
    }
}

function checkSessionState(tokenManager, config) {

    console.log('checkSessionState ', arguments);

    var handled = false;

    tokenManager.oidcClient.loadMetadataAsync().then(function (meta) {
        var element = document.getElementById('rp');
        if (meta.check_session_iframe && tokenManager.session_state) {
           
            element.src = '/verificar-sesion.html#' +
                'session_state=' + tokenManager.session_state +
                '&check_session_iframe=' + meta.check_session_iframe +
                '&client_id=' + config.tokenManager.client_id;
        }
        else {
            element.src = 'about:blank';
        }
    }).catch(function () {
        console.error('catch ', arguments);
    });

    window.onmessage = function (e) {
        if (e.origin === config.clientUrl && !handled) {
            if (e.data === 'changed' || (e.data && angular.isString(e.data) && e.data.indexOf('#error=login_required') == 0)) {
                handled = true;
                tokenManager.removeToken();
                tokenManager.renewTokenSilentAsync().then(function () {
                    // Session state changed but we managed to silently get a new identity token, everything's fine
                    console.log('renewTokenSilentAsync success');
                }, function () {
                    console.log('renewTokenSilentAsync fail');
                    // Here we couldn't get a new identity token, we have to ask the user to log in again
                    window.location.reload();
                });
            }
        }
    }
}

