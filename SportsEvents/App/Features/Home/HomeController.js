sportsEvents.controller('HomeController', ['$scope', 'dataRepository', '$rootScope', function ($scope, dataRepository, $rootScope) {

    $scope.page = 0;
    $scope.take = 20;
    $scope.loadMore = function loadCalender() {
        dataRepository.getCalender($scope.page, $scope.take).then(function (data) {
            if ($rootScope.events) {
               $rootScope.events= $rootScope.events.concat(data);
            } else {
                $rootScope.events = data;
            }
            $scope.page++;
        });

    }
    $scope.loadMore();

}])