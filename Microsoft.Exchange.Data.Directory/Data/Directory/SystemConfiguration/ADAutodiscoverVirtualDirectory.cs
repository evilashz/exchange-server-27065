using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000325 RID: 805
	[Serializable]
	public sealed class ADAutodiscoverVirtualDirectory : ADExchangeServiceVirtualDirectory
	{
		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06002564 RID: 9572 RVA: 0x0009EBF3 File Offset: 0x0009CDF3
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADAutodiscoverVirtualDirectory.schema;
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06002565 RID: 9573 RVA: 0x0009EBFA File Offset: 0x0009CDFA
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADAutodiscoverVirtualDirectory.MostDerivedClass;
			}
		}

		// Token: 0x040016F0 RID: 5872
		private static readonly ADAutodiscoverVirtualDirectorySchema schema = ObjectSchema.GetInstance<ADAutodiscoverVirtualDirectorySchema>();

		// Token: 0x040016F1 RID: 5873
		public static readonly string MostDerivedClass = "msExchAutodiscoverVirtualDirectory";
	}
}
