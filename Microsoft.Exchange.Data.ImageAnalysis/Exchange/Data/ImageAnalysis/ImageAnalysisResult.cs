using System;

namespace Microsoft.Exchange.Data.ImageAnalysis
{
	// Token: 0x02000002 RID: 2
	public enum ImageAnalysisResult
	{
		// Token: 0x04000002 RID: 2
		ThumbnailSuccess,
		// Token: 0x04000003 RID: 3
		SalientRegionSuccess,
		// Token: 0x04000004 RID: 4
		UnknownFailure,
		// Token: 0x04000005 RID: 5
		ImageTooSmallForAnalysis,
		// Token: 0x04000006 RID: 6
		UnableToPerformSalientRegionAnalysis,
		// Token: 0x04000007 RID: 7
		ImageTooBigForAnalysis
	}
}
