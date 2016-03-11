angular.module('app.core').controller('ValidarTokenCtrl', ValidarTokenCtrl);

function ValidarTokenCtrl($location, $state, $window, $rootScope, tokenManager, config) {

    var url = $location.url();
    var path = $location.path();
    var queryString = url.replace(path, '').substr(1);

    tokenManager.processTokenCallbackAsync(queryString).then(function () {

        //checkSessionState();
        $rootScope.$emit('USER_LOGGED_IN', tokenManager.profile)
        $state.go('core.home');

    }).catch(function () {
        console.log('catch ', arguments);
    });

    
}