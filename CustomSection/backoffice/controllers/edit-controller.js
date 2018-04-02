(function (angular) {
    'use strict';

    angular.module('umbraco').controller('EditController', EditController);

    function EditController(nodeService, $routeParams, notificationsService, navigationService) {

        var createNode = false;

        if ($routeParams.id.indexOf('new-') > -1) {
            createNode = true;
        }

        var id = $routeParams.id.replace('new-', '');

        var updateNavigation = function (node) {

            var pathArray = [];

            var currentNode = node;
            while (currentNode !== null && currentNode.Id > 0) {
                pathArray.unshift(currentNode.Id);

                currentNode = currentNode.ParentNode;
            }

            pathArray.unshift('-1');
            
            navigationService.syncTree({ tree: 'customTree', path: pathArray, forceReload: true });
        }

        if (createNode) {
            nodeService.getNode(id).then(function (node) {

                this.node = { ParentNode: node, Id: 0, Name: "New" };
                updateNavigation(node);

            }.bind(this));
        }
        else {
            nodeService.getNode(id).then(function (node) {

                this.node = node;
                updateNavigation(node);

            }.bind(this));
        }

        this.save = function () {
            if (createNode) {
                nodeService.createNode(id, this.node).then(function (newNode) {

                    this.node = newNode;

                    notificationsService.success("The node was created successfully.");

                    updateNavigation(this.node);

                }.bind(this));
            }
            else {
                nodeService.saveNode(id, this.node).then(function () {

                    notificationsService.success("The node was saved successfully.");

                    updateNavigation(this.node);

                }.bind(this));
            }
        }
    }
})(angular);
