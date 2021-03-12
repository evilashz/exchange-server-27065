using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002F4 RID: 756
	internal interface IPlatformUtilities
	{
		// Token: 0x0600170D RID: 5901
		bool IsTranscriptionLanguageSupported(CultureInfo transcriptionLanguage);

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x0600170E RID: 5902
		IEnumerable<CultureInfo> SupportedTranscriptionLanguages { get; }

		// Token: 0x0600170F RID: 5903
		void CompileGrammar(string grxmlGrammarPath, string compiledGrammarPath, CultureInfo culture);

		// Token: 0x06001710 RID: 5904
		void CheckGrammarEntryFormat(string wordToCheck);

		// Token: 0x06001711 RID: 5905
		ITempWavFile SynthesizePromptsToPcmWavFile(ArrayList prompts);

		// Token: 0x06001712 RID: 5906
		void RecycleServiceDependencies();

		// Token: 0x06001713 RID: 5907
		void InitializeG723Support();
	}
}
