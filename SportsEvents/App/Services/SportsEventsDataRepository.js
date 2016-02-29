repository.factory('dataRepository', [
    '$http', '$q', 'authentication', function ($http, $q, authentication) {
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
            "sports": "Sports",
            "organizer": "Organizer"
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
            "sports": baseUri + entityNames["sports"],
            "organizer": baseUri + entityNames["organizer"]


        };
        var subCollectionUri = function (type, id, collection) {
            return baseUri + entityNames[type] + "/" + id + "/" + entityNames[collection];
        }
        function dataRepository() {

            //general http functions
            function get(url) {
                var config = null;
                if (authentication.identity) {
                    config = { headers: { 'Authorization': "bearer " + authentication.identity.access_token } };

                }
                var defered = $q.defer();
                $http.get(url, config).then(function (data) {
                    defered.resolve(data.data);
                }, function (data) {
                    defered.reject(data);
                });
                return defered.promise;
            }
            function post(url, model) {
                var config = null;
                if (authentication.identity) {
                    config = { headers: { 'Authorization': "bearer " + authentication.identity.access_token } };

                }
                var defered = $q.defer();
                $http.post(url, model, config).then(function (data) {
                    defered.resolve(data.data);
                }, function (data) {
                    defered.reject(data);
                });
                return defered.promise;
            }
            function httpDelete(url) {
                var config = null;
                if (authentication.identity) {
                    config = { headers: { 'Authorization': "bearer " + authentication.identity.access_token } };

                }
                var defered = $q.defer();
                $http.delete(url, config).then(function (data) {
                    defered.resolve(data.data);
                }, function (data) {
                    defered.reject(data);
                });
                return defered.promise;
            }
            //events repository

            this.events = {

                getMyEvents: function () {
                    var url = uriColection["event"] + "/MyEvents";

                    return get(url);
                },
                getRegisteredUsers: function () {
                    var url = uriColection["organizer"] + "/RegisteredVisitors";

                    return get(url);
                },
                getClickerUsers: function () {
                    var url = uriColection["organizer"] + "/ClickerVisitors";

                    return get(url);
                },
                getRegistrationRequests: function () {
                    var url = uriColection["organizer"] + "/RegistrationRequests";

                    return get(url);
                },
                getBookmarkerVisitors: function () {
                    var url = uriColection["organizer"] + "/BookmarkerVisitors";

                    return get(url);
                },

                getBookmarkedEvents: function () {
                    var url = uriColection["event"] + "/";

                    return get(url);
                },
                getRegisteredEvents: function () {
                    var url = uriColection["event"];
                    return get(url);
                },
                bookmark(eventId) {
                    var url = uriColection['event'] + '/BookmarkEvent';
                    var model = {
                        Id: eventId
                    };
                    return post(url, model);
                },
                register(eventId) {
                    var url = uriColection['event'] + '/RequestRegistration';
                    var model = {
                        Id: eventId
                    };
                    return post(url, model);
                },

                acceptRegistration(eventId, userId) {
                    var url = uriColection['event'] + '/AcceptRegistrationRequests';
                    var model = {
                        EventId: eventId,
                        UserId: userId
                    };
                    return post(url, model);
                },

                delete (id) {
                    var url = uriColection['event'] + '/' + id;

                    return httpDelete(url);
                }
            }
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
                var config = null;
                if (authentication.identity) {
                    config = { headers: { 'Authorization': "bearer " + authentication.identity.access_token } };

                }
                $http.get(url, config).then(function (data) {
                    defered.resolve(data.data);
                }, function (data) {
                    defered.reject(data);
                });
                return defered.promise;
            }
            this.search = function (searchPhrase, sportType, eventType, startingDate, zipCode, city, startingPrice) {
                var defered = $q.defer();

                var url = uriColection["event"] + "/Search?";
                if (searchPhrase) {
                    url += "searchPhrase=" + searchPhrase + "&";
                }
                if (sportType) {
                    url += "sportType=" + sportType.Id + "&";
                }
                if (eventType) {
                    url += "eventType=" + eventType.Id + "&";
                }
                if (startingDate) {
                    url += "startingDate=" + startingDate + "&";
                }
                if (zipCode) {
                    url += "zipCode=" + zipCode + "&";
                }
                if (city) {
                    url += "city=" + city.Id + "&";
                }
                if (startingPrice) {
                    url += "startingPrice=" + startingPrice + "&";
                }

                if (url.charAt(url.length - 1) === "&") {
                    url = url.slice(0, url.length - 1);
                }

                $http.get(url).then(function (data) {
                    defered.resolve(data.data);
                }, function (data) {
                    defered.reject(data);
                });
                return defered.promise;
            }

            this.registeredEvents = function () {
                var defered = $q.defer();
                var url = uriColection["event"] + "/RegisteredEvents";
                $http.get(url).then(function (data) {
                    defered.resolve(data.data);
                }, function (data) {
                    defered.reject(data);
                });
                return defered.promise;
            }

            this.registrationRequests = function () {
                var defered = $q.defer();
                var url = uriColection["event"] + "/RegistrationRequests";
                $http.get(url).then(function (data) {
                    defered.resolve(data.data);
                }, function (data) {
                    defered.reject(data);
                });
                return defered.promise;
            }

            this.bookmarkedEvents = function () {
                var defered = $q.defer();
                var url = uriColection["event"] + "/BookmarkedEvents";
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