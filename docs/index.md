## Umbraco Custom Section

This repository is a combination of various other sites and forum posts 
about creating a custom Umbraco section. The aim is to have working example
code accompanied by some extra documentation about a custom section. This
custom section will contain the following features:

- ADFS / AzureAD
- Two factor authentication
- Custom section

This is a work in progress.

### 1. How to install

The root of this repository is the App_Plugins folder of a default install
of Umbraco. So to start, do the following:

1. Create empty ASP.NET (.NET Framework) project.
2. Add UmbracoCms nuget package.
3. Clone this repository into the App_Plugins folder. Let ModelsBuilder be.
4. Run the installer and setup Umbraco.

### 2. Table of contents

1. [Basics](basics.md)
2. [Custom Tree](tree.md)
3. Search
4. Dependency Injection
5. Dashboard
6. Custom pages
7. Multiple sections
8. Very basic two factor authentication
9. ADFS / Azure AD / Azure AD B2C

## Sources used

### Custom section
http://skrift.io/articles/archive/sections-and-trees-in-umbraco-7/
https://nicbell.github.io/ucreate/icons.html
http://www.enkelmedia.se/blogg/2013/11/22/creating-custom-sections-in-umbraco-7-part-1.aspx
https://github.com/kgiszewski/LearnUmbraco7/tree/master/Chapter%2016%20-%20Custom%20Sections%2C%20Trees%20and%20Actions

### 2FA login flow
https://github.com/Offroadcode/Umbraco-2FA

### ADFS
https://24days.in/umbraco-cms/2016/authenticating-with-ad-fs-and-identityextensions/
https://our.umbraco.org/documentation/Reference/Security/
https://our.umbraco.org/forum/extending-umbraco-and-using-the-api/79201-account-auto-linking-not-working-with-owin-ad-fsws-federation-authentication
https://github.com/umbraco/Umbraco-CMS/tree/release-7.7.6
https://chris.59north.com/post/Manually-configuring-OWIN-WS-Federation-middleware-and-accepting-encrypted-tokens
https://chris.59north.com/post/Configuring-an-ASPNET-site-to-use-WS-Federation