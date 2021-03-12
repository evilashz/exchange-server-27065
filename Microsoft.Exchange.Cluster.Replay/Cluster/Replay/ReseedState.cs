using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000283 RID: 643
	internal enum ReseedState
	{
		// Token: 0x040009FF RID: 2559
		Unknown,
		// Token: 0x04000A00 RID: 2560
		Resume,
		// Token: 0x04000A01 RID: 2561
		AssignSpare,
		// Token: 0x04000A02 RID: 2562
		InPlaceReseed,
		// Token: 0x04000A03 RID: 2563
		Completed
	}
}
