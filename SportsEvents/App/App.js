var customDirectives = angular.module('customDirectives', []);
var events = angular.module('events', ['customDirectives']);
var user = angular.module('user', []);

var sportsEvents = angular.module('SportsEvents', ['events', 'user','customDirectives']);