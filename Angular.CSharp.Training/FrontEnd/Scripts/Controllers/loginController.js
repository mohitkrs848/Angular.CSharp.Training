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
    $scope.loginWithGoogle = function (googleToken) {
        AuthService.googleLogin(googleToken).then(function (response) {
            $location.path("/employees");
            console.log('Google login successful:', response.data);
        }, function (error) {
            console.log('Google login error: ', error);
            alert('Google login failed: ' + error.statusText);
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

// Google callback function
function onGoogleSignIn(response) {
    var googleToken = response.credential;
    var $scope = angular.element(document.getElementById('g_id_signin')).scope();
    $scope.$apply(function () {
        $scope.loginWithGoogle(googleToken);
    });
}
