using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Exchange.Data.ImageAnalysis
{
	// Token: 0x02000003 RID: 3
	public interface IImageAnalysis
	{
		// Token: 0x06000001 RID: 1
		KeyValuePair<byte[], ImageAnalysisResult> GenerateThumbnail(Stream imageStream, int minImageWidth, int minImageHeight, int maxThumbnailWidth, int maxThumbnailHeight, out int width, out int height);

		// Token: 0x06000002 RID: 2
		ISalientObjectAnalysis GetSalientObjectanalysis(byte[] imageData, int imageWidth, int imageHeight);
	}
}
