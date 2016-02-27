function AuthenticationService(http, cookies, q,location,window) {
    var loadIdentity = function () {
        return cookies.get("identity");
    };
    this.identity = loadIdentity();
    this.userId = null;
    this.authenticationToken = null;
    this.http = http;
    this.q = q;
    this.cookies = cookies;
    this.location = location;
    this.window = window;
}

AuthenticationService.prototype.signOut=function() {
    this.cookies.remove('identity');
    this.location.path("/");
    this.window.location.reload();

}
AuthenticationService.prototype.authenticate = function (model) {
    var deffered = this.q.defer();
    var outerScope = this;

    var temp = this.http({
        method: "post",
        url: "/Token",
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        transformRequest: function(obj) {
            var str = [];
            for (var p in obj)
                str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
            return str.join("&");
        },

        data: { grant_type: "password", username: model.userName, password: model.password }
    });
    temp.success(function(result) {
        var token = result;
        outerScope.cookies.put('identity', token);
        deffered.resolve(result);
    });
    return  deffered.promise;

}



auth.factory("authentication", [
    "$http", "$cookieStore", "$q", '$location', '$window', function ($http, $cookieStore, $q, $location, $window) {
        var signInServiceInstance = new AuthenticationService($http, $cookieStore, $q, $location,$window);
        return signInServiceInstance;
    }
]);