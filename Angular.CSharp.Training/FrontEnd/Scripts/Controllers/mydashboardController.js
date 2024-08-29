app.controller('myDashboardController', ['$scope', 'EmployeeService', function ($scope, EmployeeService) {
    $scope.employees = [];
    $scope.departments = [];
    $scope.designations = [];
    $scope.locations = [];
    $scope.projects = [];

    $scope.filtersVisible = false; // Initialize filters visibility
    $scope.chartsGraphsVisible = false; // Initialize charts/graphs visibility

    $scope.filters = {
        department: '',
        designation: '',
        age: null,
        salaryMin: null,
        salaryMax: null,
        location: '',
        status: '',
        projectId: null
    };

    $scope.employeeCount = 0; // Initialize employeeCount

    // Toggle the visibility of the filters section
    $scope.toggleFilters = function () {
        $scope.filtersVisible = !$scope.filtersVisible;
        if ($scope.chartsGraphsVisible) {
            $scope.chartsGraphsVisible = false;
        }
    };

    // Toggle the visibility of the charts/graphs section
    $scope.toggleChartsGraphs = function () {
        $scope.chartsGraphsVisible = !$scope.chartsGraphsVisible;
        if ($scope.chartsGraphsVisible) {
            $scope.loadCharts();
        }
        if ($scope.filtersVisible) {
            $scope.filtersVisible = false;
        }
    };

    $scope.loadDistinctValues = function () {
        EmployeeService.getDistinctValues().then(function (response) {
            $scope.departments = response.data.Departments;
            $scope.designations = response.data.Designations;
            $scope.locations = response.data.Locations;
            $scope.projects = response.data.Projects;
        }, function (error) {
            console.error('Error loading distinct values:', error);
        });
    };

    $scope.loadEmployees = function () {
        EmployeeService.getEmployees($scope.filters).then(function (response) {
            $scope.employees = response.data;
            $scope.employeeCount = $scope.employees.length; // Update employeeCount
        }, function (error) {
            console.error('Error loading employees:', error);
        });
    };

    $scope.loadCharts = function () {
        // Logic to load and render charts when the Charts & Graphs section is opened
        $scope.renderEmployeeCountChart();
        $scope.renderEmployeeSalaryChart();
    };

    $scope.renderEmployeeCountChart = function () {
        var ctx = document.getElementById('employeeCountChart').getContext('2d');
        var chart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: $scope.departments, // Assuming this is an array of department names
                datasets: [{
                    label: 'Employee Count',
                    data: $scope.departments.map(function (dept) {
                        return $scope.employees.filter(function (emp) {
                            return emp.EmpDeptName === dept;
                        }).length;
                    }),
                    backgroundColor: 'rgba(54, 162, 235, 0.2)',
                    borderColor: 'rgba(54, 162, 235, 1)',
                    borderWidth: 1
                }]
            }
        });
    };

    $scope.renderEmployeeSalaryChart = function () {
        var ctx = document.getElementById('employeeSalaryChart').getContext('2d');
        var chart = new Chart(ctx, {
            type: 'pie',
            data: {
                labels: $scope.employees.map(function (emp) {
                    return emp.EmpFirstName + ' ' + emp.EmpLastName;
                }),
                datasets: [{
                    label: 'Salary',
                    data: $scope.employees.map(function (emp) {
                        return emp.EmpSalary;
                    }),
                    backgroundColor: $scope.employees.map(function () {
                        return 'rgba(255, 99, 132, 0.2)';
                    }),
                    borderColor: $scope.employees.map(function () {
                        return 'rgba(255, 99, 132, 1)';
                    }),
                    borderWidth: 1
                }]
            }
        });
    };

    $scope.$watchGroup([
        'filters.department',
        'filters.designation',
        'filters.age',
        'filters.salaryMin',
        'filters.salaryMax',
        'filters.location',
        'filters.status',
        'filters.projectId'
    ], function () {
        $scope.loadEmployees();
    });

    // Initialize loading of distinct values and employees
    $scope.loadDistinctValues();
    $scope.loadEmployees();
}]);
