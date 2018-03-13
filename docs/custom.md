[Back to index](index.md)

## Custom pages

The Custom Pages chapter is divided into multipe parts, starting with the dashboard.
The dashboard of a custom section is different compared to 'regular' custom pages,
since it comes with some default markup and it has some configuration tucked away in
`config/Dashboard.config`. More details about Dashboards [can be read here](custom_dashboard.md).

To get data into the dashboards, and any other page, you need to set up angular in
your custom section. It starts by setting up a `package.manifest` to include any javascript
and css files into Umbraco. After that, you can use anything Angular 1.1.5 provides to
create a fully working custom section. More can [be read here](custom_angular.md). 

Next, we set up [the search formatter](custom_searchformatter.md) for displaying the
search results correctly, and having the context menu's working correctly. After that,
set up [custom notifications](custom_notifications.md) and [custom dialogs](custom_dialogs.md). 

Finally, with all these parts in place, we can build the actual working parts of the
custom section by adding [controllers and pages](custom_controllers.md).