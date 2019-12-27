(function () {

    var app = angular.module("userViewer", []);

    var UsersController = function ($scope, $http, $log) {

        $scope.pageUsers = [];
        $scope.maxSize = 3;

        var onComplete = function (response) {
            $scope.users = response.data;
            $scope.totalItems = $scope.users.length;
            $scope.currentPage = 1;
            $scope.pageUsers = $scope.users.slice(0, $scope.maxSize);
        };

        var onError = function (reason) {
            $scope.error = "Could not load users.";
        };

        $scope.setPage = function (pageNo) {
            $scope.currentPage = pageNo;
            //TODO: Use $log
        };

        $scope.pageChanged = function () {
            var begin = (($scope.currentPage - 1) * $scope.numPerPage);
            var end = begin + $scope.maxSize;
            $scope.pageUsers = $scope.users.slice(begin, end);
        };

        $http.get("/api/users/").then(onComplete, onError);

        $scope.usersSortOrder = '+principalName';
    };

    app.controller("UsersController", ["$scope", "$http", "$log", UsersController]);

}());
