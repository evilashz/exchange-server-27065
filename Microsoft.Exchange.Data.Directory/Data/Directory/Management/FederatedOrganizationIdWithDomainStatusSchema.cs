using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000711 RID: 1809
	internal sealed class FederatedOrganizationIdWithDomainStatusSchema : ADPresentationSchema
	{
		// Token: 0x06005512 RID: 21778 RVA: 0x001335A9 File Offset: 0x001317A9
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<FederatedOrganizationIdSchema>();
		}

		// Token: 0x04003912 RID: 14610
		public static readonly ADPropertyDefinition AccountNamespace = FederatedOrganizationIdSchema.AccountNamespace;

		// Token: 0x04003913 RID: 14611
		public static readonly ADPropertyDefinition Enabled = FederatedOrganizationIdSchema.Enabled;

		// Token: 0x04003914 RID: 14612
		public static readonly ADPropertyDefinition OrganizationContact = FederatedOrganizationIdSchema.OrganizationContact;

		// Token: 0x04003915 RID: 14613
		public static readonly ADPropertyDefinition DelegationTrustLink = FederatedOrganizationIdSchema.DelegationTrustLink;
	}
}
