using System;

namespace Microsoft.Exchange.Data.ImageAnalysis
{
	// Token: 0x0200000A RID: 10
	public static class ImageAnalysisFactory
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002815 File Offset: 0x00000A15
		public static IImageAnalysis GetImageAnalysisImpl()
		{
			return new ImageAnalysis();
		}
	}
}
