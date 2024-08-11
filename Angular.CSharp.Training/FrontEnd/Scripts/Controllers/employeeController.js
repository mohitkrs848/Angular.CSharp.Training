app.controller('EmployeeController', ['$scope', 'EmployeeService', function ($scope, EmployeeService) {
    $scope.employees = [];
    $scope.employee = {};
    $scope.editing = false;
    $scope.searchCriteria = {};

    // Load all employees initially
    $scope.loadEmployees = function () {
        EmployeeService.getAllEmployees().then(function (response) {
            $scope.employees = response.data;
        }, function (error) {
            alert('Error loading employees: ' + error.data);
        });
    };

    // Search employees based on criteria
    $scope.searchEmployees = function () {
        EmployeeService.searchEmployees($scope.searchCriteria.email, $scope.searchCriteria.id, $scope.searchCriteria.name)
            .then(function (response) {
                $scope.employees = response.data;
                $('#searchModal').modal('hide');
            }, function (error) {
                alert('Error searching employees: ' + error.data);
            });
    };

    // Clear search criteria and reload all employees
    $scope.clearSearch = function () {
        $scope.searchCriteria = {};
        $scope.loadEmployees();
    };

    // Open the Add/Edit Employee modal
    $scope.openAddEditModal = function (employee) {
        $scope.editing = !!employee;
        $scope.employee = employee ? angular.copy(employee) : {};
        $('#addEditModal').modal('show');
    };

    // Open the Search Employee modal
    $scope.openSearchModal = function () {
        $scope.searchCriteria = {};
        $('#searchModal').modal('show');
    };

    $scope.validateEmployee = function () {
        if ($scope.employee.EmpAge < 18 || $scope.employee.EmpAge > 60) {
            alert('Employee age must be between 18 and 60.');
            return false;
        }
        if ($scope.employee.EmpSalary < 10000 || $scope.employee.EmpSalary > 10000000) {
            alert('Employee salary must be between 10000 and 10000000.');
            return false;
        }
        return true;
    };

    // Save employee (create or update)
    $scope.saveEmployee = function () {
        // Validate the age before proceeding
        if (!$scope.validateEmployee()) {
            return;
        }

        console.log('Saving employee:', $scope.employee); // Log the employee data
        if ($scope.editing) {
            EmployeeService.updateEmployee($scope.employee.Id, $scope.employee).then(function () {
                $scope.loadEmployees();
                $('#addEditModal').modal('hide');
                alert('Employee updated successfully');
            }, function (error) {
                console.error('Error updating employee:', error); // Log the error
                alert('Error updating employee: ' + error.data);
            });
        } else {
            EmployeeService.createEmployee($scope.employee).then(function (response) {
                $scope.employees.push(response.data);
                $('#addEditModal').modal('hide');
                alert('Employee created successfully');
            }, function (error) {
                console.error('Error adding employee:', error); // Log the error
                alert('Error adding employee: ' + error.data);
            });
        }
    };

    // Delete employee
    $scope.deleteEmployee = function (id) {
        EmployeeService.deleteEmployee(id).then(function () {
            $scope.loadEmployees();
            alert('Employee deleted successfully');
            console.log('Employee deleted successfully');
        }, function (error) {
            console.error('Error deleting employee:', error);
            alert('Error deleting employee: ' + error.data);
            console.log('Error deleting employee: ' + error.data);
        });
    };

    // Initial load
    $scope.loadEmployees();
}]);
