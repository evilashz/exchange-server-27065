using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x020004FC RID: 1276
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum ThreadState
	{
		// Token: 0x04001969 RID: 6505
		Running = 0,
		// Token: 0x0400196A RID: 6506
		StopRequested = 1,
		// Token: 0x0400196B RID: 6507
		SuspendRequested = 2,
		// Token: 0x0400196C RID: 6508
		Background = 4,
		// Token: 0x0400196D RID: 6509
		Unstarted = 8,
		// Token: 0x0400196E RID: 6510
		Stopped = 16,
		// Token: 0x0400196F RID: 6511
		WaitSleepJoin = 32,
		// Token: 0x04001970 RID: 6512
		Suspended = 64,
		// Token: 0x04001971 RID: 6513
		AbortRequested = 128,
		// Token: 0x04001972 RID: 6514
		Aborted = 256
	}
}
