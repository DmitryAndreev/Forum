namespace Web
{
    using System.Web.Http;
    using System.Web.Mvc;
    using Application.Boilerplate;
    using Application.Security.Utility;
    using Binders;
    using Infrastructure.BootstrapperTasks;
    using MvcExtensions;
    using MvcExtensions.Windsor;
    using StackExchange.Profiling;

    public class MvcApplication : WindsorMvcApplication
    {
        public MvcApplication()
        {
            Bootstrapper.BootstrapperTasks
                .Include<RunMigrations>()
                .Include<RegisterModelMetadata>()
                .Include<RegisterControllers>()
                .Include<RegisterRoutes>()
                .Include<RegisterGlobalFilters>()
                .Include<RegisterProfiles>()
                .Include<RegisterModelBinders>();


            new AuthenticationModule().Init(this);

            StringMetadataItemBuilder.EmailErrorMessage = "Неправильный формат адреса электронной почты";
            StringMetadataItemBuilder.EmailExpression = FormatConstans.EmailFormat;
        }


        protected override void OnStart()
        {
            GlobalConfiguration.Configuration.EnsureInitialized();
            AreaRegistration.RegisterAllAreas();
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
        }

        protected override void OnEnd()
        {
        }
# if DEBUG
        protected void Application_BeginRequest()
        {
            if (!Request.Path.StartsWith("/signalr"))
            {
                MiniProfiler.Start();
            }
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }
#endif
    }
}
