var app = angular.module("BookingEngine", [
  "ngRoute",
  "ngSanitize",
  "datetimepicker"

]);

app.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/', {
            templateUrl: '/app/experts-list.html',
            controller: 'ExpertListCtrl'
        }).
           when('/:Code', {
               templateUrl: '/app/booking.html',
               controller: 'BookingCtrl'
           }).
        otherwise({
            redirectTo: '/'
        });
  }]);

//when('/:expertId', {
//    templateUrl: 'app/booking.html',
//    controller: 'BookingCtrl'
//}).


app.controller("BookingCtrl", ["$scope", "$http", '$routeParams', "$filter", function ($scope, $http, $routeParams, $filter) {

    $scope.code = $routeParams.Code;

    var expert = { isEmpty: true };
    $scope.expert = expert;

    var booking = {};
    $scope.booking = booking;

    var dt = new Date();
    booking.date = moment(dt).format("DD/MM/YYYY");
    booking.starttime = "";
    booking.duration = "1";
    booking.endtime = "";
    booking.timezoneOffset = -dt.getTimezoneOffset() / 60;
    booking.timezoneName = /\((.*)\)/.exec(dt.toString())[1];
    booking.timezoneGMT = "GMT " + (booking.timezoneOffset < 0 ? "-" : "+") + Math.abs(booking.timezoneOffset);

    $scope.availability = [];

    $scope.sessionStarts = [];

    var code = $routeParams.Code;
    var date = moment(booking.date, "DD/MM/YYYY").format("YYYY-MM-DD");
    var tmz = booking.timezoneOffset;

    $http.get('/api/expert/' + code + "?date=" + date + "&timeZoneOffset=" + tmz).success(function (data) {

        $scope.expert = data;
        $scope.expert.isEmpty = false;

        $scope.availability = data.Availability;

        var sessionStarts = [];
        for (var i = 0; i < data.Availability.length; i++) {
            sessionStarts = sessionStarts.concat(data.Availability[i].SessionStarts);
        }

        $scope.sessionStarts = sessionStarts;

        if (sessionStarts.length > 0) {
            $scope.booking.starttime = sessionStarts[0];
            $scope.recalculateEndTime();
        }

    }).catch(function (data) { alert('error') });
  

    $scope.startchange = function () { $scope.recalculateEndTime(); };
    $scope.durationchange = function () { $scope.recalculateEndTime(); };
    $scope.recalculateEndTime = function () {
  
        var m = moment($scope.booking.starttime.StartTimeLocal, "YYYY-MM-DDTHH:mm:ss");
        var hrs = $scope.booking.duration;
        var end = m.add(hrs, "h");
        booking.endtime = end.format("hh:mm A");
    };
}]);

app.controller("ExpertListCtrl", ["$scope", "$http", function ($scope, $http) {

    $http.get('/api/expert').success(function (data) {
        $scope.experts = data;
    }).catch(function (data) { alert('error') });
}]);