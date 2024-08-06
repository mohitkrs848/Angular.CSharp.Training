app.controller('EmployeeController', ['$scope', 'EmployeeService', function ($scope, EmployeeService) {
    $scope.employees = [];
    $scope.employee = {}; // This will be used for both adding and editing
    $scope.editing = false;

    $scope.loadEmployees = function () {
        EmployeeService.getAllEmployees().then(function (response) {
            $scope.employees = response.data;
        }, function (error) {
            console.error('Error loading employees:', error);
        });
    };

    $scope.saveEmployee = function () {
        if ($scope.editing) {
            // Update employee
            EmployeeService.updateEmployee($scope.employee.Id, $scope.employee).then(function () {
                $scope.loadEmployees();
                $scope.cancelEdit();
            }, function (error) {
                console.error('Error updating employee:', error);
            });
        } else {
            // Add new employee
            EmployeeService.createEmployee($scope.employee).then(function (response) {
                $scope.employees.push(response.data);
                $scope.employee = {}; // Clear form
            }, function (error) {
                console.error('Error adding employee:', error);
            });
        }
    };

    $scope.editEmployee = function (employee) {
        $scope.editing = true;
        $scope.employee = angular.copy(employee); // Copy the employee data to the form
    };

    $scope.cancelEdit = function () {
        $scope.editing = false;
        $scope.employee = {}; // Clear form
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
