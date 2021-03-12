using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Speech.Recognition;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x0200005B RID: 91
	internal class UcmaRecognitionResult : UcmaRecognitionPhrase, IUMRecognitionResult, IUMRecognitionPhrase
	{
		// Token: 0x060003FD RID: 1021 RVA: 0x00012323 File Offset: 0x00010523
		internal UcmaRecognitionResult(RecognitionResult recognitionResult) : base(recognitionResult)
		{
			this.recognitionResult = recognitionResult;
			this.CloneSpeechResults();
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00012339 File Offset: 0x00010539
		public List<List<IUMRecognitionPhrase>> GetSpeechRecognitionResults()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "UcmaRecognitionResult::GetSpeechRecognitionResults().", new object[0]);
			return this.alternatesList;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00012358 File Offset: 0x00010558
		private void CloneSpeechResults()
		{
			this.alternatesList = new List<List<IUMRecognitionPhrase>>();
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "UcmaRecognitionResult::CloneSpeechResults().", new object[0]);
			foreach (RecognizedPhrase recognizedPhrase in this.recognitionResult.Alternates)
			{
				PIIMessage data = PIIMessage.Create(PIIType._PII, recognizedPhrase.Text);
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data, "UcmaRecognitionResult::CloneSpeechResults(Alternate = _PII Grammar=\"{0}\").", new object[]
				{
					(recognizedPhrase.Grammar != null) ? recognizedPhrase.Grammar.RuleName : "<null>"
				});
				foreach (RecognizedPhrase recognizedPhrase2 in this.recognitionResult.Homophones)
				{
					PIIMessage data2 = PIIMessage.Create(PIIType._PII, recognizedPhrase2.Text);
					CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data2, "UcmaRecognitionResult::CloneSpeechResults(Homophone = _PII Grammar=\"{0}\")", new object[]
					{
						(recognizedPhrase2.Grammar != null) ? recognizedPhrase2.Grammar.RuleName : "<null>"
					});
				}
				List<IUMRecognitionPhrase> list = null;
				foreach (List<IUMRecognitionPhrase> list2 in this.alternatesList)
				{
					if (list2[0].HomophoneGroupId == recognizedPhrase.HomophoneGroupId)
					{
						list = list2;
						break;
					}
				}
				if (list == null)
				{
					list = new List<IUMRecognitionPhrase>();
					this.alternatesList.Add(list);
				}
				list.Add(new UcmaRecognitionPhrase(recognizedPhrase));
			}
		}

		// Token: 0x0400013E RID: 318
		private RecognitionResult recognitionResult;

		// Token: 0x0400013F RID: 319
		private List<List<IUMRecognitionPhrase>> alternatesList;
	}
}
