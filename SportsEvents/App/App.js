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
                controller: "HomeController",
                caseInsensitiveMatch: true
            }).when("/addevent", {
                templateUrl: base + "Events/AddEvent/AddEventView.html",
                controller: "AddEventController",
                caseInsensitiveMatch: true
            }).when("/registerorganizer", {
                templateUrl: base + "User/Organizer/Register/RegisterOrganizerView.html",
                controller: "RegisterOrganizerController",
                caseInsensitiveMatch: true
            }).when("/manage", {
                templateUrl: base + "User/Organizer/ControlPanel/OrganizerControlPanelView.html",
                controller: "OrganizerControlPanelController",
                caseInsensitiveMatch: true
            }).when("/my", {
                templateUrl: base + "User/Visitor/VisitorControlPanelView.html",
                controller: "VisitorControlPanelController",
                caseInsensitiveMatch: true
            }).when("/signup", {
                templateUrl: base + "User/SignUp/SignUpView.html",
                controller: "SignUpController",
                caseInsensitiveMatch: true
            }).when("/signin:redirect*?", {
                templateUrl: base + "User/SignIn/SignInView.html",
                controller: "SignInController",
                caseInsensitiveMatch: true
            }).when("/admin/addcountry", {
                templateUrl: base + "User/Admin/Country/AddCountry/AddCountryView.html",
                controller: "AddCountryController",
                caseInsensitiveMatch: true
            }).when("/admin/addcity", {
                templateUrl: base + "User/Admin/City/AddCity/AddCityView.html",
                controller: "AddCityController",
                caseInsensitiveMatch: true
            }).when("/admin/addsport", {
                templateUrl: base + "User/Admin/Sport/AddSport/AddSportView.html",
                controller: "AddSportController",
                caseInsensitiveMatch: true
            }).when("/admin/addeventtype", {
                templateUrl: base + "User/Admin/EventType/AddEventType/AddEventTypeView.html",
                controller: "AddEventTypeController",
                caseInsensitiveMatch: true
            }).when("/admin/adminpanelview", {
                templateUrl: base + "User/Admin/AdminPanel/AdminPanelView.html",
                controller: "AdminPanelController",
                caseInsensitiveMatch: true
            }).when("/visitor/visitorcontrolpanelview", {
                templateUrl: base + "User/Visitor/VisitorControlPanelView.html",
                controller: "VisitorControlPanelController",
                caseInsensitiveMatch: true
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


            if (!(next.templateUrl === "/App/Features/User/SignIn/SignInView.html" || next.templateUrl === "/App/Features/User/SignUp/SignUpView.html" || next.templateUrl === "/App/Features/Home/HomeView.html" || next.originalPath === "/signup")) {
                var path = "/signin" + next.originalPath;
                $location.path(path);
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


    });
}]);