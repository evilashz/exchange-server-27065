using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000463 RID: 1123
	[ObjectScope(ConfigScopes.Server)]
	[Serializable]
	internal class ExchangeTransportConfigContainer : Container
	{
		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x0600323F RID: 12863 RVA: 0x000CB7B6 File Offset: 0x000C99B6
		internal override ADObjectSchema Schema
		{
			get
			{
				return ExchangeTransportConfigContainer.schema;
			}
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06003240 RID: 12864 RVA: 0x000CB7BD File Offset: 0x000C99BD
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ExchangeTransportConfigContainer.mostDerivedClass;
			}
		}

		// Token: 0x04002270 RID: 8816
		private static ExchangeTransportConfigContainerSchema schema = ObjectSchema.GetInstance<ExchangeTransportConfigContainerSchema>();

		// Token: 0x04002271 RID: 8817
		private static string mostDerivedClass = "msExchExchangeTransportCfgContainer";
	}
}
