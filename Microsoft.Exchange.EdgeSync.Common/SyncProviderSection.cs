using System;
using System.Configuration;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000017 RID: 23
	internal class SyncProviderSection : ConfigurationSection
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00003300 File Offset: 0x00001500
		static SyncProviderSection()
		{
			SyncProviderSection.syncProviderElementProperty = new ConfigurationProperty("synchronizationProvider", typeof(SyncProviderElementCollection), null, ConfigurationPropertyOptions.IsRequired);
			SyncProviderSection.properties.Add(SyncProviderSection.syncProviderElementProperty);
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003336 File Offset: 0x00001536
		public SyncProviderElementCollection SyncProviderElements
		{
			get
			{
				return (SyncProviderElementCollection)base[SyncProviderSection.syncProviderElementProperty];
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003348 File Offset: 0x00001548
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SyncProviderSection.properties;
			}
		}

		// Token: 0x0400004A RID: 74
		private static ConfigurationProperty syncProviderElementProperty;

		// Token: 0x0400004B RID: 75
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
