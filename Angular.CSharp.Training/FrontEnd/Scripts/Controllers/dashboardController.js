app.controller('DashboardController', ['$scope', 'EmployeeService', function ($scope, EmployeeService) {
    $scope.departments = ['HR', 'Sales', 'Engineering'];
    $scope.designations = ['Associate', 'Software Engineer', 'Senior Engineer', 'Lead Engineer', 'Manager'];
    $scope.filters = {
        department: '',
        designation: ''
    };
    $scope.employees = [];
    $scope.employeeCount = 0;

    var departmentChart = null;
    var designationChart = null;
    var chartsInitialized = false;

    function initCharts() {
        var ctxDepartment = document.getElementById('departmentChart').getContext('2d');
        var ctxDesignation = document.getElementById('designationChart').getContext('2d');

        // Initialize new charts
        departmentChart = new Chart(ctxDepartment, {
            type: 'pie',
            data: {
                labels: [],
                datasets: [{
                    label: 'Employees by Department',
                    data: [],
                    backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56']
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
            }
        });

        designationChart = new Chart(ctxDesignation, {
            type: 'pie',
            data: {
                labels: [],
                datasets: [{
                    label: 'Employees by Designation',
                    data: [],
                    backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56', '#4BC0C0', '#9966FF']
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
            }
        });

        chartsInitialized = true;
    }

    function loadEmployees() {
        EmployeeService.getEmployees($scope.filters).then(function (response) {
            $scope.employees = response.data;
            $scope.employeeCount = $scope.employees.length;
            if (!chartsInitialized) {
                initCharts();  // Initialize charts only if not already initialized
            }
            updateCharts();
        }, function (error) {
            console.error('Error loading employees:', error);
        });
    }

    function updateCharts() {
        var departmentCounts = {};
        var designationCounts = {};

        // Filter and count the data based on selected filters
        $scope.employees.forEach(function (employee) {
            if ((!$scope.filters.department || employee.EmpDeptName === $scope.filters.department) &&
                (!$scope.filters.designation || employee.EmpDesignation === $scope.filters.designation)) {
                departmentCounts[employee.EmpDeptName] = (departmentCounts[employee.EmpDeptName] || 0) + 1;
                designationCounts[employee.EmpDesignation] = (designationCounts[employee.EmpDesignation] || 0) + 1;
            }
        });

        // Update the department chart
        departmentChart.data.labels = Object.keys(departmentCounts);
        departmentChart.data.datasets[0].data = Object.values(departmentCounts);
        departmentChart.update();

        // Update the designation chart
        designationChart.data.labels = Object.keys(designationCounts);
        designationChart.data.datasets[0].data = Object.values(designationCounts);
        designationChart.update();
    }

    $scope.$watchGroup(['filters.department', 'filters.designation'], function () {
        loadEmployees(); // Reload employees and update charts when filters change
    });

    // Initial data load
    loadEmployees();
}]);
