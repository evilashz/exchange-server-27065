using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002C6 RID: 710
	[Flags]
	public enum OrganizationFlags2
	{
		// Token: 0x0400139F RID: 5023
		None = 0,
		// Token: 0x040013A0 RID: 5024
		TenantRelocationsAllowed = 1,
		// Token: 0x040013A1 RID: 5025
		IsUpgradeOperationInProgress = 2,
		// Token: 0x040013A2 RID: 5026
		OfflineOrgIdEnabled = 8,
		// Token: 0x040013A3 RID: 5027
		OfflineOrgIdAsPrimaryAuth = 24,
		// Token: 0x040013A4 RID: 5028
		PublicComputersDetectionEnabled = 32,
		// Token: 0x040013A5 RID: 5029
		OpenTenantFull = 64,
		// Token: 0x040013A6 RID: 5030
		RmsoSubscriptionStatus = 896,
		// Token: 0x040013A7 RID: 5031
		MapiHttpEnabled = 1024,
		// Token: 0x040013A8 RID: 5032
		TemplateTenant = 2048,
		// Token: 0x040013A9 RID: 5033
		IntuneManagedStatus = 4096,
		// Token: 0x040013AA RID: 5034
		HybridConfigurationStatus = 122880,
		// Token: 0x040013AB RID: 5035
		OAuth2ClientProfileEnabled = 262144,
		// Token: 0x040013AC RID: 5036
		ACLableSyncedObjectEnabled = 2097152
	}
}
