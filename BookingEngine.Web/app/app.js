var app = angular.module("BookingEngine", [
  "ngRoute",
  "ngSanitize",
  "datetimepicker"
]);

app.service('bookingService', function () {

    var booking = null;

    var set = function (value) {
        booking = value;
    };

    var get = function () {
        return booking;
    };
    var hasBooking = function () { return booking != null };

    return {
        set: set,
        get: get,
        hasBooking: hasBooking
    };
});

app.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/', { templateUrl: '/app/experts-list.html', controller: 'ExpertListCtrl' }).
        when('/Expert/:Code', { templateUrl: '/app/booking.html', controller: 'BookingCtrl' }).
        when('/Booking/', { templateUrl: '/app/billing.html', controller: 'BillingDetailsCtrl' }).
        otherwise({ redirectTo: '/' });
  }]);


