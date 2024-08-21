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
    $scope.sortColumn = 'Id';
    $scope.reverseSort = false;

    // Initialize available columns for export
    $scope.availableColumns = [
        { name: 'ID', field: 'Id', selected: true },
        { name: 'First Name', field: 'EmpFirstName', selected: true },
        { name: 'Last Name', field: 'EmpLastName', selected: true },
        { name: 'Age', field: 'EmpAge', selected: true },
        { name: 'Email', field: 'EmpEmail', selected: true },
        { name: 'Designation', field: 'EmpDesignation', selected: true },
        { name: 'ManagerID', field: 'EmpManagerID', selected: true },
        { name: 'Department Name', field: 'EmpDeptName', selected: true },
        { name: 'Status', field: 'EmpStatus', selected: true },
        { name: 'Salary', field: 'EmpSalary', selected: true },
        { name: 'Location', field: 'EmpLocation', selected: true },
        { name: 'ProjectID', field: 'ProjectId', selected: true }
    ];

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
                $scope.pageChanged();
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
                    alert('Error updating employee: ' + error.data);
                });
            } else {
                EmployeeService.createEmployee($scope.employee).then(function (response) {
                    $scope.employees.push(response.data);
                    $scope.totalItems = $scope.employees.length;
                    $scope.pageChanged();
                    $('#addEditModal').modal('hide');
                    alert('Employee created successfully');
                }, function (error) {
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
            alert('Error deleting employee: ' + error.data);
        });
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
        $scope.availableColumns.forEach(function (column) {
            column.selected = selectAll;
        });
    };

    // Open the export modal
    $scope.openExportModal = function () {
        $('#exportModal').modal('show');
    };

    $scope.exportData = function () {
        let selectedColumns = $scope.availableColumns.filter(column => column.selected);
        let format = $scope.exportFormat;

        // Only proceed if columns are selected
        if (selectedColumns.length === 0) {
            alert('Please select at least one column to export.');
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
