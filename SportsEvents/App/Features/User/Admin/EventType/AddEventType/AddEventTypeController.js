﻿admin.controller('AddEventyTypeController', ['dataRepository', 'notification',
function (dataRepository, notification) {
    $scope.submit = function (model) {
        var country = {
            "Name": model.name
        }
        dataRepository.add("eventType", country).then(
            function (data) {
                notification.success("Event Type Created Successfuly");

            }, function (data) {
                notification.error("Something bad happened while creating new Event Type");
            });
    }
}
]);