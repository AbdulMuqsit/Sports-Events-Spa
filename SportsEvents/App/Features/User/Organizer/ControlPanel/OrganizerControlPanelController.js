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
        $scope.registeredUsers = data;
    }, function () {
        notification.error("Could not load your events");
    });
    dataRepository.events.getClickerUsers().then(function (data) {
        $scope.clickerUsers = data;
    }, function () {
        notification.error("Could not load your events");
    });
    dataRepository.events.getBookmarkerVisitors().then(function (data) {
        $scope.bookmarkerVisitors = data;
    }, function () {
        notification.error("Could not load your events");
    });
    $scope.switchView = function (key) {
        $scope.view = key;
        //obviously i have to work on something you know :P you -_- :P important km kr rai :D kia to dekho na dekho to kia hua
        // koi scene to nai ana na? baten mujhe smjh nai aari aarai me sun nai rai soch rai hn shuru kr dun :P checking krti hn  apki :P mazak kr rai hn wese sun'ney k barey me serious thi :P what bolo pta ni kch ni hta. :) ok  
    }

}])