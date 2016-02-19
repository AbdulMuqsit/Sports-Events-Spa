sportsEvents.controller('SearchController', ['$scope', 'dataRepository', '$rootScope', function ($scope, dataRepository, $rootScope) {


    (function getCountries() {
        dataRepository.getAll("countries").then(function (data) {
            $scope.countries = data;
            $scope.country = data[0];

        }, function (data) {
            notification.error("countries could not be loaded while loading the form, please refresh the page");
        });
    })();
    (function getEventTypes() {
        dataRepository.getAll("eventTypes").then(function (data) {
            $scope.eventTypes = data;
            $scope.eventType = data[0];

        }, function (data) {
            notification.error("Event Types could not be loaded while loading the form, please refresh the page");
        });
    })();
    (function refreshSports() {
        dataRepository.getAll("sports").then(function (data) {
            $scope.sports = data;
            $scope.sport = data[0];

        }, function (data) {
            notification.error("Sports could not be loaded while loading the form, please refresh the page");
        });
    })();
    $scope.getCities = function (country) {
        dataRepository.getSubCollection("country", country.Id, "cities").then(function (data) {
            $scope.cities = data;
        }, function (data) {
            // refreshCities();
        });
    };



    $scope.focus = function () {
        $('.navbar').addClass('show-advanced-search-options');
        $('.content').addClass('show-advanced-search-options');
        $('.navigation-section').addClass('hide');
        $('.account-section').addClass('hide');

        $('.advanced-search-options').addClass('visible');
        $scope.showAdvancedSearchOptions = true; //$scope.$apply();
    };
    var hideSearchOptions = function () {
        $('.navbar').removeClass('show-advanced-search-options');
        $('.advanced-search-options').removeClass('visible');
        $('.navigation-section').removeClass('hide');
        $('.account-section').removeClass('hide');
        $('.content').removeClass('show-advanced-search-options');
        $scope.showAdvancedSearchOptions = false; //$scope.$apply();
    }
    hideSearchOptions();
    //$scope.searchPhrase = "";
    //$scope.sportType = 0;
    //$scope.eventType = 0;
    //$scope.startingDate = null;
    //$scope.zipCode = "";
    //$scope.city = 0;
    //$scope.StartingPrice = 0.0;
    $scope.search = function (model) {

        hideSearchOptions();
        dataRepository.search(model.text, model.sport, model.eventType, model.beginDate, model.zipCode, model.city, model.startingPrice).then(function (data) {
            $rootScope.events = data;
            if (model) {
                delete model;
            }
        });

    }
}])

//$scope.page = 0;
//$scope.take = 20;
//$scope.loadMore = function loadCalender() {
//    dataRepository.getCalender($scope.page, $scope.take).then(function (data) {
//        if ($rootScope.events) {
//            $rootScope.events = $rootScope.events.concat(data);
//        } else {
//            $rootScope.events = data;
//        }
//        $scope.page++;
//    });

//}