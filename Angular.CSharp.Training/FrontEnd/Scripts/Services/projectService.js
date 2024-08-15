app.service('ProjectService', ['$http', function ($http) {
    var baseUrl = 'https://localhost:44381/api/project';

    this.getAllProjects = function () {
        return $http.get(baseUrl);
    };
    this.addProject = function (project) {
        return $http.post(baseUrl, project);
    };
    this.updateProject = function (id, project) {
        return $http.put(baseUrl + '/' + id, project);
    };
    this.deleteProject = function (id) {
        return $http.delete(baseUrl + '/' + id);
    };
}]);
