using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Autodiscover.ConfigurationSettings
{
	// Token: 0x02000037 RID: 55
	internal class MapiHttpSettingsProvider
	{
		// Token: 0x06000185 RID: 389 RVA: 0x00008457 File Offset: 0x00006657
		public MapiHttpSettingsProvider(MapiHttpSettingsProvider.DiscoverServiceStrategy discoveryStrategy)
		{
			this.discoveryStrategy = discoveryStrategy;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00008468 File Offset: 0x00006668
		public void DiscoverSettings(HashSet<UserConfigurationSettingName> requestedSettings, string mailboxId, UserConfigurationSettings settings)
		{
			if (string.IsNullOrEmpty(mailboxId))
			{
				throw new ArgumentException("mailboxId is empty or null.", "mailboxId");
			}
			if (requestedSettings.Contains(UserConfigurationSettingName.MapiHttpUrls))
			{
				MapiHttpService mapiHttpService = this.discoveryStrategy(ClientAccessType.Internal);
				MapiHttpService mapiHttpService2 = this.discoveryStrategy(ClientAccessType.External);
				DateTime lastConfigurationTime = DateTime.MinValue;
				if ((mapiHttpService ?? mapiHttpService2) != null)
				{
					lastConfigurationTime = (mapiHttpService ?? mapiHttpService2).LastConfigurationTime;
				}
				MapiHttpProtocolUrls value = new MapiHttpProtocolUrls(this.GetUrlFromService(mapiHttpService), this.GetUrlFromService(mapiHttpService2), mailboxId, lastConfigurationTime);
				settings.Add(UserConfigurationSettingName.MapiHttpUrls, value);
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000084EA File Offset: 0x000066EA
		private Uri GetUrlFromService(MapiHttpService service)
		{
			if (service == null)
			{
				return null;
			}
			return service.Url;
		}

		// Token: 0x04000194 RID: 404
		private readonly MapiHttpSettingsProvider.DiscoverServiceStrategy discoveryStrategy;

		// Token: 0x02000038 RID: 56
		// (Invoke) Token: 0x06000189 RID: 393
		public delegate MapiHttpService DiscoverServiceStrategy(ClientAccessType clientAccessType);
	}
}
