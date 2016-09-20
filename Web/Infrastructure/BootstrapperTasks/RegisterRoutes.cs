namespace Web.Infrastructure.BootstrapperTasks
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using JetBrains.Annotations;
    using MvcExtensions;

    [UsedImplicitly]
    public class RegisterRoutes : RegisterRoutesBase
    {
        public RegisterRoutes(RouteCollection routes)
            : base(routes)
        {
        }

        protected override void Register()
        {
            Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            Routes.LowercaseUrls = true;

            Routes.MapRoute(null, "print/form/{blankTemplateId}",
                            new { controller = "Print", action = "Form", blankTemplateId = UrlParameter.Optional });

            Routes.MapRoute("Default", "{controller}/{action}/{id}",
                            new {controller = "Home", action = "Index", id = UrlParameter.Optional});
        }
    }
}