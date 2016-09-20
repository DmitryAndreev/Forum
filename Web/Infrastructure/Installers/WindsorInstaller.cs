namespace Web.Infrastructure.Installers
{
    using Application.Environment.IoC;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using JetBrains.Annotations;


    [UsedImplicitly]
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            IoC.Init(container);
        }
    }
}