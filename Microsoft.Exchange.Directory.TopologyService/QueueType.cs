using System;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x0200001C RID: 28
	[Flags]
	internal enum QueueType
	{
		// Token: 0x04000070 RID: 112
		None = 0,
		// Token: 0x04000071 RID: 113
		Urgent = 1,
		// Token: 0x04000072 RID: 114
		UnTimed = 2,
		// Token: 0x04000073 RID: 115
		Timed = 4,
		// Token: 0x04000074 RID: 116
		All = 7
	}
}
