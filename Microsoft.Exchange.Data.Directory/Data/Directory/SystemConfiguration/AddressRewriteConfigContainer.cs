using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200033E RID: 830
	[ObjectScope(ConfigScopes.Global)]
	[Serializable]
	public class AddressRewriteConfigContainer : ADContainer
	{
		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x060026C9 RID: 9929 RVA: 0x000A4B78 File Offset: 0x000A2D78
		internal override ADObjectSchema Schema
		{
			get
			{
				return AddressRewriteConfigContainer.schema;
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x060026CA RID: 9930 RVA: 0x000A4B7F File Offset: 0x000A2D7F
		internal override string MostDerivedObjectClass
		{
			get
			{
				return AddressRewriteConfigContainer.mostDerivedClass;
			}
		}

		// Token: 0x040017A8 RID: 6056
		private static AddressRewriteConfigContainerSchema schema = ObjectSchema.GetInstance<AddressRewriteConfigContainerSchema>();

		// Token: 0x040017A9 RID: 6057
		private static string mostDerivedClass = "msExchAddressRewriteConfiguration";
	}
}
