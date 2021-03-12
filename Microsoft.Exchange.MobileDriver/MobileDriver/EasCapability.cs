using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x0200001B RID: 27
	internal class EasCapability : MobileServiceCapability
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00004351 File Offset: 0x00002551
		internal EasCapability(PartType supportedPartType, int segmentPerPart, IList<CodingSupportability> codingSupportabilities, FeatureSupportability featureSupportabilities) : base(supportedPartType, segmentPerPart, codingSupportabilities, featureSupportabilities)
		{
		}
	}
}
