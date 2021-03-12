using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Speech.Recognition;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x02000059 RID: 89
	internal abstract class UcmaRecognitionPhraseBase : IUMRecognitionPhrase
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003EE RID: 1006
		public abstract float Confidence { get; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003EF RID: 1007
		public abstract string Text { get; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003F0 RID: 1008
		public abstract int HomophoneGroupId { get; }

		// Token: 0x170000D1 RID: 209
		public abstract object this[string key]
		{
			get;
		}

		// Token: 0x060003F2 RID: 1010
		public abstract string ToSml();

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003F3 RID: 1011
		protected abstract ReadOnlyCollection<RecognizedWordUnit> WordUnits { get; }

		// Token: 0x060003F4 RID: 1012 RVA: 0x00012028 File Offset: 0x00010228
		public bool ShouldAcceptBasedOnSmartConfidence(Dictionary<string, string> wordsToIgnore)
		{
			if (wordsToIgnore == null)
			{
				PIIMessage data = PIIMessage.Create(PIIType._PII, this.Text);
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data, "UcmaRecognitionResult::ShouldAcceptBasedOnSmartConfidence- no words to ignore returning original confidence for phrase _PII, confidence '{0}'", new object[]
				{
					this.Confidence
				});
				return true;
			}
			if (wordsToIgnore.Count < 1)
			{
				PIIMessage data2 = PIIMessage.Create(PIIType._PII, this.Text);
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data2, "UcmaRecognitionResult::ShouldAcceptBasedOnSmartConfidence- no words to ignore returning original confidence for phrase _PII, confidence '{0}'", new object[]
				{
					this.Confidence
				});
				return true;
			}
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			for (int i = 0; i < this.WordUnits.Count; i++)
			{
				RecognizedWordUnit recognizedWordUnit = this.WordUnits[i];
				if (!wordsToIgnore.ContainsKey(recognizedWordUnit.Text))
				{
					PIIMessage data3 = PIIMessage.Create(PIIType._PII, recognizedWordUnit.Text);
					CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data3, "UcmaRecognitionResult::ShouldAcceptBasedOnSmartConfidence- payload word=_PII, confidence='{0}'", new object[]
					{
						recognizedWordUnit.Confidence
					});
					num2 += recognizedWordUnit.Confidence;
					num += 1f;
				}
				else
				{
					PIIMessage data4 = PIIMessage.Create(PIIType._PII, recognizedWordUnit.Text);
					CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data4, "UcmaRecognitionResult::ShouldAcceptBasedOnSmartConfidence- keyword word _PII, confidence='{0}'", new object[]
					{
						recognizedWordUnit.Confidence
					});
					num4 += recognizedWordUnit.Confidence;
					num3 += 1f;
				}
			}
			float num5 = 0f;
			if (num2 > 0f && num > 0f)
			{
				num5 = num2 / num;
			}
			float num6 = 0f;
			if (num4 > 0f && num3 > 0f)
			{
				num6 = num4 / num3;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "UcmaRecognitionResult::ShouldAcceptBasedOnSmartConfidence- keyWord Time based confidence {0}, keywordsConfidence sum ='{1}', keywordsCount='{2}'", new object[]
			{
				num6,
				num4,
				num3
			});
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "UcmaRecognitionResult::ShouldAcceptBasedOnSmartConfidence- payload Time based confidence {0}, payloadConfidence sum ='{1}', payloadCount='{2}'", new object[]
			{
				num5,
				num2,
				num
			});
			return num6 >= 0.25f && num5 >= 0.25f;
		}
	}
}
