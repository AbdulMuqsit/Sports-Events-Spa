user.controller("SignUpController", [
    "$scope", "$http", function($scope, $http) {
        $scope.submit = function(model) {
            var url = "/Api/Account/RegisterOrganizer";
            var organizer = {
                "Link": model.link,
                "FirstName": model.firstName,
                "LastName": model.lastName,
                "LineOne": model.lineOne,
                "LineTwo": model.lineTwo,
                "CountryId": model.countryId,
                "CityId": model.cityId,
                "ContactFirstName": model.contctFirstName,
                "ContactLastName": model.contactLastName,
                "ContactLineOne": model.contactLineOne,
                "ContactLineTwo": model.contactLineTwo,
                "ContactCountryId": model.contactCountryId,
                "ContactCityId": model.contactCityId,
                "ContactZip": model.contactZip,
                "ContactPhone": model.contactPhone,
                "ContactEmail": model.contactEmail,
                "Password": model.organizationName,
                "OrganizationDescription": model.organizationDescription,
                "OrganizationLogo": model.organizationLogo,
                "Zip": model.zip,
                "Phone": model.phone
            };


            $http.post(url, organizer).then(function(data) {
                toastr.success("Information saved");
            }, function(data) {
                toastr.error("Something went wrong");

            });
        };
    }
]);