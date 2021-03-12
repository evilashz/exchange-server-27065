using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000278 RID: 632
	internal interface IMobileSpeechRecoRequestBehavior
	{
		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060012C6 RID: 4806
		Guid Id { get; }

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060012C7 RID: 4807
		CultureInfo Culture { get; }

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060012C8 RID: 4808
		SpeechRecognitionEngineType EngineType { get; }

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060012C9 RID: 4809
		int MaxAlternates { get; }

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060012CA RID: 4810
		int MaxProcessingTime { get; }

		// Token: 0x060012CB RID: 4811
		List<UMGrammar> PrepareGrammars();

		// Token: 0x060012CC RID: 4812
		string ProcessRecoResults(List<IMobileRecognitionResult> results);

		// Token: 0x060012CD RID: 4813
		bool CanProcessResultType(MobileSpeechRecoResultType resultType);
	}
}
