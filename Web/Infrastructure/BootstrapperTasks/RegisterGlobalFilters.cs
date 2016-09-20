namespace Web.Infrastructure.BootstrapperTasks
{
    using System.Web.Mvc;
    using Application.Boilerplate.Filters;
    using JetBrains.Annotations;
    using MvcExtensions;

    [UsedImplicitly]
    public class RegisterGlobalFilters : BootstrapperTask
    {
        public override TaskContinuation Execute()
        {
            var filters = GlobalFilters.Filters;

            filters.Add(new CustomHandleErrorAttribute());
            filters.Add(new ContextAccountAttribute());
            filters.Add(new ContextEmployeeAttribute());
            filters.Add(new VersionAttribute());

            return TaskContinuation.Continue;
        }
    }
}