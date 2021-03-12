using System;

namespace Microsoft.Isam.Esent.Interop.Windows8
{
	// Token: 0x02000317 RID: 791
	[Flags]
	public enum StopServiceGrbit
	{
		// Token: 0x040009BC RID: 2492
		All = 0,
		// Token: 0x040009BD RID: 2493
		BackgroundUserTasks = 2,
		// Token: 0x040009BE RID: 2494
		QuiesceCaches = 4,
		// Token: 0x040009BF RID: 2495
		Resume = -2147483648
	}
}
