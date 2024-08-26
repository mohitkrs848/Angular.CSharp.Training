app.controller('EmployeeController', ['$scope', 'EmployeeService', 'ProjectService', 'AuthService', function ($scope, EmployeeService, ProjectService, AuthService) {
    // Initialize scope variables
    $scope.employees = [];
    $scope.filteredEmployees = [];
    $scope.employee = {};
    $scope.editing = false;
    $scope.searchCriteria = {};
    $scope.currentPage = 1;
    $scope.itemsPerPage = 10;
    $scope.totalItems = 0;
    $scope.sortColumn = 'Id';
    $scope.reverseSort = false;
    $scope.selectAll = false;

    // Initialize available columns for export
    $scope.availableColumns = [
        { name: 'ID', field: 'Id', selected: false },
        { name: 'First Name', field: 'EmpFirstName', selected: false },
        { name: 'Last Name', field: 'EmpLastName', selected: false },
        { name: 'Age', field: 'EmpAge', selected: false },
        { name: 'Email', field: 'EmpEmail', selected: false },
        { name: 'Designation', field: 'EmpDesignation', selected: false },
        { name: 'ManagerID', field: 'EmpManagerID', selected: false },
        { name: 'Department Name', field: 'EmpDeptName', selected: false },
        { name: 'Status', field: 'EmpStatus', selected: false },
        { name: 'Salary', field: 'EmpSalary', selected: false },
        { name: 'Location', field: 'EmpLocation', selected: false },
        { name: 'ProjectID', field: 'ProjectId', selected: false }
    ];

    $scope.toasts = [];

    // Function to show a toast notification
    $scope.showToast = function (title, message, duration) {
        $scope.toasts.push({
            title: title,
            message: message,
            time: new Date().toLocaleTimeString(),
            show: true
        });

        // Automatically hide the toast after the specified duration
        setTimeout(function () {
            $scope.dismissToast($scope.toasts[0]);
        }, duration);
    };

    // Function to dismiss a toast notification
    $scope.dismissToast = function (toast) {
        toast.show = false;

        setTimeout(function () {
            $scope.toasts = $scope.toasts.filter(t => t !== toast);
            $scope.$apply(); // Apply scope changes
        }, 300); // Adjust according to your fade-out duration
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

    // Load employees
    $scope.loadEmployees = function () {
        EmployeeService.getAllEmployees().then(function (response) {
            $scope.employees = response.data;
            $scope.totalItems = $scope.employees.length;
            $scope.pageChanged();
        }, function (error) {
            $scope.showToast('Error', 'Error loading employees: ' + error.data, 5000);
        });
    };

    // Fetch projects
    $scope.getAllProjects = function () {
        ProjectService.getAllProjects().then(function (response) {
            $scope.projects = response.data;
        }, function (error) {
            $scope.showToast('Error', 'Error fetching projects: ' + error.data, 5000);
        });
    };

    // Search employees
    $scope.searchEmployees = function () {
        EmployeeService.searchEmployees($scope.searchCriteria.email, $scope.searchCriteria.id, $scope.searchCriteria.name)
            .then(function (response) {
                $scope.employees = response.data;
                $scope.totalItems = $scope.employees.length;
                $scope.pageChanged();
                $('#searchModal').modal('hide');
            }, function (error) {
                $scope.showToast('Error', 'Error searching employees: ' + error.data, 5000);
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
                $scope.showToast('Validation Error', 'Employee age must be between 18 and 60.', 5000);
                reject();
            }
            if ($scope.employee.EmpSalary < 10000 || $scope.employee.EmpSalary > 10000000) {
                $scope.showToast('Validation Error', 'Employee salary must be between 10000 and 10000000.', 5000);
                reject();
            }

            EmployeeService.isEmailUnique($scope.employee.EmpEmail, $scope.editing ? $scope.employee.Id : null).then(function (response) {
                if (!response.data.isUnique) {
                    $scope.showToast('Validation Error', 'The email is already taken.', 5000);
                    reject();
                } else {
                    resolve();
                }
            }, function (error) {
                $scope.showToast('Error', 'Error checking email uniqueness: ' + error.data, 5000);
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
                    $scope.showToast('Success', 'Employee updated successfully', 5000);
                }, function (error) {
                    $scope.showToast('Error', 'Error updating employee: ' + error.data, 5000);
                });
            } else {
                EmployeeService.createEmployee($scope.employee).then(function (response) {
                    $scope.employees.push(response.data);
                    $scope.totalItems = $scope.employees.length;
                    $scope.pageChanged();
                    $('#addEditModal').modal('hide');
                    $scope.showToast('Success', 'Employee created successfully', 5000);
                }, function (error) {
                    $scope.showToast('Error', 'Error adding employee: ' + error.data, 5000);
                });
            }
        }).catch(function () {
            // Validation failed
        });
    };

    // Delete employee
    $scope.deleteEmployee = function (id) {
        if (AuthService.getUserRole() === 'Admin') {
            EmployeeService.deleteEmployee(id).then(function () {
                $scope.loadEmployees();
                $scope.showToast('Success', 'Employee deleted successfully', 5000);
            }, function (error) {
                $scope.showToast('Error', 'Error deleting employee: ' + error.data, 5000);
            });
        } else {
            $scope.showToast('Permission Denied', 'You do not have permission to delete employees.', 5000);
        }
    };

    // Handle page change
    $scope.pageChanged = function () {
        var begin = ($scope.currentPage - 1) * $scope.itemsPerPage;
        var end = begin + $scope.itemsPerPage;
        $scope.filteredEmployees = $scope.employees.slice(begin, end);
    };

    // Pagination controls
    $scope.prevPage = function () {
        if ($scope.currentPage > 1) {
            $scope.currentPage--;
            $scope.pageChanged();
        }
    };

    $scope.nextPage = function () {
        if ($scope.currentPage < Math.ceil($scope.totalItems / $scope.itemsPerPage)) {
            $scope.currentPage++;
            $scope.pageChanged();
        }
    };

    $scope.totalPages = function () {
        return Math.ceil($scope.totalItems / $scope.itemsPerPage);
    };

    $scope.selectAllColumns = function (selectAll) {
        angular.forEach($scope.availableColumns, function (column) {
            column.selected = selectAll;
        });
    };

    $scope.updateSelectAll = function () {
        $scope.selectAll = $scope.availableColumns.every(function (column) {
            return column.selected;
        });
    };
    $scope.$watch('availableColumns', function (newValue, oldValue) {
        if (newValue !== oldValue) {
            $scope.updateSelectAll();
        }
    }, true);
    // Open the export modal
    $scope.openExportModal = function () {
        $('#exportModal').modal('show');
    };

    $scope.exportData = function () {
        let selectedColumns = $scope.availableColumns.filter(column => column.selected);
        let format = $scope.exportFormat;

        // Only proceed if columns are selected
        if (selectedColumns.length === 0) {
            $scope.showToast('Validation Error', 'Please select at least one column to export.', 5000);
            return;
        }

        if (format === 'csv') {
            exportToCSV(selectedColumns);
        } else if (format === 'pdf') {
            exportToPDF(selectedColumns);
        } else if (format === 'excel') {
            exportToExcel(selectedColumns);
        }

        $('#exportModal').modal('hide');
    };

    function exportToCSV(columns) {
        let csvContent = "data:text/csv;charset=utf-8,";
        let headers = columns.map(col => col.name).join(",") + "\n";
        csvContent += headers;

        $scope.employees.forEach(function (employee) {
            let row = columns.map(col => employee[col.field]).join(",") + "\n";
            csvContent += row;
        });

        let encodedUri = encodeURI(csvContent);
        let link = document.createElement("a");
        link.setAttribute("href", encodedUri);
        link.setAttribute("download", "employees.csv");
        document.body.appendChild(link);
        link.click();
    }

    function exportToPDF(columns) {
        const { jsPDF } = window.jspdf;
        const doc = new jsPDF();

        let headers = columns.map(col => col.name);
        let rows = [];

        $scope.employees.forEach(function (employee) {
            let row = columns.map(col => employee[col.field]);
            rows.push(row);
        });

        doc.autoTable({
            head: [headers],
            body: rows
        });

        doc.save('employees.pdf');
    }

    function exportToExcel(columns) {
        let filteredEmployees = $scope.employees.map(employee => {
            let filtered = {};
            columns.forEach(col => {
                filtered[col.name] = employee[col.field];
            });
            return filtered;
        });

        let ws = XLSX.utils.json_to_sheet(filteredEmployees);
        let wb = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(wb, ws, "Employees");
        XLSX.writeFile(wb, "employees.xlsx");
    }

    // Initialize data
    $scope.loadEmployees();
    $scope.getAllProjects();
}]);
