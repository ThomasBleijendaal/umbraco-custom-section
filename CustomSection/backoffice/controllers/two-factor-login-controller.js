angular.module("umbraco").controller("TwoFactorLoginController",
    function ($scope, $cookies, userService, authResource) {

        $scope.code = "";
        $scope.provider = "";
        $scope.providers = [];
        $scope.step = "loading";
        $scope.didFail = false;
        $scope.errorMsg = "";
        authResource.get2FAProviders()
            .then(function (data) {
                var provider = data[0];
                $scope.provider = provider;
                authResource.send2FACode(provider)
                    .then(function () {
                        $scope.step = "code";
                    });
            });
        
        $scope.validate = function (provider, code) {
            $scope.didFail = false;
            $scope.code = code;
            authResource.verify2FACode(provider, code)
                .then(function (data) {
                    userService.setAuthenticationSuccessful(data);
                    $scope.submit(true);
                },
                function (reason) {
                    console.log("didFail", reason);
                    $scope.didFail = true;
                    $scope.errorMsg = reason.errorMsg;
                });
        };
    });