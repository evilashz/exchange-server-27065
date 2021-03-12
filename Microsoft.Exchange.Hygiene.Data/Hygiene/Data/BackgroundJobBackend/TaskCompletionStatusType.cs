using System;

namespace Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend
{
	// Token: 0x0200002E RID: 46
	public enum TaskCompletionStatusType : byte
	{
		// Token: 0x040000DF RID: 223
		None,
		// Token: 0x040000E0 RID: 224
		Success,
		// Token: 0x040000E1 RID: 225
		Timedout,
		// Token: 0x040000E2 RID: 226
		Failed,
		// Token: 0x040000E3 RID: 227
		NonConformingJobDef,
		// Token: 0x040000E4 RID: 228
		ScheduleDeactivated,
		// Token: 0x040000E5 RID: 229
		ScheduleMissing,
		// Token: 0x040000E6 RID: 230
		ExecutableMissing
	}
}
