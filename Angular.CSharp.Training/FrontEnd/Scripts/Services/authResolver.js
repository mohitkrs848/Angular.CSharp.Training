app.service('AuthResolver', ['$location', 'AuthService', function ($location, AuthService) {
    this.resolve = function () {
        if (!AuthService.isAuthenticated()) {
            $location.path("/");
        }
    };
}]);
