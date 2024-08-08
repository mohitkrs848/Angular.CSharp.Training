var app = angular.module("employeeApp", ["ngRoute"]);

app.config(function ($routeProvider) {
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
        .when("/employees", {
            templateUrl: "templates/employee.html",
            controller: "EmployeeController"
        })
        .otherwise({
            redirectTo: "/"
        });
});
