using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000619 RID: 1561
	internal enum COWProcessorState
	{
		// Token: 0x04002373 RID: 9075
		Unknown,
		// Token: 0x04002374 RID: 9076
		DoNotProcess,
		// Token: 0x04002375 RID: 9077
		ProcessAfterSave,
		// Token: 0x04002376 RID: 9078
		Processed
	}
}
