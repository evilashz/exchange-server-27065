using System;
using System.Collections.Generic;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000287 RID: 647
	internal interface IMobileRecognitionResult : IUMRecognitionPhrase
	{
		// Token: 0x0600132F RID: 4911
		List<IUMRecognitionPhrase> GetRecognitionResults();

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001330 RID: 4912
		MobileSpeechRecoResultType MobileScenarioResultType { get; }
	}
}
