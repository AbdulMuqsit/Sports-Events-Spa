visitor.controller('VisitorControlPanelController', [
    '$http', '$scope', function ($http, $scope) {
        $scope.bookMarkedEvents = repository.events.getBookmarkedEvents();
        $scope.registeredEvents = repository.events.getRegisteredEvents();
        $scope.requestedEvets = repository.events.getRequestedEvents();
    }
]);