organizer.controller('OrganizerControlPanelController', ['dataRepository', 'notification', '$scope', function (dataRepository, notification, $scope) {
    $scope.view = 'myEvents';
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
    $scope.delete = function (event) {
        dataRepository.events.delete(event.Id).then(function () {
            var index = $scope.myEvents.indexOf(event);

            if (index > -1) {
                $scope.myEvents.splice(index, 1);
            }
            notification.success('Event removed successfuly');
        });
    }
    $scope.acceptRegistration = function (event, user) {
        function findEvent(e) {
            return e.Id === event.Id;
        }
        dataRepository.events.acceptRegistration(event.Id, user.Id).then(function () {
            var index = event.Users.indexOf(user);

            if (index > -1) {
                event.Users.splice(index, 1);
            }
            notification.success("Registration Accepted");
            var ev = $scope.registeredUsers.filter(findEvent);
            if (ev && ev[0]) {
                ev[0].Users.push(user);
            } else {
                var newEvent = JSON.parse(JSON.stringify(event));
                newEvent.Users = [user];
                $scope.registeredUsers.push(newEvent);
            }
        });
    }

    $scope.switchView = function (key) {
        $scope.view = key;

    }

}])