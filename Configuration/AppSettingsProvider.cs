// Tornando appsettings acessível

using Microsoft.Extensions.Configuration;

namespace Monsterflix.Api.Configurations
{
	public static class AppSettingsProvider
	{
		public static IConfiguration Settings { get; set; }
	}
}