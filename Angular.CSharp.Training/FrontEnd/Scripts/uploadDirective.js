app.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function () {
                scope.$apply(function () {
                    console.log('File selected:', element[0].files[0]);  // Log the file to ensure it's being selected
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);