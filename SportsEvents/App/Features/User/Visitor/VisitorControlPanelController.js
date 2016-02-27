visitor.controller('VisitorControlPanelController', [
    '$http', '$scope', 'dataRepository', function($http, $scope, dataRepository) {

        (function getRegisteredEvents() {
            dataRepository.registeredEvents().then(function(data) {
                $scope.regEvents = data;
            }, function(data) {
                notification.error("Registered Events could not be loaded, please refresh the page");
            });
        })();

        (function getRegistrationRequests() {
            dataRepository.registrationRequests().then(function (data) {
                $scope.regRequests = data;
            }, function (data) {
                notification.error("Registration Requests could not be loaded, please refresh the page");
            });
        })();

        (function getBookmarkedEvents() {
            dataRepository.bookmarkedEvents().then(function (data) {
                $scope.bmEvents = data;
            }, function (data) {
                notification.error("Bookmarked Events could not be loaded, please refresh the page");
            });
        })();

        
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