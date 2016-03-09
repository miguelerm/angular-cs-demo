angular.module('app.core').constant('config', {
  tokenManager: {
    authority: 'http://localhost:15001',
    client_id: 'angular-client',
    redirect_uri: 'http://localhost:65339/#/validar-token?',
    response_type: 'id_token',
    scope: 'openid profile',
    filter_protocol_claims: true
  }
});