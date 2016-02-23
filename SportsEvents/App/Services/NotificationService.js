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
        },
        warning: function (title, message) {
            if (message) {
                toastr.warning(message, title);
            } else {
                toastr.warning(title);

            }
        }

    };

    return notificationServiceInstance;
}
]);