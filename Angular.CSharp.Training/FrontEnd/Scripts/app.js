var app = angular.module("employeeApp", ["ngRoute"]);

app.config(function ($routeProvider) {
    $routeProvider
        .when("/", {
            templateUrl: "templates/login.html",
            controller: "LoginController"
        })
        .when("/dashboard", {
            templateUrl: "templates/employee.html",
            controller: "EmployeeController",
            resolve: {
                auth: 'AuthResolver'
            }
        })
        .when("/about", {
            templateUrl: "templates/about.html",
            resolve: {
                auth: 'AuthResolver'
            }
        })
        .when("/contact", {
            templateUrl: "templates/contact.html",
            resolve: {
                auth: 'AuthResolver'
            }
        })
        .when("/employees", {
            templateUrl: "templates/employee.html",
            controller: "EmployeeController",
            resolve: {
                auth: 'AuthResolver'
            }
        })
        .when("/search", {
            templateUrl: "templates/search.html",
            controller: "SearchController",
            resolve: {
                auth: 'AuthResolver'
            }
        })
        .otherwise({
            redirectTo: "/"
        });
});
