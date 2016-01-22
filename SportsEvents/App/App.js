var customDirectives = angular.module('customDirectives', []);
var repository = angular.module('repository', []);
var notification = angular.module('notification', []);
var events = angular.module('events', ['customDirectives', 'repository']);
var user = angular.module('user', ['repository','notification']);
var admin = angular.module('admin', ['repository','notification']);
var sportsEvents = angular.module('SportsEvents', ['repository', 'events', 'user', 'customDirectives', 'ngRoute']).config([
        "$locationProvider", "$routeProvider",
        function ($locationProvider, $routeProvider) {
            $locationProvider.html5Mode({
                enabled: true,
                requireBase: false
            });
            var base = "/App/Features/";
            $routeProvider.when("/", {
                templateUrl: base + "Events/AddEvent/AddEventView.html",
                controller: "AddEventController"
            }).when("/addevent", {
                templateUrl: base + "Events/AddEvent/AddEventView.html",
                controller: "FeaturedProductsController"
            }).when("/admin/addcountry", {
                templateUrl: base + "User/Admin/Country/AddCountry/AddCountryView.html",
                controller: "AddCountryController"
            }).when("/admin/addcity", {
                templateUrl: base + "User/Admin/City/AddCity/AddCityView.html",
                controller: "AddCityController"
            }).when("/admin/addsport", {
                templateUrl: base + "User/Admin/Sport/AddSport/AddSportView.html",
                controller: "AddSportController"
            }).when("/admin/addeventtype", {
                templateUrl: base + "User/Admin/EventType/AddEventType/AddEventTypeView.html",
                controller: "AddEventTypeController"
            }).otherwise({
                redirectTo: "/"
            });
        }
]
);
//.run(function ($rootScope, $location, authentication) {

//    // register listener to watch route changes
//    $rootScope.$on("$routeChangeStart", function (event, next, current) {
//        if ($rootScope.loggedUser == null) {
//            if (authentication.identity && (authentication.identity.Roles && next.templateUrl === "App/ControllersViews/SignUp/SignUpView.html" || authentication.identity.Roles && next.templateUrl === "App/ControllersViews/SignUp/SignInView.html")) {
//                $location.path("/");
//            }
//        }
//    });
//});