admin.controller('AddCityController', ['dataRepository', 'notification',
function (dataRepository, notification) {
    $scope.submit = function (model) {
        var city = {
            "Name": model.name,
            "CountryId": model.countryId
        }
        dataRepository.add("city", city).then(
            function (data) {
                notification.success("City Created Successfuly");

            }, function (data) {
                notification.error("Something bad happened while creating new city");
            });
    }
}
]);