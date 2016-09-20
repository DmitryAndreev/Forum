namespace Web.Infrastructure.Installers
{
    using Application.Administration.Services;
    using Application.Administration.Services.Impl;
    using Application.Blank.Services;
    using Application.Blank.Services.Impl;
    using Application.Boilerplate.Services;
    using Application.Boilerplate.Services.Impl;
    using Application.Company.Services;
    using Application.Context.Services;
    using Application.Context.Services.Impl;
    using Application.Payment.Services;
    using Application.Payment.Services.Impl;
    using Application.Print;
    using Application.Print.Services;
    using Application.Print.Services.Impl;
    using Application.Security.Services;
    using Application.Security.Services.Impl;
    using Byndyusoft.Mailer.Core;
    using Byndyusoft.Mailer.Core.Impl;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Domain.Model.Services;
    using Domain.Model.Services.Impl;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                // Authentication and authorization and context
                Component.For<IAuthenticationService>().ImplementedBy<AuthenticationService>().LifestyleTransient(),
                Component.For<IContextAccountProvider>().ImplementedBy<ContextAccountProvider>().LifestylePerWebRequest(),
                Component.For<IContextEmployeeProvider>().ImplementedBy<ContextEmployeeProvider>().LifestylePerWebRequest(),
                Component.For<IEmployeeEntrySingleton>().ImplementedBy<EmployeeEntrySingleton>().LifestyleSingleton(),

                //mail
                Component.For<IMailerService>().ImplementedBy<MailerSenderService>().LifestyleSingleton(),

                //administration
                Component.For<IContactExportService>().ImplementedBy<ContactExportService>().LifestyleSingleton(),

                //web
                Component.For<ITokenUrlService>().ImplementedBy<TokenUrlService>().LifestyleTransient(),
                Component.For<IBlankCalculator>().ImplementedBy<BlankCalculator>().LifestyleTransient(),
                Component.For<IRobokassa>().ImplementedBy<Robokassa>().LifestyleTransient(),
                Component.For<IPaymentCreator>().ImplementedBy<PaymentCreator>().LifestyleTransient(),
                Component.For<IBalanceUpdater>().ImplementedBy<BalanceUpdater>().LifestyleTransient(),
                Component.For<IBlankPrintFormResolver>().ImplementedBy<BlankPrintFormResolver>().LifestyleSingleton(),
                Component.For<IOrderBuilder>().ImplementedBy<OrderBuilder>().LifestyleTransient(),
                Component.For<IUploadSerializerFactory>().ImplementedBy<UploadSerializerFactory>().LifestyleSingleton(),
                Component.For<ICompanyInfoUpdateService>().ImplementedBy<CompanyInfoUpdateService>().LifestylePerWebRequest(),
                Component.For<ICompanyInfoChecker>().ImplementedBy<CompanyInfoChecker>().LifestylePerWebRequest()
                );
        }
    }
}