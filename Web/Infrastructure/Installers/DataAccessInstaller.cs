namespace Web.Infrastructure.Installers
{
    using ByndyuSoft.Infrastructure.Dapper;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Domain.DataAccess.Sources.Dapper;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public class DataAccessInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                
                // Dapper
                Component.For<IConnectionFactory>().ImplementedBy<DapperConnectionFactory>()
                );
        }
    }
}