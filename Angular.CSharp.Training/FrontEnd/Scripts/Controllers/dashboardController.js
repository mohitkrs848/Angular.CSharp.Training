app.controller('DashboardController', ['$scope', 'EmployeeService', function ($scope, EmployeeService) {
    $scope.employees = [];
    $scope.departments = [];
    $scope.designations = [];
    $scope.locations = [];
    $scope.projects = [];

    $scope.filtersVisible = false;
    $scope.chartsGraphsVisible = false;

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

    $scope.chartFilters = {
        department: '',
        designation: ''
    };

    $scope.selectedChart = 'employeeCount';
    $scope.selectedChartType = 'bar'; // New property for chart type
    $scope.employeeCount = 0;

    var employeeCountChartInstance = null;
    var employeeSalaryChartInstance = null;
    var employeeAgeChartInstance = null;
    var employeeLocationChartInstance = null;

    let debounceTimeout;

    $scope.toggleFilters = function () {
        $scope.filtersVisible = !$scope.filtersVisible;
        if ($scope.chartsGraphsVisible) {
            $scope.chartsGraphsVisible = false;
        }
    };

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
            } else if ($scope.selectedChart === 'employeeAge') {
                $scope.renderEmployeeAgeChart();
            } else if ($scope.selectedChart === 'employeeLocation') {
                $scope.renderEmployeeLocationChart();
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
            type: $scope.selectedChartType,
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
            },
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        display: true
                    },
                    tooltip: {
                        callbacks: {
                            label: function (tooltipItem) {
                                return tooltipItem.label + ': ' + tooltipItem.raw;
                            }
                        }
                    }
                }
            }
        });
    };

    $scope.renderEmployeeSalaryChart = function () {
        var ctx = document.getElementById('employeeSalaryChart').getContext('2d');
        if (employeeSalaryChartInstance) {
            employeeSalaryChartInstance.destroy();
        }
        employeeSalaryChartInstance = new Chart(ctx, {
            type: $scope.selectedChartType,
            data: {
                labels: $scope.employeesForCharts.map(function (emp) {
                    return emp.EmpFirstName + ' ' + emp.EmpLastName;
                }),
                datasets: [{
                    label: 'Salary',
                    data: $scope.employeesForCharts.map(function (emp) {
                        return emp.EmpSalary;
                    }),
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    borderColor: 'rgba(255, 99, 132, 1)',
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        display: true
                    },
                    tooltip: {
                        callbacks: {
                            label: function (tooltipItem) {
                                return tooltipItem.label + ': $' + tooltipItem.raw;
                            }
                        }
                    }
                }
            }
        });
    };

    $scope.renderEmployeeAgeChart = function () {
        var ctx = document.getElementById('employeeAgeChart').getContext('2d');
        if (employeeAgeChartInstance) {
            employeeAgeChartInstance.destroy();
        }
        employeeAgeChartInstance = new Chart(ctx, {
            type: $scope.selectedChartType,
            data: {
                labels: $scope.employeesForCharts.map(function (emp) {
                    return emp.EmpFirstName + ' ' + emp.EmpLastName;
                }),
                datasets: [{
                    label: 'Age',
                    data: $scope.employeesForCharts.map(function (emp) {
                        return emp.EmpAge;
                    }),
                    backgroundColor: 'rgba(153, 102, 255, 0.2)',
                    borderColor: 'rgba(153, 102, 255, 1)',
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        display: true
                    },
                    tooltip: {
                        callbacks: {
                            label: function (tooltipItem) {
                                return tooltipItem.label + ': ' + tooltipItem.raw + ' years';
                            }
                        }
                    }
                }
            }
        });
    };

    $scope.renderEmployeeLocationChart = function () {
        var ctx = document.getElementById('employeeLocationChart').getContext('2d');
        if (employeeLocationChartInstance) {
            employeeLocationChartInstance.destroy();
        }
        employeeLocationChartInstance = new Chart(ctx, {
            type: $scope.selectedChartType,
            data: {
                labels: $scope.locations,
                datasets: [{
                    label: 'Employee Distribution by Location',
                    data: $scope.locations.map(function (loc) {
                        return $scope.employeesForCharts.filter(function (emp) {
                            return emp.EmpLocation === loc;
                        }).length;
                    }),
                    backgroundColor: 'rgba(75, 192, 192, 0.2)',
                    borderColor: 'rgba(75, 192, 192, 1)',
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        display: true
                    },
                    tooltip: {
                        callbacks: {
                            label: function (tooltipItem) {
                                return tooltipItem.label + ': ' + tooltipItem.raw;
                            }
                        }
                    }
                }
            }
        });
    };

    $scope.selectChart = function (chartType) {
        $scope.selectedChart = chartType;
        $scope.loadCharts();
    };

    $scope.selectChartType = function (chartType) {
        $scope.selectedChartType = chartType;
        $scope.loadCharts();
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

    $scope.$watchGroup(['chartFilters.department', 'chartFilters.designation'], function () {
        if ($scope.chartsGraphsVisible) {
            clearTimeout(debounceTimeout);
            debounceTimeout = setTimeout(function () {
                $scope.loadCharts();
            }, 300);
        }
    });

    $scope.loadDistinctValues();
    $scope.loadEmployees();
}]);
