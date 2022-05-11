(function (app) {
    'use strict';
    app.controller('TransactionModeSearchCtrl', TransactionModeSearchCtrl);

    TransactionModeSearchCtrl.$inject = ['$scope','$modal', 'apiService', 'notificationService'];

    function TransactionModeSearchCtrl($scope, $modal, apiService, notificationService) {

        $scope.pageClass = 'page-TransactionMode';
        $scope.loadingTransactionMode = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.TransactionMode = [];
        $scope.search = TransactionModeSearch;
        $scope.clearSearch = clearSearch;
        $scope.search = TransactionModeSearch;
        $scope.clearSearch = clearSearch;
        $scope.openEditDialog = openEditDialog;

        function search(page) {
            page = page || 0;

            $scope.loadingTransactionMode = true;

            var config = {
                params: {
                    page: page,
                    pageSize: 4,
                    filter: $scope.filterTransactionModes
                }
            };
            debugger;
            apiService.get('api/TransactionModeRoute/transactionmodesearch/', config,
            transactionModeLoadCompleted,
            transactionModeLoadFailed);
        }

        function openEditDialog(customer) {
            $scope.EditedCustomer = customer;
            $modal.open({
                templateUrl: 'scripts/spa/transactionMode/editCustomerModal.html',
                controller: 'customerEditCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }

        function transactionModeLoadCompleted(result) {
            $scope.TransactionModes = result.data.Items;

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingTransactionMode = false;

            if ($scope.filterTransactionModes && $scope.filterTransactionModes.length) {
                notificationService.displayInfo(result.data.Items.length + ' transactionMode found');
            }

        }

        function transactionModeLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterTransactionModes = '';
            search();
        }

        $scope.TransactionModeSearch();
    }

})(angular.module('homeCinema'));