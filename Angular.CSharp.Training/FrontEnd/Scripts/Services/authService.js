app.service('AuthService', ['$http', function ($http) {
    var isAuthenticated = !!localStorage.getItem('authToken');
    var baseUrl = 'https://localhost:44381/api/auth';
    var authToken = localStorage.getItem('authToken') || null;
    var userRole = localStorage.getItem('userRole') || null;
    var loggedUser = localStorage.getItem('loggedUser') || null;

    this.getLoggedUser = function () {
        return loggedUser || localStorage.getItem('loggedUser');
    };

    this.getAuthToken = function () {
        return authToken;
    };

    this.login = function (loginModel) {
        return $http.post(baseUrl + '/login', loginModel).then(function (response) {
            if (response.data.Token && response.data.Role) {
                isAuthenticated = true;
                authToken = response.data.Token;
                userRole = response.data.Role;
                loggedUser = response.data.LoggedUser;

                localStorage.setItem('authToken', authToken);
                localStorage.setItem('userRole', userRole);
                localStorage.setItem('loggedUser', loggedUser);
                return true;
            } else {
                return false;
            }
        });
    };

    this.isAuthenticated = function () {
        return isAuthenticated || !!localStorage.getItem('authToken');
    };

    this.getUserRole = function () {
        return userRole || localStorage.getItem('userRole');
    };

    this.logout = function () {
        isAuthenticated = false;
        authToken = null;
        userRole = null;
        localStorage.removeItem('authToken');
        localStorage.removeItem('userRole');
        localStorage.removeItem('loggedUser');
    };

    this.register = function (registerModel) {
        return $http.post(baseUrl + '/register', registerModel).then(function (response) {
            return response.data.success;
        });
    };
}]);
