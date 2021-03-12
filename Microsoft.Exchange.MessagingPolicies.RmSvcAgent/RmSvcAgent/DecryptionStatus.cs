using System;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x02000023 RID: 35
	internal enum DecryptionStatus
	{
		// Token: 0x04000114 RID: 276
		StartAsync,
		// Token: 0x04000115 RID: 277
		Success,
		// Token: 0x04000116 RID: 278
		PermanentFailure,
		// Token: 0x04000117 RID: 279
		TransientFailure,
		// Token: 0x04000118 RID: 280
		ConfigurationLoadFailure
	}
}
