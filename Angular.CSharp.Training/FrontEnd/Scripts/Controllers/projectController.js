app.controller('ProjectController', ['$scope', 'ProjectService', function ($scope, ProjectService) {
    $scope.projects = [];
    $scope.project = {};
    $scope.editing = false;

    // Fetch all projects
    $scope.getAllProjects = function () {
        ProjectService.getAllProjects().then(function (response) {
            $scope.projects = response.data;
        }, function (error) {
            console.error('Error fetching projects:', error);
        });
    };

    // Open the Add/Edit Project Modal
    $scope.openAddEditModal = function (project) {
        $scope.editing = !!project;
        $scope.project = project ? angular.copy(project) : {};
        $('#addEditModal').modal('show');
    };

    // Save the project (Add or Update)
    $scope.saveProject = function () {
        if ($scope.editing) {
            ProjectService.updateProject($scope.project.Id, $scope.project).then(function (response) {
                $scope.getAllProjects();
                $('#addEditModal').modal('hide');
                alert('Project updated successfully!')
            }, function (error) {
                alert('Error updating project: ' + error.data);
                console.error('Error updating project:', error);
            });
        } else {
            ProjectService.addProject($scope.project).then(function (response) {
                $scope.getAllProjects();
                $('#addEditModal').modal('hide');
                alert('Project added successfully!');
            }, function (error) {
                alert('Error adding project: ' + error.data);
                console.error('Error adding project:', error);
            });
        }
    };

    // Delete the project
    $scope.deleteProject = function (id) {
        ProjectService.deleteProject(id).then(function (response) {
            $scope.getAllProjects();
        }, function (error) {
            console.error('Error deleting project:', error);
        });
    };

    // Initial fetch of projects
    $scope.getAllProjects();
}]);
