user.controller("SignInController", ['$http', '$scope', 'authentication', '$location', '$window', '$routeParams', function ($http, $scope, authentication, $location, $window, $routeParams) {
    $scope.signIn = function (model) {
        authentication.authenticate(model).then(function (data) {
            var path = $routeParams["redirect"] ? $routeParams["redirect"] : "/";
            $location.path(path);
            $window.location.reload();
        });
    }
}
]);

