angular.module('app.core').controller('IniciarSesionCtrl', IniciarSesionCtrl);

function IniciarSesionCtrl(tokenManager) {
    tokenManager.redirectForToken();
}