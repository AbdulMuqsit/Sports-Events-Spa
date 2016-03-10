user.controller('AdminPanelController', [ 
    '$scope', '$http', 'dataRepository', function($scope, $http,  dataRepository) {
        $scope.view = 'countries';

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




        $scope.switchView = function (key) {
            $scope.view = key;

        }

        //-------------------------------------------

      
    }
])