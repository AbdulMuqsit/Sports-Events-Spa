user.controller("RegisterOrganizerController", [
    "$scope", "$http", 'dataRepository', 'authentication', '$location', '$cookieStore', 'notification', function ($scope, $http, dataRepository, authentication, $location, $cookieStore, notification) {



        (function getCountries() {
            dataRepository.getAll("countries").then(function (data) {
                $scope.countries = data;
                $scope.country = data[0];

            }, function (data) {
                notification.error("countries could not be loaded while loading the form, please refresh the page");
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
            var url = "/Api/Account/RegisterOrganizer";
            var organizer = {
                "Link": model.link,
                "FirstName": model.firstName,
                "LastName": model.lastName,
                "LineOne": model.lineOne,
                "LineTwo": model.lineTwo,
                "CountryId": model.country ? model.country.Id : null,
                "CityId": model.city ? model.city.Id : null,
                "ContactFirstName": model.contctFirstName,
                "ContactLastName": model.contactLastName,
                "ContactLineOne": model.contactLineOne,
                "ContactLineTwo": model.contactLineTwo,
                "ContactCountryId": model.contactCountry.Id,
                "ContactCityId": model.contactCity.Id,
                "ContactZip": model.contactZip,
                "ContactPhone": model.contactPhone,
                "ContactEmail": model.contactEmail,
                "Password": model.organizationName,
                "OrganizationDescription": model.organizationDescription,
                "OrganizationLogo": model.organizationLogo,
                "Zip": model.zip,
                "Phone": model.phone
            };

            var config = { headers: { 'Authorization': "bearer " + authentication.identity.access_token } };

            $http.post(url, organizer, config).then(function (data) {
                $cookieStore.remove('identity');
                delete authentication.identity;
                $location.path("/signin/addevent");
                notification.success("Information saved please log in agian to access your updated account");


            }, function (data) {
                toastr.error("Something went wrong");

            });
        };
    }
]);