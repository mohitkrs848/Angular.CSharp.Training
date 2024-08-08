app.controller('MainController', ['$scope', '$location', 'AuthService', function ($scope, $location, AuthService) {
    $scope.isAuthenticated = function () {
        return AuthService.isAuthenticated();
    };

    $scope.logout = function () {
        AuthService.logout();
        $location.path("/");
    };
}]);
