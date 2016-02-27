if (typeof String.prototype.contains === 'undefined') { String.prototype.contains = function (it) { return this.indexOf(it) != -1; }; }


var customDirectives = angular.module('customDirectives', []);
var repository = angular.module('repository', []);
var notification = angular.module('notification', []);
var events = angular.module('events', ['customDirectives', 'repository']);
var user = angular.module('user', ['repository', 'notification']);
var admin = angular.module('admin', ['repository', 'notification']);
var organizer = angular.module('organizer', ['repository', 'notification']);
var visitor = angular.module('visitor', ['repository', 'notification']);
var auth = angular.module('auth', ["ngCookies"]);
var sportsEvents = angular.module('SportsEvents', ['repository', 'admin', 'events', 'user', 'visitor', 'organizer', 'customDirectives', 'ngRoute', 'auth']).config([
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
                controller: "AddEventController"
            }).when("/registerorganizer", {
                templateUrl: base + "User/Organizer/Register/RegisterOrganizerView.html",
                controller: "RegisterOrganizerController"
            }).when("/manage", {
                templateUrl: base + "User/Organizer/ControlPanel/OrganizerControlPanelView.html",
                controller: "OrganizerControlPanelController"
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
            }).otherwise({
                redirectTo: "/"
            });
        }
]
)
.run(['$rootScope', '$location', 'authentication', 'notification', function ($rootScope, $location, authentication, notification) {

    // register listener to watch route changes
    $rootScope.$on("$routeChangeStart", function (event, next, current) {
        if (!authentication.identity) {
          

            if (!(next.templateUrl === "/App/Features/User/SignIn/SignInView.html" || next.templateUrl === "/App/Features/User/SignUp/SignUpView.html" || next.templateUrl === "/App/Features/Home/HomeView.html" || next.templateUrl === "signup")) {
                $location.path("/signin");
                notification.warning("please sign in first");
                return;
            }
        }
        if (authentication.identity) {
            if ((next.templateUrl === "/App/Features/Events/AddEvent/AddEventView.html" || next.templateUrl === "/App/Features/User/Organizer/OrganizerControlPanel.html") && !authentication.identity.roles.contains("Organizer")) {
                $location.path("/registerorganizer");
               
                return;
            }
            if ((next.templateUrl === "/App/Features/User/SignIn/SignInView.html" || next.templateUrl === "/App/Features/User/SignUp/SignUpView.html")) {
                $location.path("/");
                notification.warning("You are already signed in");
                return;
            }

        }
        //if (!authentication.identity && !(next.templateUrl === "/"|| next.templateUrl === "/App/Features/User/SignIn/SignInView.html" || next.templateUrl === "/App/Features/User/SignUp/SignUpView.html")) {
        //    $location.path("/signIn");
        //    notification.warning("You are not allowed to access this, please sign in first");
        //    return;
        //}

    });
}]);