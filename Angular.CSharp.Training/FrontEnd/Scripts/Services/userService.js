app.service('UserService', ['$http', function ($http) {
    var baseUrl = 'https://localhost:44381/api/user';

    this.getUsers = function () {
        var token = AuthService.getAuthToken(); // Ensure this function is available and returns the token
        return $http.get(baseUrl, {
            headers: {
                'Authorization': 'Bearer ' + token
            }
        });
    };

    this.getAllUsers = function () {
        return $http.get(baseUrl);
    };

    this.makeAdmin = function (id) {
        return $http.post(baseUrl + '/MakeAdmin', { id: id });
    };

    this.deleteUser = function (id) {
        return $http.delete(baseUrl + '/' + id);
    };
}]);
