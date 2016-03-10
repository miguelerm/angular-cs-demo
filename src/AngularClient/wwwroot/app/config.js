window.configuracion = {
    clientUrl: 'http://localhost:15003',
    tokenManager: {
        authority: 'http://localhost:15001',
        client_id: 'angular-client',
        redirect_uri: 'http://localhost:15003/#/validar-token?',
        response_type: 'id_token token',
        scope: 'openid profile email api',
        filter_protocol_claims: true,
        silent_renew: true,
        silent_redirect_uri: 'http://localhost:15003/actualizar-token.html',
    }
};