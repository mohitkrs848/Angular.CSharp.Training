var app = angular.module("employeeApp", ["ngRoute", "ui.bootstrap"]);

app.config(function ($routeProvider, $locationProvider) {
    $routeProvider
        .when("/", {
            templateUrl: "FrontEnd/templates/login.html",
            controller: "LoginController",
            resolve: {
                auth: function (AuthService, $location) {
                    if (AuthService.isAuthenticated()) {
                        $location.path('/employees'); // Redirect to another route if already authenticated
                    }
                }
            }
        })
        .when("/register", {
            templateUrl: "FrontEnd/templates/register.html",
            controller: "LoginController",
            resolve: {
                auth: function (AuthService, $location) {
                    if (AuthService.isAuthenticated()) {
                        $location.path('/employees'); // Redirect to another route if already authenticated
                    }
                }
            }
        })
        .when("/employees", {
            templateUrl: "FrontEnd/templates/employee.html",
            controller: "EmployeeController",
            resolve: {
                auth: function (AuthService, $location) {
                    if (!AuthService.isAuthenticated()) {
                        $location.path('/'); // Redirect to login if not authenticated
                    }
                }
            }
        })
        .when("/projects", {
            templateUrl: "FrontEnd/templates/projects.html",
            controller: "ProjectController",
            resolve: {
                auth: function (AuthService, $location) {
                    if (!AuthService.isAuthenticated() || AuthService.getUserRole() !== 'Admin') {
                        $location.path('/'); // Redirect to login if not authenticated or not admin
                    }
                }
            }
        })
        .when("/users", {
            templateUrl: 'FrontEnd/templates/users.html',
            controller: 'UserController',
            resolve: {
                auth: function (AuthService, $location) {
                    if (!AuthService.isAuthenticated() || AuthService.getUserRole() !== 'Admin') {
                        $location.path('/'); // Redirect to login if not authenticated or not admin
                    }
                }
            }
        })
        .when("/hr", {
            templateUrl: 'FrontEnd/templates/humanResource.html',
            controller: 'HRController',
            resolve: {
                auth: function (AuthService, $location) {
                    if (!AuthService.isAuthenticated() || (AuthService.getUserRole() !== 'Admin' && AuthService.getUserRole() !== 'HR')) {
                        $location.path('/'); // Redirect to login if not authenticated or not admin
                    }
                }
            }
        })
        .when("/finance", {
            templateUrl: 'FrontEnd/templates/finance.html',
            controller: 'FinanceController',
            resolve: {
                auth: function (AuthService, $location) {
                    if (!AuthService.isAuthenticated() || (AuthService.getUserRole() !== 'Admin' && AuthService.getUserRole() !== 'Finance')) {
                        $location.path('/'); // Redirect to login if not authenticated or not admin
                    }
                }
            }
        })
        .when("/dashboard", {
            templateUrl: "FrontEnd/templates/dashboard.html",
            controller: "DashboardController",
            resolve: {
                auth: function (AuthService, $location) {
                    if (!AuthService.isAuthenticated()) {
                        $location.path('/'); // Redirect to login if not authenticated
                    }
                }
            }
        })
        .when("/about", {
            templateUrl: "FrontEnd/templates/about.html",
            resolve: {
                auth: function (AuthService, $location) {
                    if (!AuthService.isAuthenticated()) {
                        $location.path('/'); // Redirect to login if not authenticated
                    }
                }
            }
        })
        .when("/contact", {
            templateUrl: "FrontEnd/templates/contact.html",
            resolve: {
                auth: function (AuthService, $location) {
                    if (!AuthService.isAuthenticated()) {
                        $location.path('/'); // Redirect to login if not authenticated
                    }
                }
            }
        })
        .otherwise({
            redirectTo: "/"
        });
});
