using System;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x0200000F RID: 15
	internal enum ProcessingStatus
	{
		// Token: 0x04000075 RID: 117
		NeedProcessing,
		// Token: 0x04000076 RID: 118
		InProcessing,
		// Token: 0x04000077 RID: 119
		ReadyToWriteToDatabase,
		// Token: 0x04000078 RID: 120
		CompletedProcessing,
		// Token: 0x04000079 RID: 121
		Unknown
	}
}
