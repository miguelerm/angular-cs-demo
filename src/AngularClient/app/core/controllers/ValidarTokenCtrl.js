﻿angular.module('app.core').controller('ValidarTokenCtrl', ValidarTokenCtrl);

function ValidarTokenCtrl($location, $state, $window, $rootScope, config) {

    var url = $location.url();
    var path = $location.path();
    var queryString = url.replace(path, '').substr(1);

    var manager = new OidcTokenManager(config.tokenManager);
    manager.processTokenCallbackAsync(queryString).then(function () {

        checkSessionState();
        $rootScope.$emit('USER_LOGGED_IN', manager.profile)
        $state.go('core.home');

    }).catch(function () {
        console.log('catch ', arguments);
    });

    function checkSessionState() {

        var handled = false;

        manager.oidcClient.loadMetadataAsync().then(function (meta) {
            if (meta.check_session_iframe && manager.session_state) {
                document.getElementById('rp').src = 'verificar-sesion.html#' +
                    'session_state=' + manager.session_state +
                    '&check_session_iframe=' + meta.check_session_iframe +
                    '&client_id=' + config.tokenManager.client_id;
            }
            else {
                document.getElementById('rp').src = 'about:blank';
            }
        });

        window.onmessage = function (e) {
            if (e.origin === 'http://localhost:65339' && !handled) {
                if (e.data === 'changed' || (e.data && angular.isString(e.data) && e.data.indexOf('#error=login_required') == 0)) {
                    handled = true;
                    manager.removeToken();
                    manager.renewTokenSilentAsync().then(function () {
                        // Session state changed but we managed to silently get a new identity token, everything's fine
                        console.log('renewTokenSilentAsync success');
                    }, function () {
                        // Here we couldn't get a new identity token, we have to ask the user to log in again
                        $window.location.reload();
                    });
                }
            }
        }
    }
}