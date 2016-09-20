namespace Web.Infrastructure.Installers
{
    using ByndyuSoft.Infrastructure.Domain;
    using Castle.Facilities.TypedFactory;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Domain.DataAccess.Sources.Dapper;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public class QueriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var queries = Classes.FromAssemblyContaining<DapperConnectionFactory>()
                                 .BasedOn(typeof (IQuery<,>))
                                 .WithService.AllInterfaces()
                                 .LifestyleTransient();

            container.Register(
                queries,
                Component.For<IQueryBuilder>().AsFactory().LifestyleTransient(),
                Component.For<IQueryFactory>().AsFactory().LifestyleTransient(),
                Component.For(typeof (IQueryFor<>)).ImplementedBy(typeof (QueryFor<>)).LifestyleTransient()
                );
        }
    }
}