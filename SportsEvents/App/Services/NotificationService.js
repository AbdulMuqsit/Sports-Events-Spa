repository.factory('notification', [function () {

    var notificationServiceInstance = {
        success: function (title, message) {
            if (message) {
                toastr.success(title,message);
            } else {
                toastr.success(title);
            }
        },
        error: function (title, message) {
            if (message) {
                toastr.error(message, title);
            } else {
                toastr.error(title);

            }
        }

    };

    return notificationServiceInstance;
}
]);