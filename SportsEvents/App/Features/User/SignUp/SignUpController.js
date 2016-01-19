user.controller('SignUpController', ['$http', function ($http) {
    $scope.submit = function (model) {
        var url = "/Api/Account";
        var user = {
            "UserName": model.userName,
            "Password": model.pasword,
            "ConfirmPassword": model.confirmPassword,
            "Email": model.email

        };
       

        $http.post(url, user).then(function (data) {
            toastr.success("Event Created");
        }, function (data) {
            toastr.error(data);

        });
    }
}]);