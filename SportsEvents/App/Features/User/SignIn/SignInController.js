user.controller("SignInController", ['$http', '$scope', 'authentication', '$location', function ($http, $scope, authentication, $location) {
        $scope.signIn = function(model) {
            authentication.authenticate(model).then(function(data) {
                $location.path('/');
            });
        }
    }
]);

