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

    this.getEmployees = function (filters) {
        var query = '?';

        if (filters.department) {
            query += 'department=' + encodeURIComponent(filters.department);
        }
        if (filters.designation) {
            if (query !== '?') query += '&';
            query += 'designation=' + encodeURIComponent(filters.designation);
        }
        if (filters.age) {
            if (query !== '?') query += '&';
            query += 'age=' + encodeURIComponent(filters.age);
        }
        if (filters.salaryMin) {
            if (query !== '?') query += '&';
            query += 'salaryMin=' + encodeURIComponent(filters.salaryMin);
        }
        if (filters.salaryMax) {
            if (query !== '?') query += '&';
            query += 'salaryMax=' + encodeURIComponent(filters.salaryMax);
        }
        if (filters.location) {
            if (query !== '?') query += '&';
            query += 'location=' + encodeURIComponent(filters.location);
        }
        if (filters.status) {
            if (query !== '?') query += '&';
            query += 'status=' + encodeURIComponent(filters.status);
        }
        if (filters.projectId) {
            if (query !== '?') query += '&';
            query += 'projectId=' + encodeURIComponent(filters.projectId);
        }

        // Remove trailing '&' or '?' if no filters are applied
        if (query === '?') query = '';

        return $http.get(baseUrl + query);
    };

    this.getDistinctValues = function (field) {
        return $http.get(baseUrl + '/distinct-values');
    };


    // Search employees based on a single query (email, id, or name)
    this.searchEmployees = function (query) {
        var url = baseUrl + '/search';

        if (query) {
            url += '?query=' + encodeURIComponent(query);
        }

        return $http.get(url);
    };

    this.isEmailUnique = function (email, id) {
        return $http.get(baseUrl + '/checkemail', {
            params: { email: email, id: id }
        });
    };

    // Method to download the template
    this.downloadTemplate = function () {
        return $http.get(baseUrl + '/download-template', { responseType: 'arraybuffer' });
    };

    // Method to upload the file
    this.uploadFile = function (file) {
        var formData = new FormData();
        formData.append('file', file);

        return $http.post(baseUrl + '/upload', formData, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        });
    };
}]);
