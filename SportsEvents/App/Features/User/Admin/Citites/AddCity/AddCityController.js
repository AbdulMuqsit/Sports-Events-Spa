admin.controller('AddCityController', ['repository', 'notification',
function (repository, notification) {
    $scope.submit = function (model) {
        var city = {
            "Name": model.name,
            "CountryId": model.countryId
        }
        repository.add("city", city).then(
            function (data) {
                notification.success("City Created Successfuly");

            }, function (data) {
                notification.error("Something bad happened while creating new city");
            });
    }
}
]);