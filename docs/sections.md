[Back to index](index.md)

## Multiple sections

Having multiple custom section is quite easy, you just have to add extra `IApplication`s to your source
and provide the correct translations for each of the `alias`es. So something like this is perfectly fine:

```cs
using umbraco.businesslogic;
using umbraco.interfaces;

namespace UmbracoCustomSection.App_Plugins.CustomSection
{
    [Application("customSectionA", "Custom Section A", "icon-trophy", -3)]
    public class CustomSectionA : IApplication
    {
    }

    [Application("customSectionB", "Custom Section B", "icon-wall-plug", -2)]
    public class CustomSectionB : IApplication
    {
    }

    [Application("customSectionC", "Custom Section C", "icon-umb-settings", -1)]
    public class CustomSectionC : IApplication
    {
    }
}
```