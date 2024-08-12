app.controller('DashboardController', ['$scope', 'EmployeeService', function ($scope, EmployeeService) {
    $scope.employees = [];
    $scope.departments = ['HR', 'Sales', 'Engineering'];
    $scope.designations = ['Associate', 'Software Engineer', 'Senior Engineer', 'Lead Engineer', 'Manager'];

    $scope.filters = {
        department: '',
        designation: ''
    };

    $scope.employeeCount = 0; // Initialize employeeCount

    $scope.loadEmployees = function () {
        EmployeeService.getEmployees($scope.filters).then(function (response) {
            $scope.employees = response.data;
            $scope.employeeCount = $scope.employees.length; // Update employeeCount
        }, function (error) {
            console.error('Error loading employees:', error);
        });
    };

    $scope.$watchGroup(['filters.department', 'filters.designation'], function () {
        $scope.loadEmployees();
    });

    // Initialize loading of employees
    $scope.loadEmployees();
}]);
