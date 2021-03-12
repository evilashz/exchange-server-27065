using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x0200003E RID: 62
	internal enum GetAggregatedAccountMetadata
	{
		// Token: 0x040002C6 RID: 710
		[DisplayName("GAA", "TT")]
		TotalTime,
		// Token: 0x040002C7 RID: 711
		[DisplayName("GAA", "VET")]
		VerifyEnvironmentTime,
		// Token: 0x040002C8 RID: 712
		[DisplayName("GAA", "VUIT")]
		VerifyUserIdentityTypeTime,
		// Token: 0x040002C9 RID: 713
		[DisplayName("GAA", "GSCT")]
		GetSyncRequestCmdletTime,
		// Token: 0x040002CA RID: 714
		[DisplayName("GAA", "GSST")]
		GetSyncRequestStatisticsCmdletTime,
		// Token: 0x040002CB RID: 715
		[DisplayName("GAA", "GSSE")]
		GetSyncRequestStatisticsCmdletError,
		// Token: 0x040002CC RID: 716
		[DisplayName("GAA", "GSNE")]
		GetSyncRequestStatisticsCmdletNonAggregatedAccountError
	}
}
