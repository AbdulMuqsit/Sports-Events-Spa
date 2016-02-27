user.controller("SignInController", ['$http', '$scope', 'authentication', '$location','$window', function ($http, $scope, authentication, $location,$window) {
        $scope.signIn = function(model) {
            authentication.authenticate(model).then(function(data) {
                $location.path('/');
                $window.location.reload();
            });
        }
    }
]);

