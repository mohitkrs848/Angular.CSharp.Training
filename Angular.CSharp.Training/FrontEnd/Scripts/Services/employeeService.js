app.service('EmployeeService', ['$http', function ($http) {
    var baseUrl = 'https://localhost:44381/api/employee';

    this.getAllEmployees = function () {
        return $http.get(baseUrl);
    };

    this.getEmployeeById = function (id) {
        return $http.get(baseUrl + '/' + id);
    };

    this.createEmployee = function (employee) {
        return $http.post(baseUrl, employee);
    };

    this.updateEmployee = function (id, employee) {
        return $http.put(baseUrl + '/' + id, employee);
    };

    this.deleteEmployee = function (id) {
        return $http.delete(baseUrl + '/' + id);
    };

    this.searchEmployees = function (email, employeeId) {
        var url = baseUrl + '/search';
        var params = [];

        if (email) params.push('email=' + encodeURIComponent(email));
        if (employeeId) params.push('employeeId=' + employeeId);

        if (params.length > 0) {
            url += '?' + params.join('&');
        }

        return $http.get(url);
    };
}]);
