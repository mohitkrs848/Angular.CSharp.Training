var app = angular.module("employeeApp", ["ngRoute"]);

app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider
        .when("/", {
            templateUrl: "templates/home.html"
        })
        .when("/about", {
            templateUrl: "templates/about.html"
        })
        .when("/contact", {
            templateUrl: "templates/contact.html"
        })
        .when("/employee", {
            templateUrl: "templates/employee.html",
            controller: "EmployeeController"
        })
        .otherwise({
            redirectTo: "/"
        });

    $locationProvider.html5Mode(false).hashPrefix('');
}]);
