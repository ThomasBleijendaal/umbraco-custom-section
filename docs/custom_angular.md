## Angular

In order to get our Angular controllers, directives, helpers, and
anything other included in the Umbraco backoffice, we need to add a
manifest to the root of our custom section. This file, named 
`package.manifest` contains all javascript and css files included
in our custom section.

An empty `package.manifest` looks like this:

``` json
{
  "javascript": [
    
  ],
  "css": [

  ]
}
```

To include a javascript or a style sheet, specify its full path starting
with a tilde:

``` json
{
    "javascript": [
        "~/App_Plugins/CustomSection/backoffice/controllers/dashboard-controller.js"
    ],
    "css": [
        "~/App_Plugins/CustomSection/backoffice/assets/admin.css"
    ]
}
```

After you rebuild and restart the website, any javascript and css will be applied 
and executed:

![That's working](images/custom3.png)

Keep in mind that Umbraco 7.x is still running Angular 1.1.5, which is an ancient version
of Angular. They do plan on upgrading it to 1.6 in the 8.0 release of Umbraco.

### Dashboard controller

Adding a controller is quite easy, you just have to register a controller to the `umbraco`
module in javascript, and reference the controller in your html view using `ng-controller`:

``` js
(function (angular) {
    'use strict';

    angular.module('umbraco').controller('DashboardController', DashboardController);

    function DashboardController() {

        this.nodeCount = 99;

    }

})(angular);
```

``` html
<div ng-controller="DashboardController as dashboard">
    <h1>Dashboard!</h1>

    <p>Total nodes: {{dashboard.nodeCount}}.</p>
    
</div>
```

This will result in something like this:

![Node count on dashboard](images/custom4.png)

### API Service

To provide this controller with some real data, we need to have it communicate to an API
controller. To do this, first add a new `service`, which will communicate to the backoffice
API controller:

``` js
(function (angular) {
    'use strict';

    angular.module('umbraco.services').factory('dashboardService', DashboardService);

    function DashboardService() {
        return {
            getNodeCount: function () {
                return 95;
            }
        };
    }
})(angular);
```

Don't forget to add it to the `package.manifest` file. After that, update the dashboard 
controller:

``` js
(function (angular) {
    'use strict';

    angular.module('umbraco').controller('DashboardController', DashboardController);

    function DashboardController(dashboardService) {

        this.nodeCount = dashboardService.getNodeCount();

    }

})(angular);
```

Since dependency injection already works in Angular, the custom dashboards shows the following
after building and restarting the site:

![Service working](images/custom5.png)

Updating the service to have it make an API call