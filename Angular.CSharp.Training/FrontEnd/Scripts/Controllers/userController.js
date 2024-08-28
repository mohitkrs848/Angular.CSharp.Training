app.controller('UserController', ['$scope', '$http', function ($scope, $http) {
    $scope.users = [];
    $scope.user = {};
    $scope.editing = false;

    // Fetch all users
    $scope.getAllUsers = function () {
        $http.get('https://localhost:44381/api/user').then(function (response) {
            $scope.users = response.data;
        }, function (error) {
            console.error('Error fetching users:', error);
        });
    };

    // Open the Add/Edit User Modal
    $scope.openAddEditModal = function (user) {
        $scope.editing = !!user;
        $scope.user = user ? angular.copy(user) : {};
        $('#addEditModal').modal('show');
    };

    // Save the user (Add or Update)
    $scope.saveUser = function () {
        if ($scope.editing) {
            $http.put('https://localhost:44381/api/user/' + $scope.user.Id, $scope.user).then(function (response) {
                $scope.getAllUsers();
                $('#addEditModal').modal('hide');
                alert('User updated successfully!');
            }, function (error) {
                alert('Error updating user: ' + error.data);
                console.error('Error updating user:', error);
            });
        } else {
            $http.post('https://localhost:44381/api/user', $scope.user).then(function (response) {
                $scope.getAllUsers();
                $('#addEditModal').modal('hide');
                alert('User added successfully!');
            }, function (error) {
                alert('Error adding user: ' + error.data);
                console.error('Error adding user:', error);
            });
        }
    };

    // Delete the user
    $scope.deleteUser = function (id) {
        if (confirm('Are you sure you want to delete this user?')) {
            $http.delete('https://localhost:44381/api/user/' + id).then(function (response) {
                $scope.getAllUsers();
            }, function (error) {
                console.error('Error deleting user:', error);
            });
        }
    };

    // Initial fetch of users
    $scope.getAllUsers();
}]);
