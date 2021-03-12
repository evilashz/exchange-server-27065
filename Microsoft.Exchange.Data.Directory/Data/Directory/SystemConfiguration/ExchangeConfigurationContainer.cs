using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000445 RID: 1093
	[ObjectScope(ConfigScopes.Global)]
	[Serializable]
	public class ExchangeConfigurationContainer : ADContainer
	{
		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x0600317A RID: 12666 RVA: 0x000C6E15 File Offset: 0x000C5015
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchConfigurationContainer";
			}
		}

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x0600317B RID: 12667 RVA: 0x000C6E1C File Offset: 0x000C501C
		internal override ADObjectSchema Schema
		{
			get
			{
				return ExchangeConfigurationContainer.schema;
			}
		}

		// Token: 0x0400213F RID: 8511
		private const string MostDerivedObjectClassInternal = "msExchConfigurationContainer";

		// Token: 0x04002140 RID: 8512
		private static ADContainerSchema schema = ObjectSchema.GetInstance<ADContainerSchema>();
	}
}
