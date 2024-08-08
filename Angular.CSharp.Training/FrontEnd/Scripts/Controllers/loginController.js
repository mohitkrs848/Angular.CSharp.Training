var app = angular.module('loginApp', []);

app.controller('LoginController', function ($scope, $http, $window) {
    $scope.credentials = {};

    $scope.login = function () {
        $http.post('/api/login', $scope.credentials).then(function (response) {
            $window.location.href = 'home.html'; // Redirect to the home page after login
        }, function (error) {
            alert('Invalid credentials');
        });
    };
});
