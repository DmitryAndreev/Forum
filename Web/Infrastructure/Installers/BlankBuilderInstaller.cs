namespace Web.Infrastructure.Installers
{
    using Application.Print.Services;
    using Castle.Facilities.TypedFactory;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using JetBrains.Annotations;
    using Xlsx2PdfConvertService;
    using XlsxService;
    using XlsxService.BlankBuilders;
    using XlsxService.BlankBuilders.Impl;
    using XlsxService.Blanks;
    using XlsxService.Blanks.Services;
    using XlsxService.Blanks.Services.Impl;

    [UsedImplicitly]
    public class BlankBuilderInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<TypedFactoryFacility>();

            var blanks = Classes.FromAssemblyContaining<IBlankModel>()
                                .BasedOn(typeof (IBlankBuilder<>))
                                .WithService.AllInterfaces()
                                .LifestyleTransient();

            container.Register(
                blanks,
                Component.For<IBlankBuilderFactory>().AsFactory().LifestyleTransient()
                );

            container.Register(
                Component.For<IBlankNumberCreator>().ImplementedBy<BlankNumberCreator>().LifestyleTransient(),
                Component.For<IConvertService>().ImplementedBy<ConvertService>().LifestyleSingleton(),
                Component.For<IFileService>().ImplementedBy<XlsxFileService>().LifestyleSingleton(),
                Component.For<IBankBlankBuilder>().ImplementedBy<BankBlankBuilder>().LifestyleTransient(),
                Component.For<IActPaymentBuilder>().ImplementedBy<ActPaymentBuilder>().LifestyleTransient(),
                Component.For<IActPaymentBuilderHelper>().ImplementedBy<ActPaymentBulderHelper>().LifestyleTransient(),
                Component.For<IHardCopyBlankBuilder>().ImplementedBy<HardCopyBlankBuilder>().LifestyleTransient(),
                Component.For<IBsoReportBuilder>().ImplementedBy<BsoReportBuilder>().LifestyleTransient(),
                Component.For<IIfnsReportBuilder>().ImplementedBy<IfnsReportBuilder>().LifestyleTransient(),
                Component.For<IBankReceiptBuilder>().ImplementedBy<BankReceiptBuilder>().LifestyleTransient()
                );
        }
    }
}