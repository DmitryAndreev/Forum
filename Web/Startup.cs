using Forum.WebApi;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Forum.WebApi
{
	using System.Data.Entity.Migrations;
	using System.Threading;
	using System.Web.Http;
	using DataAccess.Migrations;
	using JetBrains.Annotations;
	using Microsoft.Owin.BuilderProperties;
	using Microsoft.Owin.Cors;
	using Owin;

	public class Startup
	{
		[UsedImplicitly]
		public void Configuration(IAppBuilder app)
		{
			var configuration = new HttpConfiguration();
			var properties = new AppProperties(app.Properties);
			CancellationToken token = properties.OnAppDisposing;

			if (token != CancellationToken.None)
			{
				token.Register(UnityWebApiActivator.Shutdown);
			}

			app.UseCors(CorsOptions.AllowAll);

			WebApiConfig.Register(configuration);
			UnityWebApiActivator.Start(configuration);
			app.UseWebApi(configuration);

			UpdateDatabase();
		}

		private static void UpdateDatabase()
		{
			var persistenceConfig = new Configuration();
			var migrator = new DbMigrator(persistenceConfig);
			migrator.Update();
		}
	}
}