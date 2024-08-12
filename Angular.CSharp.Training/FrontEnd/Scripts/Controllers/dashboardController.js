app.controller('DashboardController', ['$scope', 'EmployeeService', function ($scope, EmployeeService) {
    $scope.employees = [];
    $scope.departments = ['HR', 'Sales', 'Engineering'];
    $scope.designations = ['Associate', 'Software Engineer', 'Senior Engineer', 'Lead Engineer', 'Manager'];

    $scope.filters = {
        department: '',
        designation: ''
    };

    $scope.loadEmployees = function () {
        EmployeeService.getEmployees($scope.filters).then(function (response) {
            $scope.employees = response.data;
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

//app.controller('DashboardController', ['$scope', 'EmployeeService', function ($scope, EmployeeService) {
//    $scope.employees = [];
//    $scope.filters = {
//        department: '',
//        designation: ''
//    };

//    // Function to load employees
//    $scope.loadEmployees = function (department) {
//        EmployeeService.getEmployees(department).then(function (response) {
//            $scope.employees = response.data;
//        }, function (error) {
//            console.error('Error loading employees:', error);
//        });
//    };

//    // Initial load
//    $scope.loadEmployees('');

//    // Watch for changes in the selected department
//    $scope.$watch('employee.EmpDeptName', function (newValue) {
//        $scope.loadEmployees(newValue);
//    });
//}]);
