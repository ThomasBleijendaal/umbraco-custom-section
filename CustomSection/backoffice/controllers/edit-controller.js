(function (angular) {
    'use strict';

    angular.module('umbraco').controller('EditController', EditController);

    function EditController() {

        this.node = { Name: "Test" };

    }
})(angular);
