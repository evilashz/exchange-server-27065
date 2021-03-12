using System;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005A6 RID: 1446
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SlimTenantConfigImpl
	{
		// Token: 0x06004301 RID: 17153 RVA: 0x000FC210 File Offset: 0x000FA410
		public static T GetConfig<T>(string settingName)
		{
			T config;
			using (IConfigProvider configProvider = ConfigProvider.CreateADProvider(new SlimTenantConfigSchema(), null))
			{
				configProvider.Initialize();
				config = configProvider.GetConfig<T>(settingName);
			}
			return config;
		}
	}
}
