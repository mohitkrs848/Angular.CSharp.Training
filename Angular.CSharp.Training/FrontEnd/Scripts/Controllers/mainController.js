app.controller('MainController', ['$scope', '$location', 'AuthService', function ($scope, $location, AuthService) {
    $scope.isAuthenticated = function () {
        return AuthService.isAuthenticated();
    };

    $scope.$watch(AuthService.isAuthenticated, function (newVal) {
        $scope.userRole = AuthService.getUserRole();
    });

    $scope.showLogoutConfirmation = function () {
        $('#logoutConfirmationModal').modal('show');
    };

    $scope.confirmLogout = function () {
        $('#logoutConfirmationModal').modal('hide');
        $scope.logout();
    };

    $scope.logout = function () {
        AuthService.logout();
        $location.path("/");
    };

    console.log('User Role in Controller:', $scope.userRole);
}]);
