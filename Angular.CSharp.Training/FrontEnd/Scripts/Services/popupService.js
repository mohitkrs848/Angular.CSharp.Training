app.service('PopupService', ['$uibModal', function ($uibModal) {
    this.showPopup = function (title, message) {
        var modalInstance = $uibModal.open({
            templateUrl: 'popupTemplate.html',
            controller: 'PopupController',
            resolve: {
                title: function () {
                    return title;
                },
                message: function () {
                    return message;
                }
            }
        });
        return modalInstance;
    };
}]);

app.controller('PopupController', ['$scope', '$uibModalInstance', 'title', 'message', function ($scope, $uibModalInstance, title, message) {
    $scope.title = title;
    $scope.message = message;

    $scope.ok = function () {
        $uibModalInstance.close();
    };
}]);
