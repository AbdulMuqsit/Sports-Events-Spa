admin.controller("AddCityController", [
    "$scope", "dataRepository", "notification",
    function ($scope, dataRepository, notification) {
        (function refreshCountries() {
            dataRepository.getAll("countries").then(function (data) {
                $scope.countries = data;
                $scope.country = data[0];

            }, function (data) {
                refreshCountries();
            });
        })();
        $scope.submit = function (model) {
            var city = {
                "Name": model.name,
                "CountryId": model.country.Id
            };
            dataRepository.add("city", city).then(
                function (data) {
                    notification.success("City Created Successfuly");

                }, function (data) {
                    notification.error("Something bad happened while creating new city");
                });
        };
       
    }
]);