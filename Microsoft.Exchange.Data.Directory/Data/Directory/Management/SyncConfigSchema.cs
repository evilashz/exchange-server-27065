using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000757 RID: 1879
	internal class SyncConfigSchema : ADPresentationSchema
	{
		// Token: 0x06005B40 RID: 23360 RVA: 0x0013F783 File Offset: 0x0013D983
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<SyncOrganizationSchema>();
		}

		// Token: 0x04003E1D RID: 15901
		public static readonly ADPropertyDefinition FederatedTenant = SyncOrganizationSchema.FederatedTenant;

		// Token: 0x04003E1E RID: 15902
		public static readonly ADPropertyDefinition DisableWindowsLiveID = SyncOrganizationSchema.DisableWindowsLiveID;

		// Token: 0x04003E1F RID: 15903
		public static readonly ADPropertyDefinition FederatedIdentitySourceADAttribute = SyncOrganizationSchema.FederatedIdentitySourceADAttribute;

		// Token: 0x04003E20 RID: 15904
		public static readonly ADPropertyDefinition WlidUseSMTPPrimary = SyncOrganizationSchema.WlidUseSMTPPrimary;

		// Token: 0x04003E21 RID: 15905
		public static readonly ADPropertyDefinition PasswordFilePath = SyncOrganizationSchema.PasswordFilePath;

		// Token: 0x04003E22 RID: 15906
		public static readonly ADPropertyDefinition ResetPasswordOnNextLogon = SyncOrganizationSchema.ResetPasswordOnNextLogon;

		// Token: 0x04003E23 RID: 15907
		public static readonly ADPropertyDefinition ProvisioningDomain = SyncOrganizationSchema.ProvisioningDomain;

		// Token: 0x04003E24 RID: 15908
		public static readonly ADPropertyDefinition EnterpriseExchangeVersion = SyncOrganizationSchema.EnterpriseExchangeVersion;
	}
}
