namespace Web.Infrastructure.BootstrapperTasks
{
    using Application.Administration.Profiles;
    using Application.ArticleCategories.Profiles;
    using Application.Articles.Profiles;
    using Application.Boilerplate.Profiles;
    using Application.Company.Profiles;
    using Application.Employee.Profiles;
    using Application.Notifications.Profiles;
    using Application.Plan.Profiles;
    using Application.Print.Profiles;
    using Application.Profile.Profiles;
    using AutoMapper;
    using Castle.MicroKernel;
    using JetBrains.Annotations;
    using MvcExtensions;

    [UsedImplicitly]
    public class RegisterProfiles : BootstrapperTask
    {
        public RegisterProfiles(IKernel kernel)
        {
        }

        public override TaskContinuation Execute()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<SelectListProfile>();
                cfg.AddProfile<UnitProfile>();
                cfg.AddProfile<CompanyProfile>();
                cfg.AddProfile<EmployeeProfile>();
                cfg.AddProfile<PlanProfile>();
                cfg.AddProfile<AccountProfile>();
                cfg.AddProfile<AccountingPeriodProfile>();
                cfg.AddProfile<ArticleProfile>();
                cfg.AddProfile<NotificationProfile>();
                cfg.AddProfile<CompanyExportProfile>();
                cfg.AddProfile<ArticleCategoryProfile>();
                cfg.AddProfile<ActOrderProfile>();
                cfg.AddProfile<BlankModelsProfile>();
                cfg.AddProfile<BlankTemplateProfile>();
            });

            Mapper.AssertConfigurationIsValid();

            return TaskContinuation.Continue;
        }
    }
}