using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000483 RID: 1155
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class GALSyncOrganization : ADLegacyVersionableObject
	{
		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06003475 RID: 13429 RVA: 0x000D0CF6 File Offset: 0x000CEEF6
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ExchangeConfigurationUnit.MostDerivedClass;
			}
		}

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x06003476 RID: 13430 RVA: 0x000D0CFD File Offset: 0x000CEEFD
		internal override ADObjectSchema Schema
		{
			get
			{
				return GALSyncOrganization.schema;
			}
		}

		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06003477 RID: 13431 RVA: 0x000D0D04 File Offset: 0x000CEF04
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06003478 RID: 13432 RVA: 0x000D0D0C File Offset: 0x000CEF0C
		// (set) Token: 0x06003479 RID: 13433 RVA: 0x000D0D1E File Offset: 0x000CEF1E
		public string GALSyncClientData
		{
			get
			{
				return (string)this[GALSyncOrganizationSchema.GALSyncClientData];
			}
			internal set
			{
				this[GALSyncOrganizationSchema.GALSyncClientData] = value;
			}
		}

		// Token: 0x040023CA RID: 9162
		private static GALSyncOrganizationSchema schema = ObjectSchema.GetInstance<GALSyncOrganizationSchema>();
	}
}
