organizer.controller('OrganizerControlPanelController', ['dataRepository', 'notification', '$scope', function (dataRepository, notification, $scope) {
    dataRepository.events.getMyEvents().then(function (data) {
        $scope.myEvents = data;
    }, function () {
        notification.error("Could not load your events");
    });
    dataRepository.events.getRegistrationRequests().then(function (data) {
        $scope.registrationRequests = data;
    }, function () {
        notification.error("Could not load your events");
    }); dataRepository.events.getRegisteredUsers().then(function (data) {
        $scope.registrationRequests = data;
    }, function () {
        notification.error("Could not load your events");
    });
    dataRepository.events.getClickerUsers().then(function (data) {
        $scope.registrationRequests = data;
    }, function () {
        notification.error("Could not load your events");
    });

}])