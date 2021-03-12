using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000618 RID: 1560
	internal enum ContactLinkingProcessingState
	{
		// Token: 0x0400236D RID: 9069
		Unknown,
		// Token: 0x0400236E RID: 9070
		DoNotProcess,
		// Token: 0x0400236F RID: 9071
		ProcessBeforeSave,
		// Token: 0x04002370 RID: 9072
		ProcessAfterSave,
		// Token: 0x04002371 RID: 9073
		Processed
	}
}
