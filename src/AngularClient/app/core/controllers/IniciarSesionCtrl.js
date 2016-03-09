angular.module('app.core').controller('IniciarSesionCtrl', IniciarSesionCtrl);

function IniciarSesionCtrl(config) {
    var manager = new OidcTokenManager(config.tokenManager);
    manager.redirectForToken();
}