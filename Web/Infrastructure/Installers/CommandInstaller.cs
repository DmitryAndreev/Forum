namespace Web.Infrastructure.Installers
{
    using ByndyuSoft.Infrastructure.Domain.Commands;
    using Castle.Facilities.TypedFactory;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Domain.DataAccess.Sources.Dapper;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public class CommandInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var commands = Classes.FromAssemblyContaining<DapperConnectionFactory>()
                                  .BasedOn(typeof (ICommand<>))
                                  .WithService.AllInterfaces()
                                  .Configure(x => x.LifestyleTransient());

            container.Register(
                commands,
                Component.For<ICommandBuilder>().ImplementedBy(typeof (CommandBuilder)).LifestyleTransient(),
                Component.For<ICommandFactory>().AsFactory().LifestyleTransient()
                );
        }
    }
}