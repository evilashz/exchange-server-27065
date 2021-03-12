using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000286 RID: 646
	internal interface IMobileRecognizer : IDisposable
	{
		// Token: 0x0600132D RID: 4909
		void LoadGrammars(List<UMGrammar> grammars);

		// Token: 0x0600132E RID: 4910
		void RecognizeAsync(byte[] audioBytes, RecognizeCompletedDelegate callback);
	}
}
