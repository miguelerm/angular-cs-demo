
var tokenManager = new OidcTokenManager(configuracion.tokenManager);

angular.module('app.core')
    .constant('config', window.configuracion)
    .constant('tokenManager', tokenManager);