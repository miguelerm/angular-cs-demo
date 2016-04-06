window.configuracion = {
    clientUrl: 'http://localhost:56673',
    tokenManager: {
        authority: 'http://localhost:56673/identity',
        redirect_uri: 'http://localhost:56673/#/validar-token?',
        silent_redirect_uri: 'http://localhost:56673/actualizar-token.html',
        client_id: 'angular-client',
        response_type: 'id_token token',
        scope: 'openid profile email api',
        filter_protocol_claims: true,
        silent_renew: true,
        store: window.sessionStorage
    }
};