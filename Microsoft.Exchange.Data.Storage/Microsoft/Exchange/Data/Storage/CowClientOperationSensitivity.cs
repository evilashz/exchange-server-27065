using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000634 RID: 1588
	[Flags]
	internal enum CowClientOperationSensitivity
	{
		// Token: 0x040023FE RID: 9214
		Skip = 0,
		// Token: 0x040023FF RID: 9215
		Capture = 1,
		// Token: 0x04002400 RID: 9216
		PerformOperation = 2,
		// Token: 0x04002401 RID: 9217
		CaptureAndPerformOperation = 3
	}
}
