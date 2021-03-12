using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000161 RID: 353
	internal interface IUMRecognitionResult : IUMRecognitionPhrase
	{
		// Token: 0x06000A6E RID: 2670
		List<List<IUMRecognitionPhrase>> GetSpeechRecognitionResults();
	}
}
