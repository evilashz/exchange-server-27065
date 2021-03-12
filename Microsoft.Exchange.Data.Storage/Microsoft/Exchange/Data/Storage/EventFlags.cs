using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020006FF RID: 1791
	[Flags]
	internal enum EventFlags
	{
		// Token: 0x040026CA RID: 9930
		None = 0,
		// Token: 0x040026CB RID: 9931
		ReminderPropertiesModified = 1,
		// Token: 0x040026CC RID: 9932
		TimerEventFired = 2,
		// Token: 0x040026CD RID: 9933
		NonIPMChange = 4
	}
}
