var app = angular.module("employeeApp", ["ngRoute"]);

app.config(function ($routeProvider) {
    $routeProvider
        .when("/", {
            templateUrl: "FrontEnd/templates/login.html",
            controller: "LoginController"
        })
        .when("/dashboard", {
            templateUrl: "FrontEnd/templates/employee.html",
            controller: "EmployeeController",
            resolve: {
                auth: 'AuthResolver'
            }
        })
        .when("/about", {
            templateUrl: "FrontEnd/templates/about.html",
            resolve: {
                auth: 'AuthResolver'
            }
        })
        .when("/contact", {
            templateUrl: "FrontEnd/templates/contact.html",
            resolve: {
                auth: 'AuthResolver'
            }
        })
        .when("/employees", {
            templateUrl: "FrontEnd/templates/employee.html",
            controller: "EmployeeController",
            resolve: {
                auth: 'AuthResolver'
            }
        })
        //.when("/search", {
        //    templateUrl: "templates/search.html",
        //    controller: "SearchController",
        //    resolve: {
        //        auth: 'AuthResolver'
        //    }
        //})
        .otherwise({
            redirectTo: "/"
        });
});
