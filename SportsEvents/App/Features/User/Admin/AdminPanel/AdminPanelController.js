user.controller('AdminPanelController', [ 
    '$scope', '$http', 'dataRepository', function($scope, $http,  dataRepository) {

        (function getCountries() {
            dataRepository.getAll("countries").then(function (data) {
                $scope.countries = data;
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
            dataRepository.getSubCollection("country", country.Id, "cities").then(function(data) {
                $scope.cities = data;
            }, function(data) {
                // refreshCities();
            });
        };


        //$scope.items = ['countries','cities', 'sports', 'eventTypes'];
        //$scope.view = $scope.items[0];

        $scope.countryView = function() {
            $scope.view = 'countries';
        };

        $scope.cityView = function () {
            $scope.view = 'cities';
        };

        $scope.sportView = function () {
            $scope.view = 'sports';
        };

        $scope.eventTypeView = function () {
            $scope.view = 'eventTypes';
        };

        //-------------------------------------------

        $scope.addCountry = function () {
            $scope.view = 'addCountry';
        };

        $scope.addCity = function () {
            $scope.view = 'addCity';
        };

        $scope.addSport = function () {
            $scope.view = 'addSport';
        };
        $scope.addEventType = function() {
            $scope.view = 'addEventType';
        };
    }
])