using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x0200002A RID: 42
	internal enum AddAggregatedAccountMetadata
	{
		// Token: 0x040001C7 RID: 455
		[DisplayName("AAA", "TT")]
		TotalTime,
		// Token: 0x040001C8 RID: 456
		[DisplayName("AAA", "VET")]
		VerifyEnvironmentTime,
		// Token: 0x040001C9 RID: 457
		[DisplayName("AAA", "VUIT")]
		VerifyUserIdentityTypeTime,
		// Token: 0x040001CA RID: 458
		[DisplayName("AAA", "NSRG")]
		NewSyncRequestGuid,
		// Token: 0x040001CB RID: 459
		[DisplayName("AAA", "CSPR")]
		CheckShouldProceedWithRequest,
		// Token: 0x040001CC RID: 460
		[DisplayName("AAA", "AGAT")]
		AddAggregatedMailboxGuidToADUserTime,
		// Token: 0x040001CD RID: 461
		[DisplayName("AAA", "MCT")]
		MailboxCleanupTime,
		// Token: 0x040001CE RID: 462
		[DisplayName("AAA", "NSCT")]
		NewSyncRequestCmdletTime,
		// Token: 0x040001CF RID: 463
		[DisplayName("AAA", "TUCL")]
		TestUserCanLogonTime,
		// Token: 0x040001D0 RID: 464
		[DisplayName("AAA", "CVSE")]
		CacheValidatedSettings,
		// Token: 0x040001D1 RID: 465
		[DisplayName("AAA", "CNVS")]
		CacheNotValidatedSettings,
		// Token: 0x040001D2 RID: 466
		[DisplayName("AAA", "SMBE")]
		SetMailboxCmdletError,
		// Token: 0x040001D3 RID: 467
		[DisplayName("AAA", "NOAA")]
		NumberOfAggregatedAccounts
	}
}
