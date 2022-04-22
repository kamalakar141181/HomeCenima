(function (app) {
    'use strict';

    app.controller('plotCreationCtrl', plotCreationCtrl);

    plotCreationCtrl.$inject = ['$scope', 'membershipService', 'notificationService', '$rootScope', '$location'];

    function plotCreationCtrl($scope, membershipService, notificationService, $rootScope, $location) {
        $scope.pageClass = 'page-login';
        $scope.plotCreation = plotCreation;
        $scope.user = {};

        function register() {
            membershipService.plotCreation($scope.user, plotCreationCompleted)
        }

        function plotCreationCompleted(result) {
            if (result.data.success) {
                membershipService.saveCredentials($scope.user);
                notificationService.displaySuccess('Hello ' + $scope.user.username);
                $scope.userData.displayUserInfo();
                $location.path('/');
            }
            else {
                notificationService.displayError('Registration failed. Try again.');
            }
        }
    }

})(angular.module('common.core'));