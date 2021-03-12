using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.ApplicationLogic.UM;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.MessageContent;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001FD RID: 509
	internal class TranscriptionData : ITranscriptionData
	{
		// Token: 0x06000EDD RID: 3805 RVA: 0x00042E50 File Offset: 0x00041050
		internal TranscriptionData(RecoResultType recognitionResult, RecoErrorType recognitionError, CultureInfo language, List<IUMTranscriptionResult> transcriptionResults) : this(recognitionResult, recognitionError, language, transcriptionResults, null, null)
		{
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x00042E60 File Offset: 0x00041060
		internal TranscriptionData(RecoResultType recognitionResult, RecoErrorType recognitionError, CultureInfo language, List<IUMTranscriptionResult> transcriptionResults, List<int> testHookParagraphs, List<int> testHookSentences)
		{
			this.RecognitionResult = recognitionResult;
			this.RecognitionError = recognitionError;
			this.Language = language;
			this.config = LocConfig.Instance[language].Transcription;
			float num = 0f;
			float num2 = 0f;
			foreach (IUMTranscriptionResult iumtranscriptionResult in transcriptionResults)
			{
				this.recognizedFeatures.AddRange(iumtranscriptionResult.GetRecognizedFeatures(this.recognizedWords.Count));
				this.recognizedWords.AddRange(iumtranscriptionResult.GetRecognizedWords());
				float num3 = (float)iumtranscriptionResult.AudioDuration.TotalMilliseconds;
				num += num3 * iumtranscriptionResult.Confidence;
				num2 += num3;
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "TranscriptionResult:{0} confidenceDuration:{1} confidenceSum:{2}", new object[]
				{
					iumtranscriptionResult,
					num2,
					num
				});
				this.totalWords += iumtranscriptionResult.TotalWords;
				this.customWords += iumtranscriptionResult.CustomWords;
				this.topNWords += iumtranscriptionResult.TopNWords;
			}
			this.Confidence = ((num2 > 0f) ? (num / num2) : 0f);
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Confidence(final):{0} ConfidenceBand(final):{1} ", new object[]
			{
				this.Confidence,
				this.ConfidenceBand
			});
			if (this.recognizedWords.Count == 0)
			{
				if (this.RecognitionResult == RecoResultType.Attempted)
				{
					this.RecognitionResult = RecoResultType.Skipped;
					this.RecognitionError = RecoErrorType.AudioQualityPoor;
				}
				else if (this.RecognitionResult == RecoResultType.Partial)
				{
					this.RecognitionResult = RecoResultType.Skipped;
					this.RecognitionError = RecoErrorType.Throttled;
				}
			}
			else if (this.ConfidenceBand == ConfidenceBandType.Low)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Low confidence {0} detected.", new object[]
				{
					this.Confidence
				});
				this.RecognitionResult = RecoResultType.Skipped;
				this.RecognitionError = RecoErrorType.AudioQualityPoor;
				this.recognizedFeatures.Clear();
				this.recognizedWords.Clear();
			}
			this.recognizedParagraphs = new BitArray(this.recognizedWords.Count);
			this.recognizedSentences = new BitArray(this.recognizedWords.Count);
			if (AppConfig.Instance.Service.EnableTranscriptionWhitespace)
			{
				if (this.recognizedWords.Count > 0 && testHookParagraphs != null && testHookSentences != null)
				{
					foreach (int index in testHookParagraphs)
					{
						this.recognizedParagraphs.Set(index, true);
					}
					using (List<int>.Enumerator enumerator3 = testHookSentences.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							int index2 = enumerator3.Current;
							this.recognizedSentences.Set(index2, true);
						}
						goto IL_333;
					}
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Reformatting the transcription text using whitespacing logic", new object[0]);
				this.ReformatText();
			}
			IL_333:
			this.GenerateXML();
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x000431D0 File Offset: 0x000413D0
		public List<IUMRecognizedWord> RecognizedWords
		{
			get
			{
				return this.recognizedWords;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x000431D8 File Offset: 0x000413D8
		// (set) Token: 0x06000EE1 RID: 3809 RVA: 0x000431E0 File Offset: 0x000413E0
		public RecoResultType RecognitionResult { get; private set; }

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x000431E9 File Offset: 0x000413E9
		// (set) Token: 0x06000EE3 RID: 3811 RVA: 0x000431F1 File Offset: 0x000413F1
		public float Confidence { get; private set; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x000431FA File Offset: 0x000413FA
		// (set) Token: 0x06000EE5 RID: 3813 RVA: 0x00043202 File Offset: 0x00041402
		public CultureInfo Language { get; private set; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x0004320C File Offset: 0x0004140C
		public ConfidenceBandType ConfidenceBand
		{
			get
			{
				if ((double)this.Confidence > LocConfig.Instance[this.Language].Transcription.HighConfidence)
				{
					return ConfidenceBandType.High;
				}
				if ((double)this.Confidence > LocConfig.Instance[this.Language].Transcription.LowConfidence)
				{
					return ConfidenceBandType.Medium;
				}
				return ConfidenceBandType.Low;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x00043264 File Offset: 0x00041464
		// (set) Token: 0x06000EE8 RID: 3816 RVA: 0x0004326C File Offset: 0x0004146C
		public XmlDocument TranscriptionXml { get; private set; }

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x00043275 File Offset: 0x00041475
		// (set) Token: 0x06000EEA RID: 3818 RVA: 0x0004327D File Offset: 0x0004147D
		public RecoErrorType RecognitionError { get; private set; }

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x00043286 File Offset: 0x00041486
		// (set) Token: 0x06000EEC RID: 3820 RVA: 0x0004328E File Offset: 0x0004148E
		public string ErrorInformation { get; private set; }

		// Token: 0x06000EED RID: 3821 RVA: 0x00043298 File Offset: 0x00041498
		public string GetTrailingSpaces(int currentWordIndex)
		{
			IUMRecognizedWord iumrecognizedWord = this.recognizedWords[currentWordIndex];
			IUMRecognizedWord iumrecognizedWord2 = (currentWordIndex + 1 >= this.recognizedWords.Count) ? null : this.recognizedWords[currentWordIndex + 1];
			if (iumrecognizedWord2 != null && (iumrecognizedWord2.DisplayAttributes & UMDisplayAttributes.ConsumeLeadingSpaces) != UMDisplayAttributes.None)
			{
				return string.Empty;
			}
			if ((iumrecognizedWord.DisplayAttributes & UMDisplayAttributes.OneTrailingSpace) != UMDisplayAttributes.None)
			{
				return " ";
			}
			if ((iumrecognizedWord.DisplayAttributes & UMDisplayAttributes.TwoTrailingSpaces) != UMDisplayAttributes.None)
			{
				return "  ";
			}
			if ((iumrecognizedWord.DisplayAttributes & UMDisplayAttributes.ZeroTrailingSpaces) != UMDisplayAttributes.None)
			{
				return string.Empty;
			}
			if (iumrecognizedWord.DisplayAttributes == UMDisplayAttributes.None)
			{
				return string.Empty;
			}
			throw new UnexpectedSwitchValueException(iumrecognizedWord.DisplayAttributes.ToString());
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0004333C File Offset: 0x0004153C
		internal void UpdatePerfCounters()
		{
			TranscriptionCountersInstance instance = TranscriptionCounters.GetInstance(this.Language.Name);
			switch (this.RecognitionResult)
			{
			case RecoResultType.Attempted:
				Util.IncrementCounter(instance.VoiceMessagesProcessed);
				lock (TranscriptionData.perLanguageAverageConfidence)
				{
					string name = this.Language.Name;
					Average average;
					if (!TranscriptionData.perLanguageAverageConfidence.ContainsKey(name))
					{
						average = new Average();
						TranscriptionData.perLanguageAverageConfidence[name] = average;
					}
					else
					{
						average = TranscriptionData.perLanguageAverageConfidence[name];
					}
					Util.SetCounter(instance.AverageConfidence, average.Update((long)(this.Confidence * 100f)));
				}
				if ((double)this.Confidence <= LocConfig.Instance[this.Language].Transcription.LowConfidence)
				{
					Util.IncrementCounter(instance.VoiceMessagesProcessedWithLowConfidence);
				}
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_TranscriptionWordCounts, null, new object[]
				{
					this.totalWords,
					this.customWords,
					this.topNWords
				});
				UMEventNotificationHelper.PublishUMSuccessEventNotificationItem(ExchangeComponent.UMProtocol, UMNotificationEvent.UMTranscriptionThrottling.ToString());
				return;
			case RecoResultType.Skipped:
				if (RecoErrorType.MessageTooLong != this.RecognitionError && RecoErrorType.Throttled == this.RecognitionError)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_TranscriptionNotAttemptedDueToThrottling, null, new object[0]);
					Util.IncrementCounter(instance.VoiceMessagesNotProcessedBecauseOfLowAvailabilityOfResources);
					UMEventNotificationHelper.PublishUMFailureEventNotificationItem(ExchangeComponent.UMProtocol, UMNotificationEvent.UMTranscriptionThrottling.ToString());
				}
				Util.IncrementCounter(AvailabilityCounters.PercentageTranscriptionFailures);
				return;
			case RecoResultType.Partial:
				Util.IncrementCounter(AvailabilityCounters.PercentageTranscriptionFailures);
				Util.IncrementCounter(instance.VoiceMessagesPartiallyProcessedBecauseOfLowAvailabilityOfResources);
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_TranscriptionAttemptedButCancelled, null, new object[0]);
				UMEventNotificationHelper.PublishUMFailureEventNotificationItem(ExchangeComponent.UMProtocol, UMNotificationEvent.UMTranscriptionThrottling.ToString());
				return;
			default:
				return;
			}
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0004352C File Offset: 0x0004172C
		internal void UpdateStatistics(PipelineStatisticsLogger.PipelineStatisticsLogRow pipelineStatisticsLogRow)
		{
			pipelineStatisticsLogRow.TranscriptionLanguage = this.Language;
			pipelineStatisticsLogRow.TranscriptionResultType = this.RecognitionResult;
			pipelineStatisticsLogRow.TranscriptionErrorType = this.RecognitionError;
			pipelineStatisticsLogRow.TranscriptionConfidence = this.Confidence;
			pipelineStatisticsLogRow.TranscriptionTotalWords = this.totalWords;
			pipelineStatisticsLogRow.TranscriptionCustomWords = this.customWords;
			pipelineStatisticsLogRow.TranscriptionTopNWords = this.topNWords;
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x000435BC File Offset: 0x000417BC
		private void GenerateXML()
		{
			XmlDocument xmlDocument = new SafeXmlDocument();
			xmlDocument.PreserveWhitespace = true;
			xmlDocument.Schemas = VoiceMailPreviewSchema.SchemaSet;
			xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null));
			string namespaceURI = "http://schemas.microsoft.com/exchange/um/2010/evm";
			XmlElement xmlElement = xmlDocument.CreateElement("ASR", namespaceURI);
			xmlElement.SetAttribute("lang", this.Language.ToString());
			xmlElement.SetAttribute("confidence", Convert.ToString(this.Confidence, CultureInfo.InvariantCulture));
			xmlElement.SetAttribute("confidenceBand", this.ConfidenceBand.ToString().ToLowerInvariant());
			xmlElement.SetAttribute("recognitionResult", this.RecognitionResult.ToString().ToLowerInvariant());
			xmlElement.SetAttribute("recognitionError", this.RecognitionError.ToString().ToLowerInvariant());
			xmlElement.SetAttribute("schemaVersion", "1.0.0.0");
			xmlElement.SetAttribute("productVersion", Util.GetProductVersion());
			xmlElement.SetAttribute("productID", "925712");
			xmlElement.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
			xmlDocument.AppendChild(xmlElement);
			XmlElement xmlElement2 = xmlDocument.CreateElement("Information", namespaceURI);
			xmlElement2.SetAttribute("lang", this.Language.Name);
			xmlElement2.SetAttribute("linkText", Strings.LearnMore.ToString(this.Language));
			xmlElement2.SetAttribute("linkURL", "http://go.microsoft.com/fwlink/?LinkId=150048");
			xmlElement2.InnerText = Strings.InformationText.ToString(this.Language);
			xmlDocument.DocumentElement.AppendChild(xmlElement2);
			List<IUMRecognizedFeature>.Enumerator enumerator = this.recognizedFeatures.GetEnumerator();
			IUMRecognizedFeature iumrecognizedFeature = enumerator.MoveNext() ? enumerator.Current : null;
			XmlNode xmlNode = xmlDocument.DocumentElement;
			for (int i = 0; i < this.recognizedWords.Count; i++)
			{
				if (iumrecognizedFeature != null && iumrecognizedFeature.FirstWordIndex == i)
				{
					XmlElement xmlElement3 = xmlDocument.CreateElement("Feature", namespaceURI);
					xmlElement3.SetAttribute("class", iumrecognizedFeature.Name);
					if (iumrecognizedFeature.Name.Equals("Contact", StringComparison.OrdinalIgnoreCase))
					{
						StoreObjectId storeObjectId = StoreId.EwsIdToStoreObjectId(iumrecognizedFeature.Value);
						string value = Convert.ToBase64String(storeObjectId.ProviderLevelItemId);
						xmlElement3.SetAttribute("reference", value);
						xmlElement3.SetAttribute("reference2", iumrecognizedFeature.Value);
					}
					else
					{
						xmlElement3.SetAttribute("reference", iumrecognizedFeature.Value);
					}
					xmlDocument.DocumentElement.AppendChild(xmlElement3);
					xmlNode = xmlElement3;
				}
				IUMRecognizedWord iumrecognizedWord = this.recognizedWords[i];
				bool flag = i >= this.recognizedWords.Count - 1;
				bool flag2 = !flag && this.recognizedSentences.Get(i + 1);
				bool flag3 = !flag && this.recognizedParagraphs.Get(i + 1);
				bool flag4 = flag2 || flag3;
				string text = null;
				if (flag4)
				{
					text = string.Format(CultureInfo.InvariantCulture, "n{0}.5", new object[]
					{
						i
					});
				}
				else if (!flag)
				{
					text = string.Format(CultureInfo.InvariantCulture, "n{0}", new object[]
					{
						i + 1
					});
				}
				XmlElement xmlElement4 = xmlDocument.CreateElement("Text", namespaceURI);
				xmlElement4.SetAttribute("id", string.Format(CultureInfo.InvariantCulture, "n{0}", new object[]
				{
					i
				}));
				if (text != null)
				{
					xmlElement4.SetAttribute("nx", text);
				}
				xmlElement4.SetAttribute("c", Convert.ToString(iumrecognizedWord.Confidence, CultureInfo.InvariantCulture));
				xmlElement4.SetAttribute("ts", iumrecognizedWord.AudioPosition.ToString());
				xmlElement4.SetAttribute("te", (iumrecognizedWord.AudioPosition + iumrecognizedWord.AudioDuration).ToString());
				xmlElement4.SetAttribute("be", "1");
				xmlElement4.InnerText = iumrecognizedWord.Text + this.GetTrailingSpaces(i);
				xmlNode.AppendChild(xmlElement4);
				if (iumrecognizedFeature != null && iumrecognizedFeature.FirstWordIndex + iumrecognizedFeature.CountOfWords == i + 1)
				{
					xmlNode = xmlDocument.DocumentElement;
					iumrecognizedFeature = (enumerator.MoveNext() ? enumerator.Current : null);
				}
				if (flag4)
				{
					string value2 = flag3 ? "high" : "low";
					XmlElement xmlElement5 = xmlDocument.CreateElement("Break", namespaceURI);
					xmlElement5.SetAttribute("id", text);
					if (!flag)
					{
						xmlElement5.SetAttribute("nx", string.Format(CultureInfo.InvariantCulture, "n{0}", new object[]
						{
							i + 1
						}));
					}
					xmlElement5.SetAttribute("c", "1");
					xmlElement5.SetAttribute("ts", (iumrecognizedWord.AudioPosition + iumrecognizedWord.AudioDuration).ToString());
					xmlElement5.SetAttribute("te", (iumrecognizedWord.AudioPosition + iumrecognizedWord.AudioDuration).ToString());
					xmlElement5.SetAttribute("wt", value2);
					xmlElement5.SetAttribute("be", "1");
					xmlNode.AppendChild(xmlElement5);
				}
			}
			enumerator.Dispose();
			xmlDocument.Validate(delegate(object sender, ValidationEventArgs e)
			{
				string formatString = "Invalid XML generated for EVM. If you hit this it means we're generating XML that is not per our spec. We have external partners that depend on this spec, so please make sure you know what you are doing when trying to fix issue. Validation error = {0}";
				ExAssert.RetailAssert(false, formatString, new object[]
				{
					e.Message
				});
			});
			this.TranscriptionXml = xmlDocument;
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00043B6C File Offset: 0x00041D6C
		private void ReformatText()
		{
			int num = 0;
			int num2 = this.config.FirstSentenceInNewLine ? 1 : 0;
			TimeSpan t = TimeSpan.Zero;
			TimeSpan t2 = TimeSpan.Zero;
			int i = 0;
			int num3 = 0;
			IUMRecognizedFeature iumrecognizedFeature = (num3 < this.recognizedFeatures.Count) ? this.recognizedFeatures[num3] : null;
			while (i < this.recognizedWords.Count)
			{
				IUMRecognizedWord iumrecognizedWord = this.recognizedWords[i];
				TimeSpan t3 = iumrecognizedWord.AudioPosition - (t + t2);
				if (t3 > this.config.SilenceThreshold && i > 0 && !this.recognizedParagraphs.Get(i))
				{
					num++;
					this.recognizedSentences.Set(i, true);
					if (num - num2 % 3 == 0)
					{
						this.recognizedParagraphs.Set(i, true);
					}
				}
				int num4 = 1;
				if (iumrecognizedFeature != null && i == iumrecognizedFeature.FirstWordIndex)
				{
					num4 = Math.Max(1, iumrecognizedFeature.CountOfWords);
					iumrecognizedFeature = ((++num3 < this.recognizedFeatures.Count) ? this.recognizedFeatures[num3] : null);
				}
				i += num4;
				int num5 = i - 1;
				if (num5 >= 0 && num5 < this.recognizedWords.Count)
				{
					t = this.recognizedWords[num5].AudioPosition;
					t2 = this.recognizedWords[num5].AudioDuration;
				}
			}
			this.AddLastSentenceParagraph();
			for (int j = 0; j < this.recognizedWords.Count; j++)
			{
				if (this.recognizedParagraphs.Get(j))
				{
					this.recognizedSentences.Set(j, false);
					this.CapitalizeNextVisible(j);
					this.RemoveTrailingSpaceFromPreviousVisible(j);
				}
				if (this.recognizedSentences.Get(j))
				{
					if (this.config.CapStartOfNewSentence)
					{
						this.CapitalizeNextVisible(j);
					}
					this.RemoveTrailingSpaceFromPreviousVisible(j);
				}
			}
			this.CapitalizeNextVisible(0);
			this.RemoveTrailingSpaceFromPreviousVisible(this.recognizedWords.Count);
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00043D6C File Offset: 0x00041F6C
		private void AddLastSentenceParagraph()
		{
			if (this.config.LastSentenceInNewLine && this.recognizedSentences.Count > 1)
			{
				for (int i = this.recognizedSentences.Length - 1; i >= 0; i--)
				{
					if (this.recognizedSentences.Get(i))
					{
						this.recognizedParagraphs.Set(i, true);
						return;
					}
				}
			}
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x00043DC8 File Offset: 0x00041FC8
		private void CapitalizeNextVisible(int i)
		{
			i = Math.Max(0, i);
			for (int j = i; j < this.recognizedWords.Count; j++)
			{
				IUMRecognizedWord iumrecognizedWord = this.recognizedWords[j];
				iumrecognizedWord.Text = this.Language.TextInfo.ToTitleCase(iumrecognizedWord.Text);
				if (iumrecognizedWord.Text != null && !string.IsNullOrEmpty(iumrecognizedWord.Text.Trim()))
				{
					return;
				}
			}
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x00043E38 File Offset: 0x00042038
		private void RemoveTrailingSpaceFromPreviousVisible(int i)
		{
			i = Math.Min(this.recognizedWords.Count, i);
			for (int j = i - 1; j > 0; j--)
			{
				IUMRecognizedWord iumrecognizedWord = this.recognizedWords[j];
				iumrecognizedWord.DisplayAttributes = UMDisplayAttributes.None;
				if (iumrecognizedWord.Text != null && !string.IsNullOrEmpty(iumrecognizedWord.Text.Trim()))
				{
					return;
				}
			}
		}

		// Token: 0x04000B21 RID: 2849
		private static Dictionary<string, Average> perLanguageAverageConfidence = new Dictionary<string, Average>();

		// Token: 0x04000B22 RID: 2850
		private List<IUMRecognizedWord> recognizedWords = new List<IUMRecognizedWord>();

		// Token: 0x04000B23 RID: 2851
		private List<IUMRecognizedFeature> recognizedFeatures = new List<IUMRecognizedFeature>();

		// Token: 0x04000B24 RID: 2852
		private BitArray recognizedParagraphs;

		// Token: 0x04000B25 RID: 2853
		private BitArray recognizedSentences;

		// Token: 0x04000B26 RID: 2854
		private int totalWords;

		// Token: 0x04000B27 RID: 2855
		private int customWords;

		// Token: 0x04000B28 RID: 2856
		private int topNWords;

		// Token: 0x04000B29 RID: 2857
		private LocConfig.TranscriptionConfig config;
	}
}
