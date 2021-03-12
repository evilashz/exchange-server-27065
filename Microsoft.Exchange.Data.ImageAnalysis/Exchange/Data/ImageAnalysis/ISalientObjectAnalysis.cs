using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.ImageAnalysis
{
	// Token: 0x02000004 RID: 4
	public interface ISalientObjectAnalysis
	{
		// Token: 0x06000003 RID: 3
		KeyValuePair<List<RegionRect>, ImageAnalysisResult> GetSalientRectsAsList();

		// Token: 0x06000004 RID: 4
		KeyValuePair<byte[], ImageAnalysisResult> GetSalientRectsAsByteArray();
	}
}
