(function (angular) {
    'use strict';

    angular.module('umbraco.services').factory('dashboardService', DashboardService);

    function DashboardService($http, $q) {
        return {
            getNodeCount: function () {
                var url = 'backoffice/CustomSection/Dashboard/GetNodeCount';

                return $http.get(url)
                    .then(
                    function (response) {
                        return response.data;
                    },
                    function (error) {
                        return $q.reject(error);
                    });
            }
        };
    }
})(angular);

