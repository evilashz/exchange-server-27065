using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002DC RID: 732
	internal enum PipelineSubmitStatus
	{
		// Token: 0x04000D42 RID: 3394
		Ok,
		// Token: 0x04000D43 RID: 3395
		PipelineFull,
		// Token: 0x04000D44 RID: 3396
		RecipientThrottled,
		// Token: 0x04000D45 RID: 3397
		TenantThrottled
	}
}
