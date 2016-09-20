using System.Web.Http;

namespace Forum.WebApi
{
	using Newtonsoft.Json;
	using Newtonsoft.Json.Serialization;
	using Infrastracture;

	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.Formatters.Remove(config.Formatters.XmlFormatter);
			var json = config.Formatters.JsonFormatter;
			json.SerializerSettings.Formatting = Formatting.Indented;
			json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

			config.Filters.Add(new ExceptionHandlingAttribute());
			config.MapHttpAttributeRoutes();
		}
	}
}