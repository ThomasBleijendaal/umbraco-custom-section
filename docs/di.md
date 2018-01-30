## Dependency Injection

Getting dependency injection working in an Umbraco custom sections
is easy. Umbraco does not force you to use a certain type of DI, so
you can choose anything you want.

We will be using Autofac, and use it to inject a database context
build on Entity Framework Core into our Api- and TreeControllers. 
Since we replace the default dependency resolvers, we need to keep in
mind that we register all regular and api controllers used by Umbraco.

But first, to get everything working, install these NuGet packages:

- Autofac
- Autofac.Mvc5
- Autofac.WebApi2

And for Entity Framework Core:

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.InMemory
- Microsoft.EntityFrameworkCore.Relational

We will start by updating the `CustomApplication` class, and add register
some objects to the `ContainerBuilder`:

``` Csharp
public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
{
	var builder = new ContainerBuilder();
	
	//Register all controllers in Tree name space
	builder.RegisterApiControllers(typeof(CustomTreeController).Assembly);

	//register umbraco MVC + WebApi controllers used by the admin site
	builder.RegisterControllers(typeof(UmbracoApplication).Assembly);
	builder.RegisterApiControllers(typeof(UmbracoApplication).Assembly);

	builder.Register(context =>
	{
		var options = new DbContextOptionsBuilder<CustomSectionDbContext>();
		options.UseInMemoryDatabase(databaseName: "CustomSection");

		var ctx = new CustomSectionDbContext(options.Options);

		CustomSectionDbInitializer.Initialize(ctx);

		return ctx;
	}).InstancePerRequest();

	var container = builder.Build();

	//Set the MVC DependencyResolver
	DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

	//Set the WebApi DependencyResolver
	GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
}
```

To the `ContainerBuilder` we register all the the `Controllers` we need, the
`CustomTreeController` (and any other new controller added to the custom section) and
register all the controllers included in Umbraco. 

After that we setup the `CustomSectionDbContext`, which uses an `InMemoryDatabase`,
which is for an example project like this ideal. No migrations, a single initializer to
seed the data and no complex setup. In [the repository](https://github.com/ThomasBleijendaal/umbraco-custom-section/tree/master/CustomSection) 
you can find all the classes involved with the database setup, I have omitted these 
here to avoid endless code listings.

Now that we have a `DbContext` we can inject everywhere (although in a real custom section,
you probably want to wrap the context in some services). First thing we are update is the 
`CustomTreeController`, since the menu is still hard-coded:


