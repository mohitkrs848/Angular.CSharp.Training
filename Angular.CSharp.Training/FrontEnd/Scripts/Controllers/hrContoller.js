app.controller('HRController', ['$scope', 'EmployeeService', function ($scope, EmployeeService) {
    // Initialize scope variables
    $scope.employees = [];
    $scope.filteredEmployees = [];
    $scope.currentPage = 1;
    $scope.itemsPerPage = 10;
    $scope.totalItems = 0;
    $scope.sortColumn = 'Id';
    $scope.reverseSort = false;

    // Load employees from the backend
    $scope.loadEmployees = function () {
        EmployeeService.getAllEmployees().then(function (response) {
            $scope.employees = response.data;

            // Filter employees for the HR department
            $scope.filteredEmployees = $scope.employees.filter(emp => emp.EmpDeptName === 'HR');

            // Pagination and sorting setup
            $scope.totalItems = $scope.filteredEmployees.length;
            $scope.pageChanged();
        }, function (error) {
            console.log("Error loading employees: ", error);
        });
    };

    // Sorting function
    $scope.sortBy = function (column) {
        if ($scope.sortColumn === column) {
            $scope.reverseSort = !$scope.reverseSort;
        } else {
            $scope.sortColumn = column;
            $scope.reverseSort = false;
        }
    };

    // Pagination function
    $scope.pageChanged = function () {
        var begin = ($scope.currentPage - 1) * $scope.itemsPerPage;
        var end = begin + $scope.itemsPerPage;
        $scope.filteredEmployees = $scope.filteredEmployees.slice(begin, end);
    };

    // Functions for managing employees (edit, delete)
    $scope.openAddEditModal = function (employee) {
        // Open modal for editing or adding employees
        $scope.employee = employee ? angular.copy(employee) : {};
        $('#addEditModal').modal('show');
    };

    $scope.confirmDelete = function (id) {
        // Handle employee deletion
        EmployeeService.deleteEmployee(id).then(function () {
            $scope.loadEmployees(); // Reload after deletion
        }, function (error) {
            console.log("Error deleting employee: ", error);
        });
    };

    // Initialize the data by calling loadEmployees
    $scope.loadEmployees();
}]);
