using System;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x02000233 RID: 563
	public enum ReportExecutionStatusType : byte
	{
		// Token: 0x04000B79 RID: 2937
		None,
		// Token: 0x04000B7A RID: 2938
		Queued,
		// Token: 0x04000B7B RID: 2939
		Scheduled,
		// Token: 0x04000B7C RID: 2940
		Running,
		// Token: 0x04000B7D RID: 2941
		Pausing,
		// Token: 0x04000B7E RID: 2942
		Paused,
		// Token: 0x04000B7F RID: 2943
		Resuming,
		// Token: 0x04000B80 RID: 2944
		Completed,
		// Token: 0x04000B81 RID: 2945
		Failed,
		// Token: 0x04000B82 RID: 2946
		Cancelling,
		// Token: 0x04000B83 RID: 2947
		Cancelled
	}
}
