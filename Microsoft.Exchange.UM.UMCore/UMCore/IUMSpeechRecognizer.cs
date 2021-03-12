using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000162 RID: 354
	internal interface IUMSpeechRecognizer
	{
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000A6F RID: 2671
		// (remove) Token: 0x06000A70 RID: 2672
		event UMCallSessionHandler<UMSpeechEventArgs> OnSpeech;

		// Token: 0x06000A71 RID: 2673
		void PlayPrompts(ArrayList prompts, int minDigits, int maxDigits, int timeout, string stopTones, int interDigitTimeout, StopPatterns stopPatterns, int startIdx, TimeSpan offset, List<UMGrammar> grammars, bool expetingSpeechInput, int babbleTimeout, bool stopPromptOnBargeIn, string turnName, int initialSilenceTimeout);
	}
}
