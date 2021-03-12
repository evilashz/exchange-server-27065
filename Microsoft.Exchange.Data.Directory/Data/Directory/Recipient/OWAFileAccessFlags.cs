using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000230 RID: 560
	internal enum OWAFileAccessFlags
	{
		// Token: 0x04000D08 RID: 3336
		DirectFileAccessEnabledMask = 1,
		// Token: 0x04000D09 RID: 3337
		WebReadyDocumentViewingEnabledMask,
		// Token: 0x04000D0A RID: 3338
		ForceWebReadyDocumentViewingFirstMask = 4,
		// Token: 0x04000D0B RID: 3339
		WacViewingEnabledMask = 8,
		// Token: 0x04000D0C RID: 3340
		ForceWacViewingFirstMask = 16
	}
}
