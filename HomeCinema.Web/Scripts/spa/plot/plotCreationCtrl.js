(function (app) {
    'use strict';

    app.controller('plotCreationCtrl', plotCreationCtrl);

    plotCreationCtrl.$inject = ['$scope', '$location', '$rootScope', 'apiService'];

    function plotCreationCtrl($scope, $location, $rootScope, apiService) {

        $scope.newPlot = {};

        $scope.Register = Register;

        $scope.openDatePicker = openDatePicker;
        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };
        $scope.datepicker = {};

        $scope.submission = {
            successMessages: ['Successfull submission will appear here.'],
            errorMessages: ['Submition errors will appear here.']
        };
        //Register
        //apiService.post('/api/plot/plotCreation', $scope.newPlot,
        function Register() {
            apiService.post('/api/plot/plot', $scope.newPlot,
                registerCustomerSucceded,
                registerCustomerFailed);
        }

        function registerCustomerSucceded(response) {
            $scope.submission.errorMessages = ['Submition errors will appear here.'];
            console.log(response);
            var customerRegistered = response.data;
            $scope.submission.successMessages = [];
            $scope.submission.successMessages.push($scope.newPlot.LastName + ' has been successfully registed');
            $scope.submission.successMessages.push('Check ' + customerRegistered.UniqueKey + ' for reference number');
            $scope.newPlot = {};
        }

        function registerCustomerFailed(response) {
            console.log(response);
            if (response.status == '400')
                $scope.submission.errorMessages = response.data;
            else
                $scope.submission.errorMessages = response.statusText;
        }

        function openDatePicker($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.datepicker.opened = true;
        };
    }

})(angular.module('homeCinema'));