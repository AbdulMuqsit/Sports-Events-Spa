events.controller("AddEventController", [
    "$http", '$scope', 'dataRepository', 'notification', 'authentication', '$location', function ($http, $scope, dataRepository, notification, authentication, $location) {
        (function getCountries() {
            dataRepository.getAll("countries").then(function (data) {
                $scope.countries = data;
                $scope.country = data[0];

            }, function (data) {
                notification.error("countries could not be loaded while loading the form, please refresh the page");
            });
        })();
        (function getEventTypes() {
            dataRepository.getAll("eventTypes").then(function (data) {
                $scope.eventTypes = data;
                $scope.eventType = data[0];

            }, function (data) {
                notification.error("Event Types could not be loaded while loading the form, please refresh the page");
            });
        })();
        (function refreshSorts() {
            dataRepository.getAll("sports").then(function (data) {
                $scope.sports = data;
                $scope.sport = data[0];

            }, function (data) {
                notification.error("Sports could not be loaded while loading the form, please refresh the page");
            });
        })();
        $scope.getCities = function (country) {
            dataRepository.getSubCollection("country", country.Id, "cities").then(function (data) {
                $scope.cities = data;
            }, function (data) {
                // refreshCities();
            });
        };
        $scope.submit = function (model) {


            function generateBase64String(url, callback, outputFormat) {
                var img = new Image();
                img.crossOrigin = "Anonymous";
                img.onload = function () {
                    var canvas = document.createElement("CANVAS");
                    var ctx = canvas.getContext("2d");
                    var dataURL;
                    canvas.height = this.height;
                    canvas.width = this.width;
                    ctx.drawImage(this, 0, 0);
                    dataURL = canvas.toDataURL(outputFormat);
                    callback(dataURL);
                    canvas = null;
                };
                img.src = url;
            }

            var url = "/Api/Events";
            var event = {
                "Description": model.description,
                "Details": model.details,
                "BeginTime": model.beginTime,
                "EndTime": model.endTime,
                "BeginDate": model.beginDate,
                "EndDate": model.endDate,
                "CityId": model.city.Id,
                "CountryId": model.country.Id,
                "SportId": model.sport.Id,
                "EventTypeId": model.eventType.Id,
                "Zip": model.zip,
                "StartingPrice": model.startingPrice,
                "LineOne": model.lineOne,
                "LineTwo": model.lineTwo,
                "State": model.State,
                "VideoLink": model.videoLink,
                "ExternalLink": model.externalLink,
                "IsFeatured": model.isFeatured


            };
            var picturesStirngs = [];
            var funcs = [];
            generateBase64String(model.icon, function (base64String) {
                event.Icon = base64String;
            }
            );

            function createfunc(i) {
                return function (base64String) {
                    picturesStirngs[i] = base64String;
                };
            }

            if (model.pictures) {
                for (var i = 0; i < model.pictures.length; i++) {
                    funcs[i] = createfunc(i);

                    generateBase64String(model.pictures[i], funcs[i]);
                }
                for (var j = 0; j < model.pictures.length; j++) {
                    funcs[j]();
                }
                event.Pictures = picturesStirngs;

            }
            var config = { headers: { 'Authorization': "bearer " + authentication.identity.access_token } };

            $http.post(url, event, config).then(function (data) {
                $location.path("/manage");
                notification.success("Event Created");
            }, function (data) {
                notification.error("charlie foxtrot");

            });

        };
    }
]);
