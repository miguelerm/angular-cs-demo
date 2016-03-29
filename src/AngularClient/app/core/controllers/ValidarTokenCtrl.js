angular.module('app.core').controller('ValidarTokenCtrl', ValidarTokenCtrl);

function ValidarTokenCtrl($location, $state, $window, $rootScope, tokenManager, config) {

    var url = $location.url();
    var path = $location.path();
    var queryString = url.replace(path, '').substr(1);

    tokenManager.processTokenCallbackAsync(queryString).then(function () {

        //checkSessionState();
        $rootScope.$emit('USER_LOGGED_IN', tokenManager.profile)

        var returnState = JSON.parse(sessionStorage.getItem('return-state') || null);

        if (returnState) {
            $state.go(returnState.name, returnState.params);
        } else {
            $state.go('core.home');
        }

    }).catch(function () {
        console.log('catch ', arguments);
    });

    
}