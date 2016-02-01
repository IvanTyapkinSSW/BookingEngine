app.controller("BillingDetailsCtrl",
["$scope", "$http", "$location", "bookingService",
function ($scope, $http, $location, bookingService) {

    $scope.expert = {};
    $scope.booking = {};

    //  if (bookingService.hasBooking()) {

    var data = bookingService.get();
    if (data != null) {
        $scope.code = data.code;
        $scope.expert = data.expert;
        $scope.booking = data.booking;

        $scope.booking.subtotal = "$" + ($scope.booking.duration.hours * $scope.expert.HourlyRate);
        $scope.booking.total = $scope.booking.subtotal;
    }
    //}

    $scope.billing = {};
    $scope.billing.firstName = "";
    $scope.billing.lastName = "";
    $scope.billing.email = "";
    $scope.billing.phone = "";

    $scope.saveBooking = function () {
    };

    $scope.goBack = function () {
        if (bookingService.hasBooking()) {
            $location.path("/Expert/" + bookingService.get().code);
        }
        else {
            $location.path("/");
        }
    };

    $scope.isValidModel = function () {
        return bookingService.hasBooking() &&
            $scope.billing.firstName != "" &&
            $scope.billing.lastName != "" &&
            $scope.billing.phone != "" &&
            $scope.billing.email != "";
    };

}]);