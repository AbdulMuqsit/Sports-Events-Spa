visitor.controller('VisitorControlPanelController', [
    '$http', '$scope', 'dataRepository', function ($http, $scope, dataRepository) {

        $scope.registeredEvents = function () {

            dataRepository.registeredEvents().then(function (data) {
                $scope.events = data;
                if ($scope.model) {
                    delete $scope.model;
                }
            });
        }

        $scope.registrationRequests = function () {

            dataRepository.registrationRequests().then(function (data) {
                $scope.events = data;
                if ($scope.model) {
                    delete $scope.model;
                }
            });
        }

        $scope.bookmarkedEvents = function () {

            dataRepository.bookmarkedEvents().then(function (data) {
                $scope.events = data;
                if ($scope.model) {
                    delete $scope.model;
                }
            });
        }

        $scope.registeredEventsView = function () {
            $scope.visitorView = 'registeredEvents';
        };

        $scope.registrationRequestsView = function () {
            $scope.visitorView = 'registrationRequests';
        };

        $scope.bookmarkedEventsView = function () {
            $scope.visitorView = 'bookmarkedEvents';
        };
    }
]);