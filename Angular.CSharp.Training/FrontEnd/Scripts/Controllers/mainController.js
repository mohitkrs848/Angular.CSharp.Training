app.controller('MainController', ['$scope', '$location', 'AuthService', function ($scope, $location, AuthService) {
    $scope.isAuthenticated = function () {
        return AuthService.isAuthenticated();
    };

    $scope.$watch(AuthService.isAuthenticated, function (newVal) {
        $scope.userRole = AuthService.getUserRole();
    });

    //$scope.getUserRole = function () {
    //    var role = AuthService.getUserRole();
    //    console.log('User Role in Controller1:', role);
    //    return role;
    //};

    //$scope.isAuthorized = function (roles) {
    //    return roles.includes(AuthService.getRole());
    //};

    $scope.logout = function () {
        AuthService.logout();
        $location.path("/");
    };

    console.log('User Role in Controller:', $scope.userRole);
}]);
