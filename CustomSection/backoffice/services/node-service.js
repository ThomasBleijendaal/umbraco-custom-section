(function (angular) {
    'use strict';

    angular.module('umbraco.services').factory('nodeService', NodeService);

    function NodeService($http, $q) {
        return {
            getNode: function (id) {
                var url = 'backoffice/CustomSection/Node/GetNode?id=' + id;

                return $http.get(url)
                    .then(
                    function (response) {
                        return response.data;
                    },
                    function (error) {
                        return $q.reject(error);
                    });
            },
            saveNode: function (id, node) {
                var url = 'backoffice/CustomSection/Node/SaveNode';

                return $http.post(url, node)
                    .then(
                    function (response) {
                        return true;
                    },
                    function (error) {
                        return $q.reject(error);
                    });
            },
            createNode: function (id, node) {
                var url = 'backoffice/CustomSection/Node/CreateNode';

                return $http.post(url, node)
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

