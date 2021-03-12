using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005A5 RID: 1445
	[ObjectScope(ConfigScopes.Global)]
	[Serializable]
	public class SitesContainer : ADNonExchangeObject
	{
		// Token: 0x170015E6 RID: 5606
		// (get) Token: 0x060042FD RID: 17149 RVA: 0x000FC1D7 File Offset: 0x000FA3D7
		internal override ADObjectSchema Schema
		{
			get
			{
				return SitesContainer.schema;
			}
		}

		// Token: 0x170015E7 RID: 5607
		// (get) Token: 0x060042FE RID: 17150 RVA: 0x000FC1DE File Offset: 0x000FA3DE
		internal override string MostDerivedObjectClass
		{
			get
			{
				return SitesContainer.mostDerivedClass;
			}
		}

		// Token: 0x04002D81 RID: 11649
		internal static readonly string DefaultName = "Sites";

		// Token: 0x04002D82 RID: 11650
		private static SitesContainerSchema schema = ObjectSchema.GetInstance<SitesContainerSchema>();

		// Token: 0x04002D83 RID: 11651
		private static string mostDerivedClass = "sitesContainer";
	}
}
