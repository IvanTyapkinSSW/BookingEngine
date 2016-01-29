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

    $scope.init = function () {
        $scope.code = $routeParams.Code;

        $scope.expert = { isEmpty: true };
        $scope.booking = {};
        $scope.availability = [];
        $scope.sessionStarts = [];
        $scope.durations = [];

        var dt = new Date();
        $scope.booking.date = moment(dt).format("DD/MM/YYYY");
        $scope.booking.starttime = "";
        $scope.booking.endtime = "";
        $scope.booking.timezoneOffset = -dt.getTimezoneOffset() / 60;
        $scope.booking.timezoneName = /\((.*)\)/.exec(dt.toString())[1];
        $scope.booking.timezoneGMT = "GMT " + ($scope.booking.timezoneOffset < 0 ? "-" : "+") + Math.abs($scope.booking.timezoneOffset);
    };

    $scope.startchange = function () {
        $scope.recalculateDurations();
        $scope.recalculateEndTime();

        if ($scope.durations.length > 0) {
            $scope.booking.duration = $scope.durations[0];
        }
    };

    $scope.durationchange = function () {
        $scope.recalculateEndTime();
    };

    $scope.recalculateEndTime = function () {

        if ($scope.booking.starttime == null) { $scope.booking.endtime = ""; return; }

        var m = moment($scope.booking.starttime.StartDateTimeUtc, "YYYY-MM-DDTHH:mm:ss");
        var hrs = $scope.booking.duration.hours;
        var end = m.add(hrs, "h");

        $scope.booking.endtime = end.format("hh:mm A");
    };

    $scope.recalculateDurations = function () {

        if ($scope.booking.starttime == null) { $scope.durations = []; return; }

        var m = moment($scope.booking.starttime.StartDateTimeUtc, "YYYY-MM-DDTHH:mm:ss");

        for (var i = 0; i < $scope.availability.length; i++) {
            var a = $scope.availability[i];
            var start = moment(a.StartDateTimeUtc, "YYYY-MM-DDTHH:mm:ss");
            var end = moment(a.EndDateTimeUtc, "YYYY-MM-DDTHH:mm:ss");

            if (!(m.isSame(start) || m.isBetween(start, end))) continue;

            var count = moment.duration(end.diff(m)).asHours();

            $scope.loadDurations(count);
        }

    };

    $scope.loadDurations = function (count) {
        var durations = [];

        for (var i = 0; i < count; i++) {
            var hrs = i + 1;
            var d = { hours: hrs, durationFormatted: hrs + " Hour(s)" };
            durations.push(d);
        }

        $scope.durations = durations;
    };

    $scope.datechange = function () {
        var dt = moment($scope.booking.date, "DD/MM/YYYY");
        $scope.loadAvailability(dt)
    };

    $scope.loadAvailability = function (dt) {

        $scope.availability = [];
        $scope.sessionStarts = [];

        var code = $scope.code;
        var date = moment(dt).format("YYYY-MM-DDTHH:mm:ss");
        var tmz = $scope.booking.timezoneOffset;

        $http.get('/api/expert/' + code + "?date=" + date + "&timeZoneOffset=" + tmz).success(function (data) {

            $scope.expert = data;
            $scope.expert.isEmpty = false;

            $scope.availability = data.Availability;

            var maxDuration = 0;
            var sessionStarts = [];
            for (var i = 0; i < data.Availability.length; i++) {
                var block = data.Availability[i];

                block.StartTimeFormatted = moment(block.StartDateTimeUtc, "YYYY-MM-DDTHH:mm:ss").format("hh:mm A");
                block.DurationFormatted = block.Duration + " Hour(s)"
                block.EndTimeFormatted = moment(block.EndDateTimeUtc, "YYYY-MM-DDTHH:mm:ss").format("hh:mm A");

                sessionStarts = sessionStarts.concat(block.SessionStarts);

            }

            for (var i = 0; i < sessionStarts.length; i++) {
                var session = sessionStarts[i];
                session.StartTimeFormatted = moment(session.StartDateTimeUtc, "YYYY-MM-DDTHH:mm:ss").format("hh:mm A");
            }

            $scope.sessionStarts = sessionStarts;
            if (sessionStarts.length > 0) {
                $scope.booking.starttime = sessionStarts[0];
            }

            $scope.recalculateDurations();

            if ($scope.durations.length > 0) {
                $scope.booking.duration = $scope.durations[0];
            }

            $scope.recalculateEndTime();

        }).catch(function (data) { alert('error') });
    };

    $scope.isValidModel = function () {
       return $scope.booking.duration != null;
    };

    $scope.init();
    $scope.loadAvailability();

}]);

app.controller("ExpertListCtrl", ["$scope", "$http", function ($scope, $http) {

    $http.get('/api/expert').success(function (data) {
        $scope.experts = data;
    }).catch(function (data) { alert('error') });
}]);