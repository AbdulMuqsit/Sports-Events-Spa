events.controller('AddEventController', ['$http', function ($http) {
    $scope.submit = function (model) {
        var url = "/Api/Events";
        var event = {
            "Description": model.description,
            "Details": model.details,
            "BeginTime": model.beginTime,
            "EndTime": model.endTime,
            "BeginDate": model.beginDate,
            "EndDate": model.endDate,
            "CityId": model.cityId,
            "CountryId": model.CountryId,
            "SportId": model.sportId,
            "SportTypeId": model.sportTypeId
        };

        $http.post(url, model).then(function (data) {

        });
    }
}]);