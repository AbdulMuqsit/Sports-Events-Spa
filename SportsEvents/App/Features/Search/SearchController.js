sportsEvents.controller('SearchController', ['$scope', 'dataRepository', function ($scope, dataRepository) {


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
    $scope.search = function (model) {

        hideSearchOptions();
    }
}])