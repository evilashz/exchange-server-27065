using System;

namespace Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend
{
	// Token: 0x0200002D RID: 45
	public enum TaskExecutionStateType : byte
	{
		// Token: 0x040000D6 RID: 214
		NotStarted,
		// Token: 0x040000D7 RID: 215
		Started,
		// Token: 0x040000D8 RID: 216
		Election,
		// Token: 0x040000D9 RID: 217
		Inauguration,
		// Token: 0x040000DA RID: 218
		Running,
		// Token: 0x040000DB RID: 219
		Failover,
		// Token: 0x040000DC RID: 220
		Failed,
		// Token: 0x040000DD RID: 221
		Completed
	}
}
