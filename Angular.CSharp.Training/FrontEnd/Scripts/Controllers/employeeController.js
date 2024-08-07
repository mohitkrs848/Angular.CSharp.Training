app.controller('EmployeeController', ['$scope', 'EmployeeService', function ($scope, EmployeeService) {
    $scope.employees = [];
    $scope.employee = {};
    $scope.editing = false;

    $scope.loadEmployees = function () {
        EmployeeService.getAllEmployees().then(function (response) {
            $scope.employees = response.data;
        }, function (error) {
            alert('Error loading employees: ' + error.data);
        });
    };

    $scope.saveEmployee = function () {
        if ($scope.editing) {
            // Update employee
            EmployeeService.updateEmployee($scope.employee.Id, $scope.employee).then(function () {
                $scope.loadEmployees();
                $scope.cancelEdit();
                alert('Employee updated successfully');
            }, function (error) {
                alert('Error updating employee: ' + error.data);
            });
        } else {
            // Add new employee
            EmployeeService.createEmployee($scope.employee).then(function (response) {
                $scope.employees.push(response.data);
                $scope.employee = {};
                alert('Employee created successfully');
            }, function (error) {
                alert('Error adding employee: ' + error.data);
            });
        }
    };

    $scope.editEmployee = function (employee) {
        $scope.editing = true;
        $scope.employee = angular.copy(employee);
    };

    $scope.cancelEdit = function () {
        $scope.editing = false;
        $scope.employee = {};
    };

    $scope.deleteEmployee = function (id) {
        EmployeeService.deleteEmployee(id).then(function () {
            $scope.loadEmployees();
            alert('Employee deleted successfully');
        }, function (error) {
            alert('Error deleting employee: ' + error.data);
        });
    };

    $scope.loadEmployees();
}]);
