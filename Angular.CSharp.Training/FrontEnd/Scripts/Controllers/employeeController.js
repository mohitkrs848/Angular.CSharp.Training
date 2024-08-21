app.controller('EmployeeController', ['$scope', 'EmployeeService', 'ProjectService', function ($scope, EmployeeService, ProjectService) {
    // Initialize scope variables
    $scope.employees = [];
    $scope.filteredEmployees = [];
    $scope.employee = {};
    $scope.editing = false;
    $scope.searchCriteria = {};
    $scope.currentPage = 1;
    $scope.itemsPerPage = 10;
    $scope.totalItems = 0;

    // Load employees
    $scope.loadEmployees = function () {
        EmployeeService.getAllEmployees().then(function (response) {
            $scope.employees = response.data;
            $scope.totalItems = $scope.employees.length;
            $scope.pageChanged(); // Update filtered employees
        }, function (error) {
            alert('Error loading employees: ' + error.data);
        });
    };

    // Fetch projects
    $scope.getAllProjects = function () {
        ProjectService.getAllProjects().then(function (response) {
            $scope.projects = response.data;
        }, function (error) {
            console.error('Error fetching projects:', error);
        });
    };

    // Search employees
    $scope.searchEmployees = function () {
        EmployeeService.searchEmployees($scope.searchCriteria.email, $scope.searchCriteria.id, $scope.searchCriteria.name)
            .then(function (response) {
                $scope.employees = response.data;
                $scope.totalItems = $scope.employees.length;
                $scope.pageChanged(); // Update filtered employees
                $('#searchModal').modal('hide');
            }, function (error) {
                alert('Error searching employees: ' + error.data);
            });
    };

    // Clear search
    $scope.clearSearch = function () {
        $scope.searchCriteria = {};
        $scope.loadEmployees();
    };

    // Open Add/Edit modal
    $scope.openAddEditModal = function (employee) {
        $scope.editing = !!employee;
        $scope.employee = employee ? angular.copy(employee) : {};
        $('#addEditModal').modal('show');
    };

    // Open Search modal
    $scope.openSearchModal = function () {
        $scope.searchCriteria = {};
        $('#searchModal').modal('show');
    };

    // Validate employee data
    $scope.validateEmployee = function () {
        return new Promise(function (resolve, reject) {
            if ($scope.employee.EmpAge < 18 || $scope.employee.EmpAge > 60) {
                alert('Employee age must be between 18 and 60.');
                reject();
            }
            if ($scope.employee.EmpSalary < 10000 || $scope.employee.EmpSalary > 10000000) {
                alert('Employee salary must be between 10000 and 10000000.');
                reject();
            }

            // Check email uniqueness
            EmployeeService.isEmailUnique($scope.employee.EmpEmail, $scope.editing ? $scope.employee.Id : null).then(function (response) {
                if (!response.data.isUnique) {
                    alert('The email is already taken.');
                    reject();
                } else {
                    resolve();
                }
            }, function (error) {
                alert('Error checking email uniqueness: ' + error.data);
                reject();
            });
        });
    };

    // Save employee
    $scope.saveEmployee = function () {
        $scope.validateEmployee().then(function () {
            if ($scope.editing) {
                EmployeeService.updateEmployee($scope.employee.Id, $scope.employee).then(function () {
                    $scope.loadEmployees();
                    $('#addEditModal').modal('hide');
                    alert('Employee updated successfully');
                }, function (error) {
                    console.error('Error updating employee:', error);
                    alert('Error updating employee: ' + error.data);
                });
            } else {
                EmployeeService.createEmployee($scope.employee).then(function (response) {
                    $scope.employees.push(response.data);
                    $scope.totalItems = $scope.employees.length;
                    $scope.pageChanged(); // Update filtered employees
                    $('#addEditModal').modal('hide');
                    alert('Employee created successfully');
                }, function (error) {
                    console.error('Error adding employee:', error);
                    alert('Error adding employee: ' + error.data);
                });
            }
        }).catch(function () {
            // Validation failed
        });
    };

    // Delete employee
    $scope.deleteEmployee = function (id) {
        EmployeeService.deleteEmployee(id).then(function () {
            $scope.loadEmployees();
            alert('Employee deleted successfully');
        }, function (error) {
            console.error('Error deleting employee:', error);
            alert('Error deleting employee: ' + error.data);
        });
    };

    // Handle page change
    $scope.pageChanged = function () {
        var begin = ($scope.currentPage - 1) * $scope.itemsPerPage;
        var end = begin + $scope.itemsPerPage;
        $scope.filteredEmployees = $scope.employees.slice(begin, end);
    };

    // Go to previous page
    $scope.prevPage = function () {
        if ($scope.currentPage > 1) {
            $scope.currentPage--;
            $scope.pageChanged();
        }
    };

    // Go to next page
    $scope.nextPage = function () {
        if ($scope.currentPage < Math.ceil($scope.totalItems / $scope.itemsPerPage)) {
            $scope.currentPage++;
            $scope.pageChanged();
        }
    };

    $scope.totalPages = function () {
        return Math.ceil($scope.totalItems / $scope.itemsPerPage);
    };

    // Initial load
    $scope.loadEmployees();
    $scope.getAllProjects();

    // Export to Excel
    $scope.selectedColumns = {}; // Store selected columns

    $scope.exportToExcel = function () {
        $('#columnSelectModal').modal('show');
    };

    $scope.confirmExport = function () {
        var selectedKeys = Object.keys($scope.selectedColumns).filter(key => $scope.selectedColumns[key]);

        // Filter employees based on selected columns
        var filteredEmployees = $scope.employees.map(function (employee) {
            var filteredEmployee = {};
            selectedKeys.forEach(function (key) {
                filteredEmployee[key] = employee[key];
            });
            return filteredEmployee;
        });

        // Create workbook and add data
        var wb = XLSX.utils.book_new();
        var ws = XLSX.utils.json_to_sheet(filteredEmployees);
        XLSX.utils.book_append_sheet(wb, ws, 'Employees');

        // Generate Excel file
        var wbout = XLSX.write(wb, { bookType: 'xlsx', type: 'binary' });

        function s2ab(s) {
            var buf = new ArrayBuffer(s.length);
            var view = new Uint8Array(buf);
            for (var i = 0; i < s.length; i++) view[i] = s.charCodeAt(i) & 0xFF;
            return buf;
        }

        var blob = new Blob([s2ab(wbout)], { type: 'application/octet-stream' });
        var link = document.createElement('a');
        link.href = URL.createObjectURL(blob);
        link.download = 'employees_filtered.xlsx';
        link.click();

        $('#columnSelectModal').modal('hide');
    };
}]);
