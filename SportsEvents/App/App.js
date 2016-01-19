var customDirectives = angular.module('customDirectives', []);
var events = angular.module('events', ['customDirectives']);
var user = angular.module('user', []);

var sportsEvents = angular.module('SportsEvents', ['events', 'user', 'customDirectives', 'ngRoute']).config([
        "$locationProvider", "$routeProvider",
        function ($locationProvider, $routeProvider) {
            $locationProvider.html5Mode({
                enabled: true,
                requireBase: false
            });
            var base = "/App/Features/";
            $routeProvider.when("/", {
                templateUrl: base + "Events/AddEvent/AddEventView.html",
                controller: "FeaturedProductsController"
            }).when("/postevent", {
                templateUrl: base + "Events/AddEvent/AddEventView.html",
                controller: "FeaturedProductsController"
            }).when("/featuredservices", {
                templateUrl: base + "FeaturedServices/FeaturedServicesView.html",
                controller: "FeaturedServicesController"
            }).when("/product/:productId", {
                templateUrl: base + "ProductDetails/ProductDetailsView.html",
                controller: "ProductDetailsController"
            }).when("/signup", {
                templateUrl: base + "SignUp/SignUpView.html",
                controller: "SignUpController"
            }).when("/signin", {
                templateUrl: base + "SignIn/SignInView.html",
                controller: "SignInController"
            }).when("/controlpanel", {
                templateUrl: base + "ControlPanel/ControlPanelView.html",
                controller: "ControlPanelController"
            }).when("/searchproducts/:q?", {
                templateUrl: base + "SearchProducts/SearchProductsView.html",
                controller: "SearchProductsController"
            }).when("/searchservices/:q?", {
                templateUrl: base + "SearchServices/SearchServicesView.html",
                controller: "SearchServicesController"
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