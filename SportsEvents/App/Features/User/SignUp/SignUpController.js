user.controller('SignUpController', ['$scope', '$http', '$location', function ($scope, $http, $location) {
    $scope.submit = function (model) {
        var url = "/Api/Account/Register";
        var user = {
            "UserName": model.userName,
            "Password": model.password,
            "ConfirmPassword": model.confirmPassword,
            "Email": model.email

        };


        $http.post(url, user).then(function (data) {
            toastr.success("Account Created");
            $location.path("/signin");
        }, function (data) {
            toastr.error("Account could not be created");

        });
    }
}]);