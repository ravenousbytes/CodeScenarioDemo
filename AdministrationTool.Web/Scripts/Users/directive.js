(function () {

    var user = function ($http) {

        var getUser = function (principalname) {
            return $http.get("/api/users/" + username)
                .then(function (response) {
                    return response.data;
                });
        };

        var getUsers = function () {
            return $http.get("/api/users/")
                .then(function (response) {
                    return response.data;
                });
        };

        return {
            getUser: getUser,
            getUsers: getUsers//,
            //addUser: addUser,
            //updateUser: updateUser,
            //deleteUser: deleteUser
        }
    };

    var module = angular.module("userViewer")
    module.factory("user", user);

}());
