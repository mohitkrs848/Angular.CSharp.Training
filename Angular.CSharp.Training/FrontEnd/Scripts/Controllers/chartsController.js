app.controller('ChartsController', ['$scope', 'EmployeeService', function ($scope, EmployeeService) {
    $scope.employees = [];
    var departmentChart = null;
    var designationChart = null;

    function initCharts() {
        var ctxDepartment = document.getElementById('departmentChart').getContext('2d');
        var ctxDesignation = document.getElementById('designationChart').getContext('2d');

        // Destroy existing charts if they exist
        if (departmentChart) {
            departmentChart.destroy();
        }
        if (designationChart) {
            designationChart.destroy();
        }

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
            }
        });
    }

    function loadEmployees() {
        var filters = JSON.parse(localStorage.getItem('filters')) || {};
        EmployeeService.getEmployees(filters).then(function (response) {
            $scope.employees = response.data;
            updateCharts();
        }, function (error) {
            console.error('Error loading employees:', error);
        });
    }

    function updateCharts() {
        var departmentCounts = {};
        var designationCounts = {};

        $scope.employees.forEach(function (employee) {
            departmentCounts[employee.EmpDeptName] = (departmentCounts[employee.EmpDeptName] || 0) + 1;
            designationCounts[employee.EmpDesignation] = (designationCounts[employee.EmpDesignation] || 0) + 1;
        });

        departmentChart.data.labels = Object.keys(departmentCounts);
        departmentChart.data.datasets[0].data = Object.values(departmentCounts);
        departmentChart.update();

        designationChart.data.labels = Object.keys(designationCounts);
        designationChart.data.datasets[0].data = Object.values(designationCounts);
        designationChart.update();
    }

    initCharts();
    loadEmployees();
}]);
