using System;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x02000027 RID: 39
	public enum AsyncQueueStatus : short
	{
		// Token: 0x040000A2 RID: 162
		None,
		// Token: 0x040000A3 RID: 163
		Pending = 5,
		// Token: 0x040000A4 RID: 164
		Paused = 10,
		// Token: 0x040000A5 RID: 165
		NotStarted = 30,
		// Token: 0x040000A6 RID: 166
		InProgress = 40,
		// Token: 0x040000A7 RID: 167
		ErrorRetry = 50,
		// Token: 0x040000A8 RID: 168
		ErrorRetryTransient = 60,
		// Token: 0x040000A9 RID: 169
		AutoPaused = 70,
		// Token: 0x040000AA RID: 170
		Continuous = 90,
		// Token: 0x040000AB RID: 171
		Completed = 100,
		// Token: 0x040000AC RID: 172
		CompletedWithError = 110,
		// Token: 0x040000AD RID: 173
		Failed = 120,
		// Token: 0x040000AE RID: 174
		Cancelled = 130,
		// Token: 0x040000AF RID: 175
		SystemCancelled = 140,
		// Token: 0x040000B0 RID: 176
		Skipped = 150
	}
}
