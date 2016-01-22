repository.factory('dataRepository', [
    '$http', '$q', function ($http, $q) {
        var baseUri = "/api";
        var uriColection = {
            "city": baseUri + "/Cities",
            "country": baseUri + "/Countries",
             "event": baseUri + "/Events"
        };
        var dataRepositoryInstacne = {
            add: function (type, entity) {
                var defered = $q.defer();
                $http.post(uriColection[type], entity).then(function (data) {
                    defered.resolve(data);
                }, function (data) {
                    defered.reject(data);
                });
                return defered;
            }

        };

        return dataRepositoryInstacne;
    }
]);