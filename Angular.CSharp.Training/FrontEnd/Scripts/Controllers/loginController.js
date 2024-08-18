app.controller('LoginController', ['$scope', '$location', 'AuthService', function ($scope, $location, AuthService) {
    $scope.user = {};

    $scope.login = function () {
        AuthService.login($scope.user).then(function (response) {
            $location.path("/employees");
            console.log('Login successful:', response.data);
        }, function (error) {
            console.log('Error log: ', error);
            alert('Login failed: ' + error.statusText);
        });
    };

    $scope.register = function () {
        AuthService.register($scope.user).then(function () {
            alert('Registration successful. You can now log in.');
        }, function (error) {
            console.log('Error log: ', error);
            alert('Registration failed: ' + error.statusText);
        });
    };
}]);
//app.controller('LoginController', ['$scope', '$location', 'AuthService', function ($scope, $location, AuthService) {
//    $scope.credentials = {
//        username: 'admin',
//        password: 'password'
//    };

//    $scope.errorMessage = '';

//    $scope.login = function () {
//        if (AuthService.login($scope.credentials.username, $scope.credentials.password)) {
//            $location.path("/employees");
//        } else {
//            $scope.errorMessage = "Invalid username or password";
//        }
//    };
//}]);
