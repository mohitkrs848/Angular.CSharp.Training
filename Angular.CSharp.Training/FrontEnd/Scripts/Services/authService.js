app.service('AuthService', function () {
    var isAuthenticated = false;

    var hardcodedCredentials = {
        username: 'admin',
        password: 'password'
    };

    this.login = function (username, password) {
        if (username === hardcodedCredentials.username && password === hardcodedCredentials.password) {
            isAuthenticated = true;
            return true;
        } else {
            return false;
        }
    };

    this.isAuthenticated = function () {
        return isAuthenticated;
    };

    this.logout = function () {
        isAuthenticated = false;
    };

});
