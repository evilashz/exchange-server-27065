using System;

namespace System.Threading
{
	// Token: 0x020004EA RID: 1258
	[Serializable]
	internal enum StackCrawlMark
	{
		// Token: 0x04001941 RID: 6465
		LookForMe,
		// Token: 0x04001942 RID: 6466
		LookForMyCaller,
		// Token: 0x04001943 RID: 6467
		LookForMyCallersCaller,
		// Token: 0x04001944 RID: 6468
		LookForThread
	}
}
