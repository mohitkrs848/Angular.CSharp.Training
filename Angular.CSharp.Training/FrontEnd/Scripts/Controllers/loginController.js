app.controller('LoginController', ['$scope', '$location', 'AuthService', function ($scope, $location, AuthService) {
    $scope.credentials = {
        username: 'admin',
        password: 'password'
    };

    $scope.errorMessage = '';

    $scope.login = function () {
        if (AuthService.login($scope.credentials.username, $scope.credentials.password)) {
            $location.path("/dashboard");
        } else {
            $scope.errorMessage = "Invalid username or password";
        }
    };
}]);
