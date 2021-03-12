using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001DB RID: 475
	internal interface ISystemProbeTrace
	{
		// Token: 0x06000D5E RID: 3422
		void TracePass(Guid activityId, long etlTraceId, string message);

		// Token: 0x06000D5F RID: 3423
		void TracePass<T0>(Guid activityId, long etlTraceId, string formatString, T0 arg0);

		// Token: 0x06000D60 RID: 3424
		void TracePass<T0, T1>(Guid activityId, long etlTraceId, string formatString, T0 arg0, T1 arg1);

		// Token: 0x06000D61 RID: 3425
		void TracePass<T0, T1, T2>(Guid activityId, long etlTraceId, string formatString, T0 arg0, T1 arg1, T2 arg2);

		// Token: 0x06000D62 RID: 3426
		void TracePass(Guid activityId, long etlTraceId, string formatString, params object[] args);

		// Token: 0x06000D63 RID: 3427
		void TracePfdPass(Guid activityId, long etlTraceId, string message);

		// Token: 0x06000D64 RID: 3428
		void TracePfdPass<T0>(Guid activityId, long etlTraceId, string formatString, T0 arg0);

		// Token: 0x06000D65 RID: 3429
		void TracePfdPass<T0, T1>(Guid activityId, long etlTraceId, string formatString, T0 arg0, T1 arg1);

		// Token: 0x06000D66 RID: 3430
		void TracePfdPass<T0, T1, T2>(Guid activityId, long etlTraceId, string formatString, T0 arg0, T1 arg1, T2 arg2);

		// Token: 0x06000D67 RID: 3431
		void TracePfdPass(Guid activityId, long etlTraceId, string formatString, params object[] args);

		// Token: 0x06000D68 RID: 3432
		void TraceFail(Guid activityId, long etlTraceId, string message);

		// Token: 0x06000D69 RID: 3433
		void TraceFail<T0>(Guid activityId, long etlTraceId, string formatString, T0 arg0);

		// Token: 0x06000D6A RID: 3434
		void TraceFail<T0, T1>(Guid activityId, long etlTraceId, string formatString, T0 arg0, T1 arg1);

		// Token: 0x06000D6B RID: 3435
		void TraceFail<T0, T1, T2>(Guid activityId, long etlTraceId, string formatString, T0 arg0, T1 arg1, T2 arg2);

		// Token: 0x06000D6C RID: 3436
		void TraceFail(Guid activityId, long etlTraceId, string formatString, params object[] args);

		// Token: 0x06000D6D RID: 3437
		void TracePass(ISystemProbeTraceable activityIdHolder, long etlTraceId, string message);

		// Token: 0x06000D6E RID: 3438
		void TracePass<T0>(ISystemProbeTraceable activityIdHolder, long etlTraceId, string formatString, T0 arg0);

		// Token: 0x06000D6F RID: 3439
		void TracePass<T0, T1>(ISystemProbeTraceable activityIdHolder, long etlTraceId, string formatString, T0 arg0, T1 arg1);

		// Token: 0x06000D70 RID: 3440
		void TracePass<T0, T1, T2>(ISystemProbeTraceable activityIdHolder, long etlTraceId, string formatString, T0 arg0, T1 arg1, T2 arg2);

		// Token: 0x06000D71 RID: 3441
		void TracePass(ISystemProbeTraceable activityIdHolder, long etlTraceId, string formatString, params object[] args);

		// Token: 0x06000D72 RID: 3442
		void TracePfdPass(ISystemProbeTraceable activityIdHolder, long etlTraceId, string message);

		// Token: 0x06000D73 RID: 3443
		void TracePfdPass<T0>(ISystemProbeTraceable activityIdHolder, long etlTraceId, string formatString, T0 arg0);

		// Token: 0x06000D74 RID: 3444
		void TracePfdPass<T0, T1>(ISystemProbeTraceable activityIdHolder, long etlTraceId, string formatString, T0 arg0, T1 arg1);

		// Token: 0x06000D75 RID: 3445
		void TracePfdPass<T0, T1, T2>(ISystemProbeTraceable activityIdHolder, long etlTraceId, string formatString, T0 arg0, T1 arg1, T2 arg2);

		// Token: 0x06000D76 RID: 3446
		void TracePfdPass(ISystemProbeTraceable activityIdHolder, long etlTraceId, string formatString, params object[] args);

		// Token: 0x06000D77 RID: 3447
		void TraceFail(ISystemProbeTraceable activityIdHolder, long etlTraceId, string message);

		// Token: 0x06000D78 RID: 3448
		void TraceFail<T0>(ISystemProbeTraceable activityIdHolder, long etlTraceId, string formatString, T0 arg0);

		// Token: 0x06000D79 RID: 3449
		void TraceFail<T0, T1>(ISystemProbeTraceable activityIdHolder, long etlTraceId, string formatString, T0 arg0, T1 arg1);

		// Token: 0x06000D7A RID: 3450
		void TraceFail<T0, T1, T2>(ISystemProbeTraceable activityIdHolder, long etlTraceId, string formatString, T0 arg0, T1 arg1, T2 arg2);

		// Token: 0x06000D7B RID: 3451
		void TraceFail(ISystemProbeTraceable activityIdHolder, long etlTraceId, string formatString, params object[] args);
	}
}
