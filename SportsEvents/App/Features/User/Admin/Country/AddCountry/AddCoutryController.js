admin.controller('AddCountryController', ['dataRepository', 'notification',
function (dataRepository, notification) {
    $scope.submit = function (model) {
        var country = {
            "Name": model.name
        }
        dataRepository.add("country", country).then(
            function (data) {
                notification.success("Country Created Successfuly");

            }, function (data) {
                notification.error("Something bad happened while creating new Country");
            });
    }
}
]);