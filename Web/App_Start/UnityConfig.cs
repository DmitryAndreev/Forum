namespace Forum.Web
{
	using System;
	using System.Data.Entity;
	using System.Linq;
	using AutoMapper;
	using BusinessLogic;
	using DataAccess;
	using JetBrains.Annotations;
	using Microsoft.Practices.Unity;
	using Profiles;

	/// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
	[UsedImplicitly]
	public class UnityConfig
    {
        #region Unity Container
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
			container.RegisterType<DbContext, ForumDbContext>(new HierarchicalLifetimeManager());
			container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());

			container.RegisterType<IMessageService, MessageService>(new HierarchicalLifetimeManager());
			container.RegisterType<IExtensionMethodsWrapper, EntityExtensionMethodsWrapper>(new HierarchicalLifetimeManager());

			var profiles =
			from t in typeof(MessageProfile).Assembly.GetTypes()
			where typeof(Profile).IsAssignableFrom(t)
			select (Profile)Activator.CreateInstance(t);
			
			var config = new MapperConfiguration(cfg =>
			{
				foreach (var profile in profiles)
				{
					cfg.AddProfile(profile);
				}
			});

			container.RegisterInstance(config);
			container.RegisterType<IMapper>(new InjectionFactory(c => c.Resolve<MapperConfiguration>().CreateMapper()));
		}
    }
}
