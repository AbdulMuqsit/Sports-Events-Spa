var customDirectives = angular.module('customDirectives', []);
var repository = angular.module('repository', []);
var notification = angular.module('notification', []);
var events = angular.module('events', ['customDirectives', 'repository']);
var user = angular.module('user', ['repository', 'notification']);
var admin = angular.module('admin', ['repository', 'notification']);
var organizer = angular.module('organizer', ['repository', 'notification']);
var visitor = angular.module('visitor', ['repository', 'notification']);

var auth = angular.module('auth', ["ngCookies"]);
var sportsEvents = angular.module('SportsEvents', ['repository', 'admin', 'events', 'user', 'visitor', 'customDirectives', 'ngRoute', 'auth']).config([
        "$locationProvider", "$routeProvider",
        function ($locationProvider, $routeProvider) {
            $locationProvider.html5Mode({
                enabled: true,
                requireBase: false
            });
            var base = "/App/Features/";
            $routeProvider.when("/", {
                templateUrl: base + "Home/HomeView.html",
                controller: "HomeController"
            }).when("/addevent", {
                templateUrl: base + "Events/AddEvent/AddEventView.html",
                controller: "FeaturedProductsController"
            }).when("/registerorganizer", {
                templateUrl: base + "User/Organizer/Register/RegisterOrganizerView.html",
                controller: "RegisterOrganizerController"
            }).when("/my", {
                templateUrl: base + "User/Visitor/VisitorControlPanelView.html",
                controller: "VisitorControlPanelController"
            }).when("/signup", {
                templateUrl: base + "User/SignUp/SignUpView.html",
                controller: "SignUpController"
            }).when("/signin", {
                templateUrl: base + "User/SignIn/SignInView.html",
                controller: "SignInController"
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
            }).when("/admin/adminpanelview", {
                templateUrl: base + "User/Admin/AdminPanel/AdminPanelView.html",
                controller: "AdminPanelController"
            }).when("/visitor/visitorcontrolpanelview", {
                templateUrl: base + "User/Visitor/VisitorControlPanelView.html",
                controller: "VisitorControlPanelController"
            }).otherwise({
                redirectTo: "/"
            });
        }
]
)
.run(['$rootScope', '$location', 'authentication', 'notification', function ($rootScope, $location, authentication, notification) {

    // register listener to watch route changes
    $rootScope.$on("$routeChangeStart", function (event, next, current) {
        if (!authentication.identity && !(next.templateUrl === "/", next.templateUrl === "/App/Features/User/SignIn/SignInView.html" || next.templateUrl === "/App/Features/User/SignUp/SignUpView.html")) {
            $location.path("/signIn");
            notification.warning("You are not allowed to access this, please sign in first");
            return;
        }
        if (authentication.identity && (next.templateUrl === "/App/Features/User/SignIn/SignInView.html" || next.templateUrl === "/App/Features/User/SignUp/SignUpView.html")) {
            $location.path("/");
            notification.warning("You are already signed in");
            return;
        }

    });
}]);