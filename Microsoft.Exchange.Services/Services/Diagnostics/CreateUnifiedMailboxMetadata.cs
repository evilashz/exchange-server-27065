using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x0200002F RID: 47
	internal enum CreateUnifiedMailboxMetadata
	{
		// Token: 0x04000214 RID: 532
		[DisplayName("CRUM", "TT")]
		TotalTime,
		// Token: 0x04000215 RID: 533
		[DisplayName("CRUM", "VET")]
		VerifyEnvironmentTime,
		// Token: 0x04000216 RID: 534
		[DisplayName("CRUM", "VUIT")]
		VerifyUserIdentityTypeTime,
		// Token: 0x04000217 RID: 535
		[DisplayName("CRUM", "NMCT")]
		NewMailboxCmdletTime,
		// Token: 0x04000218 RID: 536
		[DisplayName("CRUM", "GAUT")]
		GlsAddUserTime,
		// Token: 0x04000219 RID: 537
		[DisplayName("CRUM", "GAUS")]
		GlsAddUserOperationStatus
	}
}
