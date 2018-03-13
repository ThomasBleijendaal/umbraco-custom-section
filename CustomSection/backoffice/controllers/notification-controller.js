(function (angular) {
    'use strict';

    angular.module('umbraco').controller('NotificationController', NotificationController);

    function NotificationController($scope, notificationsService) {
        this.notification = $scope.notification;
        this.notificationsService = notificationsService;

        this.heading = $scope.notification.args.heading;
        this.message = $scope.notification.args.message;
        this.ok = function () {
            this.notificationsService.remove(this.notification);

            this.notification.args.ok();
        }.bind(this);
        this.cancel = function () {
            this.notificationsService.remove(this.notification);

            this.notification.args.cancel();
        }.bind(this);
        
    }

})(angular);
