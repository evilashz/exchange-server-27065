using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003C5 RID: 965
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class ADComplianceProgramCollection : ADConfigurationObject
	{
		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06002C25 RID: 11301 RVA: 0x000B6267 File Offset: 0x000B4467
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADComplianceProgramCollection.schema;
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06002C26 RID: 11302 RVA: 0x000B626E File Offset: 0x000B446E
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADComplianceProgramCollection.mostDerivedClass;
			}
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x000B6275 File Offset: 0x000B4475
		internal ADComplianceProgram[] GetCompliancePrograms()
		{
			return base.Session.Find<ADComplianceProgram>(base.Id, QueryScope.OneLevel, null, new SortBy(ADObjectSchema.Name, SortOrder.Ascending), 0);
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06002C28 RID: 11304 RVA: 0x000B6296 File Offset: 0x000B4496
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x04001A72 RID: 6770
		private static ADComplianceProgramCollectionSchema schema = ObjectSchema.GetInstance<ADComplianceProgramCollectionSchema>();

		// Token: 0x04001A73 RID: 6771
		private static string mostDerivedClass = "msExchMailflowPolicyCollection";
	}
}
