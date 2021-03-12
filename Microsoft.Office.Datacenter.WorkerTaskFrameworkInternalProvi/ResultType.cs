using System;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000017 RID: 23
	public enum ResultType
	{
		// Token: 0x040000DC RID: 220
		Abandoned,
		// Token: 0x040000DD RID: 221
		TimedOut,
		// Token: 0x040000DE RID: 222
		Poisoned,
		// Token: 0x040000DF RID: 223
		Succeeded,
		// Token: 0x040000E0 RID: 224
		Failed,
		// Token: 0x040000E1 RID: 225
		Quarantined,
		// Token: 0x040000E2 RID: 226
		Rejected
	}
}
