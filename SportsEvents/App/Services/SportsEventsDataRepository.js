repository.factory('dataRepository', [
    '$http', '$q', function ($http, $q) {
        var baseUri = "/api/";
        var entityNames = {
            "city": "Cities",
            "cities": "Cities",
            "country": "Countries",
            "event": "Events",
            "countries": "Countries",
            "eventType": "EventTypes",
            "sport": "Sports",
            "eventTypes": "EventTypes",
            "sports": "Sports"
        }

        var uriColection = {
            "city": baseUri + entityNames["city"],
            "cities": baseUri + entityNames["cities"],
            "country": baseUri + entityNames["country"],
            "event": baseUri + entityNames["event"],
            "countries": baseUri + entityNames["countries"],
            "eventType": baseUri + entityNames["eventType"],
            "sport": baseUri + entityNames["sport"],
            "eventTypes": baseUri + entityNames["eventTypes"],
            "sports": baseUri + entityNames["sports"]



        };
        var subCollectionUri = function (type, id, collection) {
            return baseUri + entityNames[type] + "/" + id + "/" + entityNames[collection];
        }
        function dataRepository() {
            this.add = function (type, entity) {
                var defered = $q.defer();
                $http.post(uriColection[type], entity).then(function (data) {
                    defered.resolve(data.data);
                }, function (data) {
                    defered.reject(data);
                });
                return defered.promise;
            }
            this.getAll = function (collection) {
                var defered = $q.defer();
                var url = uriColection[collection];
                $http.get(url).then(function (data) {
                    defered.resolve(data.data);
                }, function (data) {
                    defered.reject(data);
                });
                return defered.promise;
            }

            this.getSubCollection = function (type, id, collection) {
                var defered = $q.defer();
                var url = subCollectionUri(type, id, collection);
                $http.get(url).then(function (data) {
                    defered.resolve(data.data);
                }, function (data) {
                    defered.reject(data);
                });
                return defered.promise;
            }
            this.getCalender = function (page, take) {
                var defered = $q.defer();
                var url = uriColection["event"] + "/Calender?page=" + page + "&take=" + take;
                $http.get(url).then(function (data) {
                    defered.resolve(data.data);
                }, function (data) {
                    defered.reject(data);
                });
                return defered.promise;
            }
            this.search = function (searchPhrase, sportType, eventType, startingDate, zipCode, city, startingPrice) {
                var defered = $q.defer();
                var url = uriColection["event"] + "/Search?searchPhrase=" + searchPhrase;
                //+ "&sportType=" + sportType + "&eventType=" + eventType + "&startingDate=" + startingDate + "&zipCode=" + zipCode + "&city=" + city + "&startingPrice=" + startingPrice;
                $http.get(url).then(function (data) {
                    defered.resolve(data.data);
                }, function (data) {
                    defered.reject(data);
                });
                return defered.promise;
            }
        }

        var dataRepositoryInstacne = new dataRepository();




        return dataRepositoryInstacne;
    }
]);