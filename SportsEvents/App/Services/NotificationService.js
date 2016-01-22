repository.factory('notification', [function () {

    var notificationServiceInstance = {
        success: function (message) {
            toastr.success(message);
        },
        error: function (message) {
            toastr.error(message);
        }

    };

    return notificationServiceInstance;
}
]);