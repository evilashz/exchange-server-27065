using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200055D RID: 1373
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class RecipientPoliciesContainer : ADConfigurationObject
	{
		// Token: 0x170013D0 RID: 5072
		// (get) Token: 0x06003DD8 RID: 15832 RVA: 0x000EB810 File Offset: 0x000E9A10
		internal override ADObjectSchema Schema
		{
			get
			{
				return RecipientPoliciesContainer.schema;
			}
		}

		// Token: 0x170013D1 RID: 5073
		// (get) Token: 0x06003DD9 RID: 15833 RVA: 0x000EB817 File Offset: 0x000E9A17
		internal override string MostDerivedObjectClass
		{
			get
			{
				return RecipientPoliciesContainer.mostDerivedClass;
			}
		}

		// Token: 0x040029E0 RID: 10720
		private static RecipientPoliciesContainerSchema schema = ObjectSchema.GetInstance<RecipientPoliciesContainerSchema>();

		// Token: 0x040029E1 RID: 10721
		private static string mostDerivedClass = "msExchRecipientPolicyContainer";
	}
}
