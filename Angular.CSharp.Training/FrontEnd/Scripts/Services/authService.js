app.service('AuthService', ['$http', function ($http) {
    var isAuthenticated = false;
    var baseUrl = 'https://localhost:44381/api/auth';
    var authToken = null; // Add this to store the token if needed

    this.login = function (loginModel) {
        return $http.post(baseUrl + '/login', loginModel).then(function (response) {
            if (response.data.Token) {
                isAuthenticated = true;
                authToken = response.data.Token; // Store the token if needed
                // Optionally store token or user data in localStorage/sessionStorage
                localStorage.setItem('authToken', authToken);
                return true;
            } else {
                return false;
            }
        });
    };

    this.isAuthenticated = function () {
        return isAuthenticated || !!localStorage.getItem('authToken');
    };

    this.logout = function () {
        isAuthenticated = false;
        authToken = null;
        localStorage.removeItem('authToken'); // Remove token from storage
    };

    this.register = function (registerModel) {
        return $http.post(baseUrl + '/register', registerModel).then(function (response) {
            return response.data.success;
        });
    };
}]);