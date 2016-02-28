sportsEvents.controller('HomeController', ['$scope', 'dataRepository', '$rootScope', 'authentication', '$location', function ($scope, dataRepository, $rootScope, authentication, $location) {

    $scope.page = 0;
    $scope.take = 20;
    $scope.loadMore = function loadCalender() {
        dataRepository.getCalender($scope.page, $scope.take).then(function (data) {
            if ($rootScope.events) {
                $rootScope.events = $rootScope.events.concat(data);
            } else {
                $rootScope.events = data;
            }
            $scope.page++;
        });

    }

    $scope.bookmark = function (event) {
        if (!authentication.identity) {
            $location.path('/signin');
            return;
        }
        dataRepository.events.bookmark(event.Id).then(function (data) {
            event.Bookmarked = true;
        });


    }

    $scope.register = function (event) {
        if (!authentication.identity) {
            $location.path('/signin');
            return;
        }
        dataRepository.events.register(event.Id).then(function (data) {
            event.RequestedRegistration = true;
        });
    }
    $scope.loadMore();

}]);
