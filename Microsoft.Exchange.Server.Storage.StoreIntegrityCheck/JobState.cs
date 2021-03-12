using System;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x0200001B RID: 27
	public enum JobState : short
	{
		// Token: 0x04000048 RID: 72
		[MapToManagement(null, false)]
		Initializing,
		// Token: 0x04000049 RID: 73
		[MapToManagement("Queued", false)]
		Pending,
		// Token: 0x0400004A RID: 74
		[MapToManagement(null, false)]
		Running,
		// Token: 0x0400004B RID: 75
		[MapToManagement("Succeeded", false)]
		Completed,
		// Token: 0x0400004C RID: 76
		[MapToManagement(null, false)]
		Failed
	}
}
