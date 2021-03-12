using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000521 RID: 1313
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class OnPremisesOrganization : ADConfigurationObject
	{
		// Token: 0x06003A0B RID: 14859 RVA: 0x000E0571 File Offset: 0x000DE771
		internal override void InitializeSchema()
		{
		}

		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x06003A0C RID: 14860 RVA: 0x000E0573 File Offset: 0x000DE773
		internal override ADObjectSchema Schema
		{
			get
			{
				return OnPremisesOrganization.schema;
			}
		}

		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x06003A0D RID: 14861 RVA: 0x000E057A File Offset: 0x000DE77A
		internal override string MostDerivedObjectClass
		{
			get
			{
				return OnPremisesOrganization.mostDerivedClass;
			}
		}

		// Token: 0x1700123F RID: 4671
		// (get) Token: 0x06003A0E RID: 14862 RVA: 0x000E0581 File Offset: 0x000DE781
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x17001240 RID: 4672
		// (get) Token: 0x06003A0F RID: 14863 RVA: 0x000E0594 File Offset: 0x000DE794
		internal override ADObjectId ParentPath
		{
			get
			{
				return OnPremisesOrganization.rootId;
			}
		}

		// Token: 0x17001241 RID: 4673
		// (get) Token: 0x06003A10 RID: 14864 RVA: 0x000E059B File Offset: 0x000DE79B
		// (set) Token: 0x06003A11 RID: 14865 RVA: 0x000E05AD File Offset: 0x000DE7AD
		public Guid OrganizationGuid
		{
			get
			{
				return (Guid)this[OnPremisesOrganizationSchema.OrganizationGuid];
			}
			set
			{
				this[OnPremisesOrganizationSchema.OrganizationGuid] = value;
			}
		}

		// Token: 0x17001242 RID: 4674
		// (get) Token: 0x06003A12 RID: 14866 RVA: 0x000E05C0 File Offset: 0x000DE7C0
		// (set) Token: 0x06003A13 RID: 14867 RVA: 0x000E05D2 File Offset: 0x000DE7D2
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<SmtpDomain> HybridDomains
		{
			get
			{
				return (MultiValuedProperty<SmtpDomain>)this[OnPremisesOrganizationSchema.HybridDomains];
			}
			set
			{
				this[OnPremisesOrganizationSchema.HybridDomains] = value;
			}
		}

		// Token: 0x17001243 RID: 4675
		// (get) Token: 0x06003A14 RID: 14868 RVA: 0x000E05E0 File Offset: 0x000DE7E0
		// (set) Token: 0x06003A15 RID: 14869 RVA: 0x000E05F2 File Offset: 0x000DE7F2
		public ADObjectId InboundConnector
		{
			get
			{
				return (ADObjectId)this[OnPremisesOrganizationSchema.InboundConnectorLink];
			}
			set
			{
				this[OnPremisesOrganizationSchema.InboundConnectorLink] = value;
			}
		}

		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x06003A16 RID: 14870 RVA: 0x000E0600 File Offset: 0x000DE800
		// (set) Token: 0x06003A17 RID: 14871 RVA: 0x000E0612 File Offset: 0x000DE812
		public ADObjectId OutboundConnector
		{
			get
			{
				return (ADObjectId)this[OnPremisesOrganizationSchema.OutboundConnectorLink];
			}
			set
			{
				this[OnPremisesOrganizationSchema.OutboundConnectorLink] = value;
			}
		}

		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x06003A18 RID: 14872 RVA: 0x000E0620 File Offset: 0x000DE820
		// (set) Token: 0x06003A19 RID: 14873 RVA: 0x000E0632 File Offset: 0x000DE832
		public ADObjectId OrganizationRelationship
		{
			get
			{
				return (ADObjectId)this[OnPremisesOrganizationSchema.OrganizationRelationshipLink];
			}
			set
			{
				this[OnPremisesOrganizationSchema.OrganizationRelationshipLink] = value;
			}
		}

		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x06003A1A RID: 14874 RVA: 0x000E0640 File Offset: 0x000DE840
		// (set) Token: 0x06003A1B RID: 14875 RVA: 0x000E0652 File Offset: 0x000DE852
		public string OrganizationName
		{
			get
			{
				return (string)this[OnPremisesOrganizationSchema.OrganizationName];
			}
			set
			{
				this[OnPremisesOrganizationSchema.OrganizationName] = value;
			}
		}

		// Token: 0x0400279E RID: 10142
		private static OnPremisesOrganizationSchema schema = ObjectSchema.GetInstance<OnPremisesOrganizationSchema>();

		// Token: 0x0400279F RID: 10143
		private static string mostDerivedClass = "msExchOnPremisesOrganization";

		// Token: 0x040027A0 RID: 10144
		private static ADObjectId rootId = new ADObjectId("CN=On-Premises Organization");
	}
}
