using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000522 RID: 1314
	[Flags]
	public enum OrganizationProvisioningFlags
	{
		// Token: 0x040027A2 RID: 10146
		None = 0,
		// Token: 0x040027A3 RID: 10147
		EnableAsSharedConfiguration = 1,
		// Token: 0x040027A4 RID: 10148
		EnableLicensingEnforcement = 2,
		// Token: 0x040027A5 RID: 10149
		ExcludedFromBackSync = 4,
		// Token: 0x040027A6 RID: 10150
		EhfAdminAccountSyncEnabled = 8,
		// Token: 0x040027A7 RID: 10151
		AllowDeleteOfExternalIdentityUponRemove = 16,
		// Token: 0x040027A8 RID: 10152
		HostingDeploymentEnabled = 32,
		// Token: 0x040027A9 RID: 10153
		UseServicePlanAsCounterInstanceName = 64,
		// Token: 0x040027AA RID: 10154
		Dehydrated = 128,
		// Token: 0x040027AB RID: 10155
		Static = 256,
		// Token: 0x040027AC RID: 10156
		AppsForOfficeDisabled = 512,
		// Token: 0x040027AD RID: 10157
		ImmutableConfiguration = 1024,
		// Token: 0x040027AE RID: 10158
		ExcludedFromForwardSyncEDU2BPOS = 2048,
		// Token: 0x040027AF RID: 10159
		EDUEnabled = 4096,
		// Token: 0x040027B0 RID: 10160
		MSOEnabled = 8192,
		// Token: 0x040027B1 RID: 10161
		ForwardSyncEnabled = 16384,
		// Token: 0x040027B2 RID: 10162
		GuidPrefixedLegacyDn = 32768,
		// Token: 0x040027B3 RID: 10163
		MailboxForcedReplicationDisabled = 65536,
		// Token: 0x040027B4 RID: 10164
		SyncPropertySetUpgradeAllowed = 131072,
		// Token: 0x040027B5 RID: 10165
		ProcessEhaMigratedMessagesEnabled = 524288,
		// Token: 0x040027B6 RID: 10166
		PilotingOrganization = 1048576,
		// Token: 0x040027B7 RID: 10167
		IsTenantAccessBlocked = 4194304,
		// Token: 0x040027B8 RID: 10168
		IsUpgradeOperationInProgress = 8388608
	}
}
