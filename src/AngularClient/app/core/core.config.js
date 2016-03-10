angular.module('app.core').constant('config', {
  tokenManager: {
    authority: 'http://localhost:15001',
    client_id: 'angular-client',
    redirect_uri: 'http://localhost:65339/#/validar-token?',
    response_type: 'id_token token',
    scope: 'openid profile email api',
    filter_protocol_claims: true,
    silent_renew: true,
    silent_redirect_uri: 'http://localhost:65339/actualizar-token.html',
  }
});