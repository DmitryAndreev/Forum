namespace Web.Infrastructure.Installers
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using JetBrains.Annotations;
    using log4net;
    using log4net.Config;

    [UsedImplicitly]
    public class EnvironmentInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                // Logging and stats
                //Component.For<IActionsLogger>().ImplementedBy<ActionsLogger>().LifestyleTransient(),
                Component.For<ILog>().UsingFactoryMethod(InitializeLog4Net)
                         .LifestyleSingleton()
                );
        }

        private static ILog InitializeLog4Net()
        {
            XmlConfigurator.Configure();
            return LogManager.GetLogger("Logger");
        }
    }
}