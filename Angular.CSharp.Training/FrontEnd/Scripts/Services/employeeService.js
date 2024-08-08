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

    // Method to search employees based on criteria
    this.searchEmployees = function (email, id, name) {
        var url = baseUrl + '/search';
        var params = [];

        if (email) params.push('email=' + encodeURIComponent(email));
        if (id) params.push('id=' + id);
        if (name) params.push('name=' + encodeURIComponent(name));

        if (params.length > 0) {
            url += '?' + params.join('&');
        }

        return $http.get(url);
    };
}]);
