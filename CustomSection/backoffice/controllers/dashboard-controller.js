(function (angular) {
    'use strict';

    angular.module('umbraco').controller('DashboardController', DashboardController);

    function DashboardController(dashboardService) {

        this.nodeCount = -1;

        dashboardService.getNodeCount().then(function (count) {
            this.nodeCount = count;
        }.bind(this));

    }

})(angular);
