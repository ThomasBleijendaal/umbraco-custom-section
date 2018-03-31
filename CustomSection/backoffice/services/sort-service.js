(function (angular) {
    'use strict';

    angular.module('umbraco.services').factory('sortService', SortService);

    function SortService($http, $q) {
        return {
            getList: function (id) {
                var url = 'backoffice/CustomSection/Sort/GetList?id=' + id;

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

