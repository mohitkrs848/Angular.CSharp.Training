// dashboardController.js
app.controller('DashboardController', ['$scope', 'EmployeeService', function ($scope, EmployeeService) {
    $scope.employees = [];
    $scope.selectedDepartment = '';

    // Function to load employees
    $scope.loadEmployees = function (department) {
        EmployeeService.getEmployees(department).then(function (response) {
            $scope.employees = response.data;
        }, function (error) {
            console.error('Error loading employees:', error);
        });
    };

    // Initial load
    $scope.loadEmployees('');

    // Watch for changes in the selected department
    $scope.$watch('employee.EmpDeptName', function (newValue) {
        $scope.loadEmployees(newValue);
    });
}]);
