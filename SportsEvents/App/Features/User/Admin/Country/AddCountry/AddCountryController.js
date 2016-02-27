admin.controller('AddCountryController', ['$scope', 'dataRepository', 'notification',
function ($scope, dataRepository, notification) {
    $scope.submit = function (model) {
        var country = {
            "Name": model.countryName
        }
        
        dataRepository.add("country", country).then(
            function(data) {
                notification.success("Country Created Successfuly");

            }, function(data) {
                notification.error("Something bad happened while creating new Country");
            });
        //$scope.view = 'countries';
    };
}
]);