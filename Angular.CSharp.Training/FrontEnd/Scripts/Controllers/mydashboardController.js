app.controller('myDashboardController', ['$scope', 'EmployeeService', function ($scope, EmployeeService) {
    $scope.employees = [];
    $scope.departments = [];
    $scope.designations = [];
    $scope.locations = [];
    $scope.projects = [];

    $scope.filtersVisible = false;
    $scope.chartsGraphsVisible = false;

    // Filters for the table
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

    // Filters for the charts section
    $scope.chartFilters = {
        department: '',
        designation: ''
    };

    $scope.selectedChart = 'employeeCount';

    $scope.employeeCount = 0;

    // Store references to chart instances
    var employeeCountChartInstance = null;
    var employeeSalaryChartInstance = null;

    let debounceTimeout;

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
            $scope.employeeCount = $scope.employees.length;
        }, function (error) {
            console.error('Error loading employees:', error);
        });
    };

    $scope.loadCharts = function () {
        EmployeeService.getEmployees($scope.chartFilters).then(function (response) {
            $scope.employeesForCharts = response.data;
            if ($scope.selectedChart === 'employeeCount') {
                $scope.renderEmployeeCountChart();
            } else if ($scope.selectedChart === 'employeeSalary') {
                $scope.renderEmployeeSalaryChart();
            }
        }, function (error) {
            console.error('Error loading chart data:', error);
        });
    };

    $scope.renderEmployeeCountChart = function () {
        if ($scope.departments.length === 0 || $scope.employeesForCharts.length === 0) {
            console.log('No data to display in Employee Count chart');
            return;
        }
        var ctx = document.getElementById('employeeCountChart').getContext('2d');
        if (employeeCountChartInstance) {
            employeeCountChartInstance.destroy();
        }
        employeeCountChartInstance = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: $scope.departments,
                datasets: [{
                    label: 'Employee Count',
                    data: $scope.departments.map(function (dept) {
                        return $scope.employeesForCharts.filter(function (emp) {
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
        if (employeeSalaryChartInstance) {
            employeeSalaryChartInstance.destroy();
        }
        employeeSalaryChartInstance = new Chart(ctx, {
            type: 'pie',
            data: {
                labels: $scope.employeesForCharts.map(function (emp) {
                    return emp.EmpFirstName + ' ' + emp.EmpLastName;
                }),
                datasets: [{
                    label: 'Salary',
                    data: $scope.employeesForCharts.map(function (emp) {
                        return emp.EmpSalary;
                    }),
                    backgroundColor: $scope.employeesForCharts.map(function () {
                        return 'rgba(255, 99, 132, 0.2)';
                    }),
                    borderColor: $scope.employeesForCharts.map(function () {
                        return 'rgba(255, 99, 132, 1)';
                    }),
                    borderWidth: 1
                }]
            }
        });
    };

    $scope.selectChart = function (chartType) {
        $scope.selectedChart = chartType;
        $scope.loadCharts();
    };

    // Watchers for table filters
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

    // Watchers for chart filters
    $scope.$watchGroup(['chartFilters.department', 'chartFilters.designation'], function () {
        if ($scope.chartsGraphsVisible) {
            clearTimeout(debounceTimeout);
            debounceTimeout = setTimeout(function () {
                $scope.loadCharts();
            }, 300); // 300ms debounce delay
        }
    });

    $scope.loadDistinctValues();
    $scope.loadEmployees();
}]);
