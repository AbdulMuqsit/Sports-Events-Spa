sportsEvents.controller('HomeController', ['$scope', 'dataRepository', function ($scope, dataRepository) {

    $scope.page = 0;
    $scope.take = 20;
    $scope.loadMore = function loadCalender() {
        dataRepository.getCalender($scope.page, $scope.take).then(function (data) {
            if ($scope.events) {
               $scope.events= $scope.events.concat(data);
            } else {
                $scope.events = data;
            }
            $scope.page++;
        });

    }
    $scope.loadMore();

}])