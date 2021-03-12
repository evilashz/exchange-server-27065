using System;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x0200001A RID: 26
	internal enum AsyncQueuePriority : byte
	{
		// Token: 0x0400005F RID: 95
		High,
		// Token: 0x04000060 RID: 96
		Normal = 50,
		// Token: 0x04000061 RID: 97
		Low = 100,
		// Token: 0x04000062 RID: 98
		System_High = 253,
		// Token: 0x04000063 RID: 99
		System_Normal,
		// Token: 0x04000064 RID: 100
		System_Low
	}
}
