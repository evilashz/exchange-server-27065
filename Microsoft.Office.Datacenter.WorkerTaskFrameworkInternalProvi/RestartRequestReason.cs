using System;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000019 RID: 25
	public enum RestartRequestReason
	{
		// Token: 0x040000E9 RID: 233
		DataAccessError,
		// Token: 0x040000EA RID: 234
		PoisonResult,
		// Token: 0x040000EB RID: 235
		Maintenance,
		// Token: 0x040000EC RID: 236
		Unknown,
		// Token: 0x040000ED RID: 237
		SelfHealing
	}
}
