using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003BB RID: 955
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	internal sealed class ADClientAccessRuleCollection : ADConfigurationObject
	{
		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06002BDD RID: 11229 RVA: 0x000B534E File Offset: 0x000B354E
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADClientAccessRuleCollection.schema;
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06002BDE RID: 11230 RVA: 0x000B5355 File Offset: 0x000B3555
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADClientAccessRuleCollection.mostDerivedClass;
			}
		}

		// Token: 0x04001A51 RID: 6737
		public static readonly string ContainerName = "Client Access Rules";

		// Token: 0x04001A52 RID: 6738
		private static ADClientAccessRuleCollectionSchema schema = ObjectSchema.GetInstance<ADClientAccessRuleCollectionSchema>();

		// Token: 0x04001A53 RID: 6739
		private static string mostDerivedClass = "msExchContainer";
	}
}
