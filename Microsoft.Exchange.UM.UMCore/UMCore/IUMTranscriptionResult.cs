using System;
using System.Collections.Generic;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000163 RID: 355
	internal interface IUMTranscriptionResult
	{
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000A72 RID: 2674
		float Confidence { get; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000A73 RID: 2675
		TimeSpan AudioDuration { get; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000A74 RID: 2676
		int TotalWords { get; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000A75 RID: 2677
		int CustomWords { get; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000A76 RID: 2678
		int TopNWords { get; }

		// Token: 0x06000A77 RID: 2679
		List<IUMRecognizedWord> GetRecognizedWords();

		// Token: 0x06000A78 RID: 2680
		List<IUMRecognizedFeature> GetRecognizedFeatures(int firstWordOffset);
	}
}
