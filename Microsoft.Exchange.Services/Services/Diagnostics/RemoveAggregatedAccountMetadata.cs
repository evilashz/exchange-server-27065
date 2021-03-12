using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x02000050 RID: 80
	internal enum RemoveAggregatedAccountMetadata
	{
		// Token: 0x040004AA RID: 1194
		[DisplayName("RAA", "TT")]
		TotalTime,
		// Token: 0x040004AB RID: 1195
		[DisplayName("RAA", "VET")]
		VerifyEnvironmentTime,
		// Token: 0x040004AC RID: 1196
		[DisplayName("RAA", "VUIT")]
		VerifyUserIdentityTypeTime,
		// Token: 0x040004AD RID: 1197
		[DisplayName("RAA", "RSCT")]
		RemoveSyncRequestCmdletTime,
		// Token: 0x040004AE RID: 1198
		[DisplayName("RAA", "RGAT")]
		RemoveAggregatedMailboxGuidFromADUserTime,
		// Token: 0x040004AF RID: 1199
		[DisplayName("RAA", "GMGT")]
		GetAggregatedMailboxGuidFromSyncRequestStatisticsTime,
		// Token: 0x040004B0 RID: 1200
		[DisplayName("RAA", "GSNE")]
		GetSyncRequestStatisticsCmdletNonAggregatedAccountError
	}
}
