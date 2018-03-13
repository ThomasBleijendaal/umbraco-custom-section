## Umbraco Custom Section

This repository is a combination of various other sites and forum posts 
about creating a custom Umbraco section. The aim is to have working example
code accompanied by some extra documentation about a custom section. This
custom section will contain the following features:

- Custom tree and pages
- AzureAD / ADFS
- Two factor authentication

It has been developed for and tested on Umbraco 7.7 - 7.9.

### 1. How to install

The root of this repository is the App_Plugins folder of a default install
of Umbraco. So to start, do the following:

1. Create empty ASP.NET (.NET Framework) project.
2. Add UmbracoCms nuget package.    
3. Clone this repository into the App_Plugins folder. Let ModelsBuilder be. Include all the files from this repository into your Umbraco-csproj.
4. Add the following nuget packages:
    - Microsoft.EntityFrameworkCore
    - Microsoft.EntityFrameworkCore.InMemory
    - Microsoft.EntityFrameworkCore.Relational
    - Autofac
    - Autofac.Mvc5
    - Autofac.WebApi2
    - TwoFactorAuthNet
    - Microsoft.Owin.Security.WsFederation (3.1.0)
5. Build.
6. Add the project as a new website in IIS.
6. Run the installer and setup Umbraco.
7. Update `web.config` for these features:
    - [Basic two factor authentication](tfa.md)
    - [Azure AD / ADFS](adfs.md)

### 2. Table of contents

1. [Basics](basics.md)
2. [Custom Tree](tree.md)
3. [Search](search.md)
4. [Dependency Injection](di.md)
5. [Custom pages](custom.md)
    - [Setting up dashboard](custom_dashboard.md)
    - [Setting up Angular in your section](custom_angular.md)
    - [Setting up a search result formatter](custom_searchformatter.md)
    - [Setting up custom notifications](custom_notifications.md)
    - [Setting up custom dialogs](custom_dialogs.md)
    - [Setting up backoffice controllers and pages](custom_controllers.md)
6. [Multiple sections](sections.md)
7. [Basic two factor authentication](tfa.md)
8. [Azure AD / ADFS](adfs.md)

## Sources used

### Umbraco icons
- [https://nicbell.github.io/ucreate/icons.html](https://nicbell.github.io/ucreate/icons.html)

### Custom section
- [http://skrift.io/articles/archive/sections-and-trees-in-umbraco-7/](http://skrift.io/articles/archive/sections-and-trees-in-umbraco-7/)
- [http://www.enkelmedia.se/blogg/2013/11/22/creating-custom-sections-in-umbraco-7-part-1.aspx](http://www.enkelmedia.se/blogg/2013/11/22/creating-custom-sections-in-umbraco-7-part-1.aspx)
- [https://github.com/kgiszewski/LearnUmbraco7/tree/master/Chapter%2016%20-%20Custom%20Sections%2C%20Trees%20and%20Actions](https://github.com/kgiszewski/LearnUmbraco7/tree/master/Chapter%2016%20-%20Custom%20Sections%2C%20Trees%20and%20Actions)

### 2FA login flow
- [https://github.com/Offroadcode/Umbraco-2FA](https://github.com/Offroadcode/Umbraco-2FA)

### ADFS
- [https://24days.in/umbraco-cms/2016/authenticating-with-ad-fs-and-identityextensions/](https://24days.in/umbraco-cms/2016/authenticating-with-ad-fs-and-identityextensions/)
- [https://our.umbraco.org/documentation/Reference/Security/](https://our.umbraco.org/documentation/Reference/Security/)
- [https://our.umbraco.org/forum/extending-umbraco-and-using-the-api/79201-account-auto-linking-not-working-with-owin-ad-fsws-federation-authentication](https://our.umbraco.org/forum/extending-umbraco-and-using-the-api/79201-account-auto-linking-not-working-with-owin-ad-fsws-federation-authentication)
- [https://github.com/umbraco/Umbraco-CMS/tree/release-7.7.6](https://github.com/umbraco/Umbraco-CMS/tree/release-7.7.6)
- [https://chris.59north.com/post/Manually-configuring-OWIN-WS-Federation-middleware-and-accepting-encrypted-tokens](https://chris.59north.com/post/Manually-configuring-OWIN-WS-Federation-middleware-and-accepting-encrypted-tokens)
- [https://chris.59north.com/post/Configuring-an-ASPNET-site-to-use-WS-Federation](https://chris.59north.com/post/Configuring-an-ASPNET-site-to-use-WS-Federation)