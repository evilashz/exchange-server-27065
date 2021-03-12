using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Exchange.Hygiene.Data.DataProvider
{
	// Token: 0x020000B4 RID: 180
	internal abstract class BloomFilterProviderFactory
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x00013D2C File Offset: 0x00011F2C
		// (set) Token: 0x060005F0 RID: 1520 RVA: 0x00013D6C File Offset: 0x00011F6C
		internal static BloomFilterProviderFactory Default
		{
			get
			{
				if (BloomFilterProviderFactory.defaultInstance == null)
				{
					Assembly assembly = Assembly.Load("Microsoft.Exchange.Hygiene.CacheDataProvider");
					Type type = assembly.GetType("Microsoft.Exchange.Hygiene.Cache.DataProvider.AutoUpdatingBloomFilterDataProvider+Factory");
					BloomFilterProviderFactory.defaultInstance = (BloomFilterProviderFactory)Activator.CreateInstance(type);
				}
				return BloomFilterProviderFactory.defaultInstance;
			}
			set
			{
				BloomFilterProviderFactory.defaultInstance = value;
			}
		}

		// Token: 0x060005F1 RID: 1521
		internal abstract IBloomFilterDataProvider Create(IEnumerable<Type> supportedTypes, TimeSpan autoUpdateFrequency, bool tracerTokenCheckEnabled);

		// Token: 0x040003AC RID: 940
		private static BloomFilterProviderFactory defaultInstance;
	}
}
