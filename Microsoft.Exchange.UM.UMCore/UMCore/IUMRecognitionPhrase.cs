using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000160 RID: 352
	internal interface IUMRecognitionPhrase
	{
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000A69 RID: 2665
		float Confidence { get; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000A6A RID: 2666
		string Text { get; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000A6B RID: 2667
		int HomophoneGroupId { get; }

		// Token: 0x1700029F RID: 671
		object this[string key]
		{
			get;
		}

		// Token: 0x06000A6D RID: 2669
		bool ShouldAcceptBasedOnSmartConfidence(Dictionary<string, string> wordsToIgnore);
	}
}
