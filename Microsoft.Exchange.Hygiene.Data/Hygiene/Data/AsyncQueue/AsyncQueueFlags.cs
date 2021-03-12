using System;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x02000011 RID: 17
	[Flags]
	internal enum AsyncQueueFlags
	{
		// Token: 0x04000038 RID: 56
		None = 0,
		// Token: 0x04000039 RID: 57
		HARequest = 1,
		// Token: 0x0400003A RID: 58
		ContinueOnFailure = 2,
		// Token: 0x0400003B RID: 59
		Finalizer = 4,
		// Token: 0x0400003C RID: 60
		SkipIfAssemblyMissing = 8,
		// Token: 0x0400003D RID: 61
		Continuous = 16,
		// Token: 0x0400003E RID: 62
		ContinueOnDependantRequestSuccess = 32,
		// Token: 0x0400003F RID: 63
		MarkSkipOnMaxRetries = 64
	}
}
