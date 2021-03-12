using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x0200005E RID: 94
	internal enum UpdateGroupMailboxMetadata
	{
		// Token: 0x0400051E RID: 1310
		[DisplayName("UGM", "Guid")]
		ExchangeGuid,
		// Token: 0x0400051F RID: 1311
		[DisplayName("UGM", "ExtId")]
		ExternalDirectoryObjectId,
		// Token: 0x04000520 RID: 1312
		[DisplayName("UGM", "ADSCT")]
		ADSessionCreateTime,
		// Token: 0x04000521 RID: 1313
		[DisplayName("UGM", "AMC")]
		AddedMembersCount,
		// Token: 0x04000522 RID: 1314
		[DisplayName("UGM", "RMC")]
		RemovedMembersCount,
		// Token: 0x04000523 RID: 1315
		[DisplayName("UGM", "APMC")]
		AddedPendingMembersCount,
		// Token: 0x04000524 RID: 1316
		[DisplayName("UGM", "RPMC")]
		RemovedPendingMembersCount,
		// Token: 0x04000525 RID: 1317
		[DisplayName("UGM", "FC")]
		ForceConfigurationActionValue,
		// Token: 0x04000526 RID: 1318
		[DisplayName("UGM", "GADT")]
		GroupAdLookupTime,
		// Token: 0x04000527 RID: 1319
		[DisplayName("UGM", "EUT")]
		ExecutingUserLookupTime,
		// Token: 0x04000528 RID: 1320
		[DisplayName("UGM", "LT")]
		MailboxLogonTime,
		// Token: 0x04000529 RID: 1321
		[DisplayName("UGM", "RMT")]
		ResolveMembersTime,
		// Token: 0x0400052A RID: 1322
		[DisplayName("UGM", "MT")]
		SetMembershipTime,
		// Token: 0x0400052B RID: 1323
		[DisplayName("UGM", "ADUserCached")]
		IsPopulateADUserInCacheSuccessful,
		// Token: 0x0400052C RID: 1324
		[DisplayName("UGM", "MiniRecipCached")]
		IsPopulateMiniRecipientInCacheSuccessful,
		// Token: 0x0400052D RID: 1325
		[DisplayName("UGM", "GPUT")]
		GroupPhotoUploadTime,
		// Token: 0x0400052E RID: 1326
		[DisplayName("UGM", "ERPT")]
		ExchangeResourcePublishTime,
		// Token: 0x0400052F RID: 1327
		[DisplayName("UGM", "CFGExe")]
		IsConfigurationExecuted,
		// Token: 0x04000530 RID: 1328
		[DisplayName("UGM", "CFGWarn")]
		ConfigurationWarnings,
		// Token: 0x04000531 RID: 1329
		[DisplayName("UGM", "SRST")]
		SetRegionalSettingsTime,
		// Token: 0x04000532 RID: 1330
		[DisplayName("UGM", "DFT")]
		CreateDefaultFoldersTime,
		// Token: 0x04000533 RID: 1331
		[DisplayName("UGM", "DFTC")]
		CreateDefaultFoldersCount,
		// Token: 0x04000534 RID: 1332
		[DisplayName("UGM", "ACL")]
		SetFolderPermissionsTime,
		// Token: 0x04000535 RID: 1333
		[DisplayName("UGM", "ACLC")]
		SetFolderPermissionsCount,
		// Token: 0x04000536 RID: 1334
		[DisplayName("UGM", "CALT")]
		ConfigureCalendarTime,
		// Token: 0x04000537 RID: 1335
		[DisplayName("UGM", "WMT")]
		SendWelcomeMessageTime,
		// Token: 0x04000538 RID: 1336
		[DisplayName("UGM", "CFG")]
		AdditionalConfigurationDetails
	}
}
