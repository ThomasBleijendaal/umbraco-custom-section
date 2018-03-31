(function (angular) {
    'use strict';

    angular.module('umbraco').controller('SortController', SortController);

    function SortController(sortService, $scope, navigationService, treeService) {

        var selectedNode = $scope.dialogOptions.currentNode;
        
        this.header = selectedNode.name;

        sortService.getList(selectedNode.id)
            .then(function (nodes) {
                this.nodes = nodes;
            }.bind(this));

        this.save = function () {

            treeService.loadNodeChildren({ node: $scope.currentNode, section: $scope.currentSection });
            navigationService.hideNavigation();

        };
    }
})(angular);
