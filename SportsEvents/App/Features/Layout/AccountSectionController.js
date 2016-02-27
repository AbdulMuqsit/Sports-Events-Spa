user.controller('AccountSectionController', ['$scope', 'authentication', function ($scope, authentication) {
    $scope.authenticated = !!authentication.identity;
    if (authentication.identity) {
        $scope.userName = authentication.identity.userName;
    }

    
    $scope.showAccountPanel=function() {
        $scope.showPanel = true;
    }
    $scope.hideAccountPanel = function () {
        $scope.showPanel = false;
    }
    $scope.signOut = function() {
        authentication.signOut();
    }
}])