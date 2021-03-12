using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005DE RID: 1502
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class TransportRuleCollection : ADConfigurationObject
	{
		// Token: 0x170016F0 RID: 5872
		// (get) Token: 0x06004573 RID: 17779 RVA: 0x00102408 File Offset: 0x00100608
		internal override ADObjectSchema Schema
		{
			get
			{
				return TransportRuleCollection.schema;
			}
		}

		// Token: 0x170016F1 RID: 5873
		// (get) Token: 0x06004574 RID: 17780 RVA: 0x0010240F File Offset: 0x0010060F
		internal override string MostDerivedObjectClass
		{
			get
			{
				return TransportRuleCollection.mostDerivedClass;
			}
		}

		// Token: 0x06004575 RID: 17781 RVA: 0x00102416 File Offset: 0x00100616
		internal TransportRule[] GetRules()
		{
			return base.Session.Find<TransportRule>(base.Id, QueryScope.OneLevel, null, new SortBy(TransportRuleSchema.Priority, SortOrder.Ascending), 0);
		}

		// Token: 0x04002FAA RID: 12202
		private static TransportRuleCollectionSchema schema = ObjectSchema.GetInstance<TransportRuleCollectionSchema>();

		// Token: 0x04002FAB RID: 12203
		private static string mostDerivedClass = "msExchTransportRuleCollection";
	}
}
