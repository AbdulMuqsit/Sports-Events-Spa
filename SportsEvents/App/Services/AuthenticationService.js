function AuthenticationService(http, cookieStore, q) {
    var loadIdentity = function () {
        return cookieStore.get("identity");
    };
    this.identity = loadIdentity();
    this.userId = null;
    this.authenticationToken = null;
    this.http = http;
    this.q = q;
    this.cookieStore = cookieStore;
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
        outerScope.cookieStore.put('identity', token);
        deffered.resolve(result);
    });
    return  deffered.promise;

}



auth.factory("authentication", [
    "$http", "$cookieStore", "$q", function($http, $cookieStore, $q) {
        var signInServiceInstance = new AuthenticationService($http, $cookieStore, $q);
        return signInServiceInstance;
    }
]);