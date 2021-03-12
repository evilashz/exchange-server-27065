using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Transport.Agent.ConnectionFiltering
{
	// Token: 0x02000008 RID: 8
	internal class ConnectionFilterConfig
	{
		// Token: 0x0600002A RID: 42 RVA: 0x000036BC File Offset: 0x000018BC
		public ConnectionFilterConfig(IConfigurationSession session)
		{
			ADPagedReader<IPAllowListProvider> adpagedReader = session.FindAllPaged<IPAllowListProvider>();
			this.AllowListProviders = adpagedReader.ReadAllPages();
			ADPagedReader<IPBlockListProvider> adpagedReader2 = session.FindAllPaged<IPBlockListProvider>();
			this.BlockListProviders = adpagedReader2.ReadAllPages();
			this.AllowListConfig = session.FindSingletonConfigurationObject<IPAllowListConfig>();
			this.BlockListConfig = session.FindSingletonConfigurationObject<IPBlockListConfig>();
			this.AllowListProviderConfig = session.FindSingletonConfigurationObject<IPAllowListProviderConfig>();
			this.BlockListProviderConfig = session.FindSingletonConfigurationObject<IPBlockListProviderConfig>();
		}

		// Token: 0x04000022 RID: 34
		public readonly IPAllowListProvider[] AllowListProviders;

		// Token: 0x04000023 RID: 35
		public readonly IPBlockListProvider[] BlockListProviders;

		// Token: 0x04000024 RID: 36
		public readonly IPAllowListConfig AllowListConfig;

		// Token: 0x04000025 RID: 37
		public readonly IPBlockListConfig BlockListConfig;

		// Token: 0x04000026 RID: 38
		public readonly IPAllowListProviderConfig AllowListProviderConfig;

		// Token: 0x04000027 RID: 39
		public readonly IPBlockListProviderConfig BlockListProviderConfig;
	}
}
