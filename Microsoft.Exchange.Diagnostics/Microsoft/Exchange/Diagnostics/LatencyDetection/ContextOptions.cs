using System;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x0200016D RID: 365
	[Flags]
	internal enum ContextOptions
	{
		// Token: 0x04000705 RID: 1797
		Default = 0,
		// Token: 0x04000706 RID: 1798
		DoNotMeasureTime = 1,
		// Token: 0x04000707 RID: 1799
		DoNotCreateReport = 2
	}
}
