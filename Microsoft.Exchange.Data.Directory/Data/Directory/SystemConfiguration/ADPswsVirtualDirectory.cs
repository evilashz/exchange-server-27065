using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200037F RID: 895
	[Serializable]
	public sealed class ADPswsVirtualDirectory : ADPowerShellCommonVirtualDirectory
	{
		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06002940 RID: 10560 RVA: 0x000AD84B File Offset: 0x000ABA4B
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADPswsVirtualDirectory.schema;
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06002941 RID: 10561 RVA: 0x000AD852 File Offset: 0x000ABA52
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x0400192F RID: 6447
		private static readonly ADPswsVirtualDirectorySchema schema = ObjectSchema.GetInstance<ADPswsVirtualDirectorySchema>();
	}
}
