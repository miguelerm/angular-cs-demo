angular.module('app.core').controller('ValidarTokenCtrl', ValidarTokenCtrl);

function ValidarTokenCtrl($location, config) {

    var url = $location.url();
    var path = $location.path();
    var queryString = url.replace(path, '').substr(1);

    console.log(queryString);
    var manager = new OidcTokenManager(config.tokenManager);
    manager.processTokenCallbackAsync(queryString).then(function () {
        console.log('then ', manager.profile);
    }).catch(function () {
        console.log('catch ', arguments);
    });
}