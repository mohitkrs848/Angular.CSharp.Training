var app = angular.module("employeeApp", ["ngRoute"]);

app.config(function ($routeProvider) {
    $routeProvider
        .when("/", {
            templateUrl: "FrontEnd/templates/login.html",
            controller: "LoginController"
        })
        .when("/register", {
            templateUrl: "FrontEnd/templates/register.html",
            controller: "LoginController"
        })
        .when("/employees", {
            templateUrl: "FrontEnd/templates/employee.html",
            controller: "EmployeeController",
            resolve: {
                auth: function (AuthResolver) { return AuthResolver.resolve(); }
            }
        })
        .when("/projects", {
            templateUrl: "FrontEnd/templates/projects.html",
            controller: "ProjectController",
            resolve: {
                auth: function (AuthResolver) { return AuthResolver.resolve(); }
            }
        })
        .when("/dashboard/filters", {
            templateUrl: "FrontEnd/templates/dashboard-filters.html",
            controller: "FiltersController",
            resolve: {
                auth: function (AuthResolver) { return AuthResolver.resolve(); }
            }
        })
        .when("/dashboard/charts", {
            templateUrl: "FrontEnd/templates/dashboard-charts.html",
            controller: "ChartsController",
            resolve: {
                auth: function (AuthResolver) { return AuthResolver.resolve(); }
            }
        })
        .when("/about", {
            templateUrl: "FrontEnd/templates/about.html",
            resolve: {
                auth: function (AuthResolver) { return AuthResolver.resolve(); }
            }
        })
        .when("/contact", {
            templateUrl: "FrontEnd/templates/contact.html",
            resolve: {
                auth: function (AuthResolver) { return AuthResolver.resolve(); }
            }
        })
        .otherwise({
            redirectTo: "/"
        });
});
