user.controller('AccountSectionController', ['$scope', 'authentication', function ($scope, authentication) {
    $scope.authenticated = !!authentication.identity;
    if (authentication.identity) {
        $scope.userName = authentication.identity.userName;
    }
}])