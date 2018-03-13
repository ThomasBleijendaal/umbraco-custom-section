(function (angular) {
    'use strict';

    angular.module('umbraco').controller('DashboardController', DashboardController);

    function DashboardController(dashboardService, notificationsService) {

        this.nodeCount = -1;

        dashboardService.getNodeCount().then(function (count) {
            this.nodeCount = count;
        }.bind(this));

        notificationsService.success('Success');
        notificationsService.error('Error', 'Lots of errors.');
        
        notificationsService.add({
            view: '/App_Plugins/CustomSection/backoffice/notifications/notification.html',
            args: {
                heading: "Sample",
                message: "Some message",
                ok: function () {
                    console.log("OK clicked");
                },
                cancel: function () {
                    console.log("Cancel clicked");
                }
            }
        })
    }
})(angular);
