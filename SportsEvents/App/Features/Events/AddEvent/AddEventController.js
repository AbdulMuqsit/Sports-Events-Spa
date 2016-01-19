events.controller("AddEventController", [
    "$http", function($http) {
        $scope.submit = function(model) {


            function generateBase64String(url, callback, outputFormat) {
                var img = new Image();
                img.crossOrigin = "Anonymous";
                img.onload = function() {
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
                "CityId": model.cityId,
                "CountryId": model.CountryId,
                "SportId": model.sportId,
                "EventTypeId": model.eventTypeId,
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
            event.Icon = generateBase64String(model.icon);
            for (var i = 0; i < model.pictures.length; i++) {
                picturesStirngs[i] = generateBase64String(model.pictures[i]);
            }

            event.Pictures = picturesStirngs;
            $http.post(url, user).then(function(data) {

            }, function(data) {

            });
        };
    }
]);
