using System;

namespace Microsoft.Exchange.Data.Metering.Throttling
{
	// Token: 0x0200002D RID: 45
	internal enum MeteredCount
	{
		// Token: 0x040000E9 RID: 233
		AllQueue,
		// Token: 0x040000EA RID: 234
		AcceptedSubmissionQueue,
		// Token: 0x040000EB RID: 235
		AcceptedTotalQueue,
		// Token: 0x040000EC RID: 236
		CurrentRejectedSubmissionQueue,
		// Token: 0x040000ED RID: 237
		CurrentRejectedTotalQueue,
		// Token: 0x040000EE RID: 238
		RejectedSubmissionQueue,
		// Token: 0x040000EF RID: 239
		RejectedTotalQueue,
		// Token: 0x040000F0 RID: 240
		TempRejected,
		// Token: 0x040000F1 RID: 241
		PermanentRejected,
		// Token: 0x040000F2 RID: 242
		Accepted,
		// Token: 0x040000F3 RID: 243
		Deferred,
		// Token: 0x040000F4 RID: 244
		Deprioritized,
		// Token: 0x040000F5 RID: 245
		OutstandingJobs,
		// Token: 0x040000F6 RID: 246
		Memory,
		// Token: 0x040000F7 RID: 247
		ProcessingTicks
	}
}
