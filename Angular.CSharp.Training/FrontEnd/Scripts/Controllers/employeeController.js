app.controller('EmployeeController', ['$scope', 'EmployeeService', function ($scope, EmployeeService) {
    $scope.employees = [];
    $scope.newEmployee = {};

    $scope.loadEmployees = function () {
        EmployeeService.getAllEmployees().then(function (response) {
            $scope.employees = response.data;
        }, function (error) {
            console.error('Error loading employees:', error);
        });
    };

    $scope.addEmployee = function () {
        EmployeeService.createEmployee($scope.newEmployee).then(function (response) {
            $scope.employees.push(response.data);
            $scope.newEmployee = {};
        }, function (error) {
            console.error('Error adding employee:', error);
        });
    };

    $scope.updateEmployee = function (employee) {
        EmployeeService.updateEmployee(employee.id, employee).then(function () {
            $scope.loadEmployees();
        }, function (error) {
            console.error('Error updating employee:', error);
        });
    };

    $scope.deleteEmployee = function (id) {
        EmployeeService.deleteEmployee(id).then(function () {
            $scope.loadEmployees();
        }, function (error) {
            console.error('Error deleting employee:', error);
        });
    };

    $scope.loadEmployees();
}]);
