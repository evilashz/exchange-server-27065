using System;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x0200000C RID: 12
	internal enum MigrationServicePropTag : uint
	{
		// Token: 0x0400000C RID: 12
		InArgMethodCode = 2684420099U,
		// Token: 0x0400000D RID: 13
		InArgUserLegacyDN = 2684485663U,
		// Token: 0x0400000E RID: 14
		InArgSubscriptionMessageId = 2684551426U,
		// Token: 0x0400000F RID: 15
		InArgAggregationSubscriptionType = 2684616707U,
		// Token: 0x04000010 RID: 16
		InArgCreateSyncSubscriptionName = 2685403167U,
		// Token: 0x04000011 RID: 17
		InArgCreateSyncSubscriptionImapServer = 2685468703U,
		// Token: 0x04000012 RID: 18
		InArgCreateSyncSubscriptionImapPort = 2685534211U,
		// Token: 0x04000013 RID: 19
		InArgCreateSyncSubscriptionEmailaddress = 2685599775U,
		// Token: 0x04000014 RID: 20
		InArgCreateSyncSubscriptionLogonName = 2685665311U,
		// Token: 0x04000015 RID: 21
		InArgCreateSyncSubscriptionSecurePassword = 2685730847U,
		// Token: 0x04000016 RID: 22
		InArgCreateSyncSubscriptionSecurityMechanism = 2685796355U,
		// Token: 0x04000017 RID: 23
		InArgCreateSyncSubscriptionAuthenticationMechanism = 2685861891U,
		// Token: 0x04000018 RID: 24
		InArgCreateSyncSubscriptionDisplayName = 2685927455U,
		// Token: 0x04000019 RID: 25
		InArgCreateSyncSubscriptionEncryptedPassword = 2685992991U,
		// Token: 0x0400001A RID: 26
		InArgCreateSyncSubscriptionFoldersToExclude = 2686058527U,
		// Token: 0x0400001B RID: 27
		InArgCreateSyncSubscriptionUserRootFolder = 2686124063U,
		// Token: 0x0400001C RID: 28
		InArgCreateSyncSubscriptionNetId = 2686189599U,
		// Token: 0x0400001D RID: 29
		InArgCreateSyncSubscriptionForceNew = 2686255115U,
		// Token: 0x0400001E RID: 30
		InArgUpdateSyncSubscriptionAction = 2162691U,
		// Token: 0x0400001F RID: 31
		InArgSubscriptionGuid = 2228296U,
		// Token: 0x04000020 RID: 32
		InArgMdbGuid = 2688614472U,
		// Token: 0x04000021 RID: 33
		InArgMigrationMailboxId = 2688679967U,
		// Token: 0x04000022 RID: 34
		InArgTenantAdmin = 2688745503U,
		// Token: 0x04000023 RID: 35
		InArgOrganizationId = 2688811266U,
		// Token: 0x04000024 RID: 36
		InArgRefresh = 2688876555U,
		// Token: 0x04000025 RID: 37
		InArgOrganizationName = 2688876802U,
		// Token: 0x04000026 RID: 38
		InArgMigrationMailboxUserLegacyDN = 2684420127U,
		// Token: 0x04000027 RID: 39
		InArgIsInitialSyncComplete = 2684485643U,
		// Token: 0x04000028 RID: 40
		InArgUpdateMigrationRequestAction = 2684551171U,
		// Token: 0x04000029 RID: 41
		InArgSubscriptionStatus = 2684616707U,
		// Token: 0x0400002A RID: 42
		InArgSubscriptionDetailedStatus = 2684682243U,
		// Token: 0x0400002B RID: 43
		InArgLastSuccessfulSyncTime = 2684747796U,
		// Token: 0x0400002C RID: 44
		InArgLastSyncTime = 2684813332U,
		// Token: 0x0400002D RID: 45
		InArgMigrationMailboxOrgId = 2684813384U,
		// Token: 0x0400002E RID: 46
		InArgMigrationDetailedStatus = 2684878851U,
		// Token: 0x0400002F RID: 47
		InArgUserExchangeMailboxSmtpAddress = 2684944415U,
		// Token: 0x04000030 RID: 48
		InArgItemsSynced = 2685009940U,
		// Token: 0x04000031 RID: 49
		InArgItemsSkipped = 2685075476U,
		// Token: 0x04000032 RID: 50
		InArgLastSyncNowRequestTime = 2685141012U,
		// Token: 0x04000033 RID: 51
		InArgUserExchangeMailboxLegDN = 2685206559U,
		// Token: 0x04000034 RID: 52
		OutArgErrorCode = 2936012803U,
		// Token: 0x04000035 RID: 53
		OutArgSubscriptionMessageId = 2936078594U,
		// Token: 0x04000036 RID: 54
		OutArgSubscriptionStatus = 2936143875U,
		// Token: 0x04000037 RID: 55
		OutArgDetailedSubscriptionStatus = 2936209411U,
		// Token: 0x04000038 RID: 56
		OutArgMigrationRequestUpdateResponse = 2936274947U,
		// Token: 0x04000039 RID: 57
		OutArgErrorDetails = 2936340511U,
		// Token: 0x0400003A RID: 58
		OutArgIsInitialSyncComplete = 2936406027U,
		// Token: 0x0400003B RID: 59
		OutArgLastSuccessfulSyncTime = 2936471572U,
		// Token: 0x0400003C RID: 60
		OutArgLastSyncTime = 2936537108U,
		// Token: 0x0400003D RID: 61
		OutArgItemsSynced = 2936602644U,
		// Token: 0x0400003E RID: 62
		OutArgItemsSkipped = 2936668180U,
		// Token: 0x0400003F RID: 63
		OutArgLastSyncNowRequestTime = 2936733716U,
		// Token: 0x04000040 RID: 64
		OutArgSubscriptionGuid = 2936799304U
	}
}
