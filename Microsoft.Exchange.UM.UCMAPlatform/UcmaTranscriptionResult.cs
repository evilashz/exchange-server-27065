using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Speech.Recognition;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x02000058 RID: 88
	internal class UcmaTranscriptionResult : IUMTranscriptionResult
	{
		// Token: 0x060003DD RID: 989 RVA: 0x000118E8 File Offset: 0x0000FAE8
		internal UcmaTranscriptionResult(RecognitionResult transcriptionResult)
		{
			this.confidence = transcriptionResult.Confidence;
			this.rawWords = transcriptionResult.Words;
			this.audioPosition = transcriptionResult.Audio.AudioPosition;
			this.audioDuration = transcriptionResult.Audio.Duration;
			this.debugText = transcriptionResult.Text;
			this.semanticRoot = UcmaTranscriptionResult.BuildSemanticRoot(transcriptionResult);
			this.replacementWords = UcmaTranscriptionResult.BuildReplacementWordList(transcriptionResult.ReplacementWordUnits, this.semanticRoot, this.rawWords);
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0001196A File Offset: 0x0000FB6A
		public float Confidence
		{
			get
			{
				return this.confidence;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00011972 File Offset: 0x0000FB72
		public TimeSpan AudioDuration
		{
			get
			{
				return this.audioDuration;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0001197A File Offset: 0x0000FB7A
		public int TotalWords
		{
			get
			{
				return this.rawWords.Count;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00011987 File Offset: 0x0000FB87
		public int CustomWords
		{
			get
			{
				if (this.lazyCustomWords == null)
				{
					this.lazyCustomWords = new int?(this.GetWordCount("customGrammarWords"));
				}
				return this.lazyCustomWords.Value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x000119B7 File Offset: 0x0000FBB7
		public int TopNWords
		{
			get
			{
				if (this.lazyTopNWords == null)
				{
					this.lazyTopNWords = new int?(this.GetWordCount("topNWords"));
				}
				return this.lazyTopNWords.Value;
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x000119E8 File Offset: 0x0000FBE8
		public List<IUMRecognizedWord> GetRecognizedWords()
		{
			List<IUMRecognizedWord> list = new List<IUMRecognizedWord>();
			TimeSpan transcriptionResultAudioPosition = this.audioPosition;
			int i = 0;
			using (List<UcmaReplacementText>.Enumerator enumerator = this.replacementWords.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					UcmaReplacementText ucmaReplacementText = enumerator.Current;
					while (i < ucmaReplacementText.FirstWordIndex)
					{
						RecognizedWordUnit recognizedWordUnit = this.rawWords[i++];
						list.Add(new UcmaRecognizedWordUnit(recognizedWordUnit, transcriptionResultAudioPosition));
					}
					list.Add(new UcmaRecognizedWordUnit(ucmaReplacementText, this.rawWords, transcriptionResultAudioPosition));
					i += ucmaReplacementText.CountOfWords;
				}
				goto IL_A6;
			}
			IL_86:
			RecognizedWordUnit recognizedWordUnit2 = this.rawWords[i++];
			list.Add(new UcmaRecognizedWordUnit(recognizedWordUnit2, transcriptionResultAudioPosition));
			IL_A6:
			if (i >= this.rawWords.Count)
			{
				return list;
			}
			goto IL_86;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00011ABC File Offset: 0x0000FCBC
		public List<IUMRecognizedFeature> GetRecognizedFeatures(int firstWordOffset)
		{
			List<IUMRecognizedFeature> list = new List<IUMRecognizedFeature>();
			try
			{
				foreach (KeyValuePair<string, SemanticValue> fragment in this.semanticRoot)
				{
					UcmaRecognizedFeature item;
					if (UcmaRecognizedFeature.TryCreate(fragment, firstWordOffset, this.replacementWords, out item))
					{
						list.Add(item);
					}
				}
			}
			catch (InvalidOperationException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, this, "Exception accessing semantics {0}", new object[]
				{
					ex
				});
			}
			return list;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00011B58 File Offset: 0x0000FD58
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Confidence:{0} DurationMSEC:{1} Text:{2}", new object[]
			{
				this.Confidence,
				this.AudioDuration,
				this.debugText
			});
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00011BA4 File Offset: 0x0000FDA4
		private static List<UcmaReplacementText> BuildReplacementWordList(Collection<ReplacementText> speechReplacements, SemanticValue semanticRoot, ReadOnlyCollection<RecognizedWordUnit> words)
		{
			List<UcmaReplacementText> result = UcmaTranscriptionResult.CloneReplacementWords(speechReplacements);
			List<UcmaReplacementText> list = UcmaTranscriptionResult.CreatePhoneReplacementText(semanticRoot, words);
			foreach (UcmaReplacementText phoneText in list)
			{
				result = UcmaTranscriptionResult.InsertMissingPhoneText(phoneText, result);
			}
			return result;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00011C04 File Offset: 0x0000FE04
		private static List<UcmaReplacementText> CloneReplacementWords(Collection<ReplacementText> speechReplacements)
		{
			List<UcmaReplacementText> list = new List<UcmaReplacementText>(speechReplacements.Count);
			foreach (ReplacementText replacementText in speechReplacements)
			{
				UcmaReplacementText item = new UcmaReplacementText(replacementText.Text, replacementText.DisplayAttributes, replacementText.FirstWordIndex, replacementText.CountOfWords);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00011C9C File Offset: 0x0000FE9C
		private static List<UcmaReplacementText> CreatePhoneReplacementText(SemanticValue root, ReadOnlyCollection<RecognizedWordUnit> words)
		{
			List<UcmaReplacementText> list = new List<UcmaReplacementText>();
			try
			{
				foreach (KeyValuePair<string, SemanticValue> keyValuePair in root)
				{
					string text = UcmaRecognizedFeature.ParseName(keyValuePair.Value);
					if (text.Equals("PhoneNumber", StringComparison.OrdinalIgnoreCase))
					{
						string text2 = UcmaRecognizedFeature.ParsePhoneNumberSemanticValue(keyValuePair.Value);
						int num = UcmaRecognizedFeature.ParseWordCount(keyValuePair.Value);
						int num2 = UcmaRecognizedFeature.ParseFirstWordIndex(keyValuePair.Value);
						int index = num2 + num - 1;
						DisplayAttributes displayAttributes = 16 & words[num2].DisplayAttributes;
						displayAttributes |= words[index].DisplayAttributes;
						list.Add(new UcmaReplacementText(text2, displayAttributes, num2, num));
					}
				}
				list.Sort((UcmaReplacementText lhs, UcmaReplacementText rhs) => lhs.FirstWordIndex.CompareTo(rhs.FirstWordIndex));
			}
			catch (InvalidOperationException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, null, "Exception accessing semantics {0}", new object[]
				{
					ex
				});
			}
			return list;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00011DC8 File Offset: 0x0000FFC8
		private static List<UcmaReplacementText> InsertMissingPhoneText(UcmaReplacementText phoneText, List<UcmaReplacementText> replacementWords)
		{
			bool flag = true;
			int i;
			for (i = 0; i < replacementWords.Count; i++)
			{
				UcmaReplacementText ucmaReplacementText = replacementWords[i];
				if (ucmaReplacementText.FirstWordIndex == phoneText.FirstWordIndex)
				{
					PIIMessage[] data = new PIIMessage[]
					{
						PIIMessage.Create(PIIType._PII, phoneText.Text),
						PIIMessage.Create(PIIType._PII, ucmaReplacementText.Text)
					};
					CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, null, data, "Ignoring phone semantic value _PII1 because there is already replacement text _PII2 for it.", new object[0]);
					flag = false;
					break;
				}
				if (ucmaReplacementText.FirstWordIndex > phoneText.FirstWordIndex)
				{
					break;
				}
			}
			if (flag)
			{
				PIIMessage data2 = PIIMessage.Create(PIIType._PII, phoneText.Text);
				CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, null, data2, "Adding phone semantic value _PII at index '{0}' because no other suitable replacement text was found.", new object[]
				{
					i
				});
				replacementWords.Insert(i, phoneText);
			}
			return replacementWords;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00011E94 File Offset: 0x00010094
		private static SemanticValue BuildSemanticRoot(RecognitionResult transcriptionResult)
		{
			SemanticValue result = null;
			try
			{
				result = transcriptionResult.Semantics["Fragments"];
			}
			catch (InvalidOperationException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, null, "Exception accessing semantics {0}", new object[]
				{
					ex
				});
				result = new SemanticValue(string.Empty);
			}
			return result;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00011EF4 File Offset: 0x000100F4
		private int GetWordCount(string semanticTarget)
		{
			int result = 0;
			try
			{
				foreach (KeyValuePair<string, SemanticValue> keyValuePair in this.semanticRoot)
				{
					this.GetWordCount(keyValuePair, semanticTarget, ref result);
				}
			}
			catch (InvalidOperationException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, this, "Exception accessing semantics {0}", new object[]
				{
					ex
				});
			}
			return result;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00011F78 File Offset: 0x00010178
		private void GetWordCount(KeyValuePair<string, SemanticValue> semanticRoot, string semanticTarget, ref int count)
		{
			if (semanticRoot.Value == null || semanticRoot.Value.Count == 0)
			{
				return;
			}
			if (semanticRoot.Value.ContainsKey(semanticTarget))
			{
				SemanticValue semanticValue = semanticRoot.Value["_attributes"];
				string s = (string)semanticValue["CountOfWords"].Value;
				count += int.Parse(s, CultureInfo.InvariantCulture);
			}
			foreach (KeyValuePair<string, SemanticValue> keyValuePair in semanticRoot.Value)
			{
				this.GetWordCount(keyValuePair, semanticTarget, ref count);
			}
		}

		// Token: 0x04000133 RID: 307
		private readonly float confidence;

		// Token: 0x04000134 RID: 308
		private readonly string debugText;

		// Token: 0x04000135 RID: 309
		private readonly TimeSpan audioPosition;

		// Token: 0x04000136 RID: 310
		private readonly TimeSpan audioDuration;

		// Token: 0x04000137 RID: 311
		private List<UcmaReplacementText> replacementWords;

		// Token: 0x04000138 RID: 312
		private SemanticValue semanticRoot;

		// Token: 0x04000139 RID: 313
		private ReadOnlyCollection<RecognizedWordUnit> rawWords;

		// Token: 0x0400013A RID: 314
		private int? lazyCustomWords;

		// Token: 0x0400013B RID: 315
		private int? lazyTopNWords;
	}
}
