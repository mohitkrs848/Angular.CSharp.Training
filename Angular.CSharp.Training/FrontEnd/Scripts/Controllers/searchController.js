// Scripts/Controllers/searchController.js
app.controller('SearchController', ['$scope', 'EmployeeService', function ($scope, EmployeeService) {
    $scope.employees = [];
    $scope.search = {};

    $scope.searchEmployees = function () {
        var query = $scope.search.query;

        // Check if query is a number (for Employee ID) or string (for Email)
        var isEmail = !isNaN(query);
        var employeeId = isEmail ? null : query;
        var email = isEmail ? query : null;

        EmployeeService.searchEmployees(email, employeeId).then(function (response) {
            $scope.employees = response.data;
        }, function (error) {
            alert('Error searching employees: ' + error.data);
        });
    };

    $scope.editEmployee = function (employee) {
        // Implement the logic to edit employee
    };

    $scope.deleteEmployee = function (id) {
        EmployeeService.deleteEmployee(id).then(function () {
            $scope.searchEmployees(); // Refresh the search results
            alert('Employee deleted successfully');
        }, function (error) {
            alert('Error deleting employee: ' + error.data);
        });
    };
}]);
