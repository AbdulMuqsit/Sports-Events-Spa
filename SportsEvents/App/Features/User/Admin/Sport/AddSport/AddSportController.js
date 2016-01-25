admin.controller('AddSportController', ['$scope','dataRepository', 'notification',
function ($scope,dataRepository, notification) {
    $scope.submit = function (model) {
        var sport = {
            "Name": model.name
        }
        dataRepository.add("sport", sport).then(
            function (data) {
                notification.success("Sport Created Successfuly");

            }, function (data) {
                notification.error("Something bad happened while creating new Sport");
            });
    }
}
]);