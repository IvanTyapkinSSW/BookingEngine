app.controller("ExpertListCtrl",
["$scope", "$http",
function ($scope, $http) {

    $http.get('/api/expert').then(
    function (response) {
        $scope.experts = response.data;
    },
    function (response) { alert('error') });

}]);