using System;
using System.Configuration;

namespace Microsoft.Exchange.SharedCache
{
	// Token: 0x0200000D RID: 13
	internal class CachesSection : ConfigurationSection
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003242 File Offset: 0x00001442
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00003254 File Offset: 0x00001454
		[ConfigurationProperty("Caches")]
		public CacheSettingsCollection CachesElement
		{
			get
			{
				return (CacheSettingsCollection)base["Caches"];
			}
			set
			{
				base["Caches"] = value;
			}
		}
	}
}
