# Fallen Nova
Fallen Nova is the name of a corp I joined in [EVE Online](http://www.eveonline.com/) several years back. Shortly after I put myself to work building a corp website that utilised the latest Microsoft technologies as well as the [EVE Online API](http://wiki.eveonline.com/en/wiki/EVE_API_Functions). 

This project is the latest [incarnation](http://community.eveonline.com/en/incarna/) of that website, this time utilising an number of the following frameworks, technologies, patterns:

* [ASP.NET MVC 4] (http://www.asp.net/mvc)
* [Entity Framework 6] (http://msdn.microsoft.com/en-gb/data/ef.aspx)
* [SQL 2008 R2] (http://technet.microsoft.com/en-us/sqlserver/ff398089.aspx)
* [Ninject] (http://www.ninject.org/)
* [Bootstrap] (http://twitter.github.com/bootstrap/)
* [nUnit] (http://www.nunit.org/)
* .NET 4.5 claims based security
* Service, Repository, Unit of Work patterns...

And the list goes on. 

This is still a work in progress, and does contain a number of TODOs. In addition, I'm also trying out new techniques with certain frameworks so do excuse any implementations of a given design pattern or framework that seems to break a given DDD or SOLID principle. 

Do feel free to [fork][fk], improve and then send me a [pull request][pr] if you like. Alternatively feel free to drop by [my blog](http://berniecook.wordpress.com) and post a comment.

##Setup
To get the solution up and running simply clone it then run the scripts in the "Solution Resources" folder. If you're having trouble getting the database working let me know and I'll send you an SQL backup.
If you want to re-run the T4 templates you'll need to install the "EF 5.x DbContext Fluent Generator for C#" via Visual Studio's Extensions and Updates tool, or grab the [Fluent Generator installer](http://visualstudiogallery.msdn.microsoft.com/5d663b99-ed3b-481d-b7bc-b947d2457e3c?SRC=VSIDE) from the official Visual Studio gallery page.

##Frameworks, Tools, Languages, etc.
Tools and frameworks currently employed: AutoMapper, ASP.NET MVC 4, Bootstrap, BCrypt.Net, C#, ELMAH, Entity Framework, Git Extensions, HTML 5, jQuery, glimpse, Modernizr, Ninject, nUnit, ReSharper, SQL 2008 R2 and Visual Studio 2012.

##Warning
As noted above this is still a work in progress. In particular the unit tests are still in need of some attention (I know it sounds like I'm doing Test Last Development) but certain features were initially implemented for me to tinker with so the unit tests came later.

Enjoy.

