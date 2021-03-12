using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.FaultInjection;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Text;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.UMGrammarGenerator
{
	// Token: 0x020001C1 RID: 449
	internal static class SpeechRecognizerInfo
	{
		// Token: 0x0600115F RID: 4447 RVA: 0x00065C40 File Offset: 0x00063E40
		static SpeechRecognizerInfo()
		{
			SpeechRecognizerInfo.InitializeInstalledRecognizers();
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00065CAC File Offset: 0x00063EAC
		public static string GetRecognizerId(CultureInfo culture)
		{
			ValidateArgument.NotNull(culture, "culture");
			UMTracer.DebugTrace("Entering SpeechRecognizerInfo.GetRecognizerId - Culture='{0}'", new object[]
			{
				culture
			});
			string result;
			try
			{
				result = SpeechRecognizerInfo.installedSpeechRecognizers[culture];
			}
			catch (KeyNotFoundException)
			{
				UMTracer.DebugTrace("SpeechRecognizerInfo.GetRecognizerId - Culture='{0}' was not found", new object[]
				{
					culture
				});
				result = null;
			}
			return result;
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00065D14 File Offset: 0x00063F14
		public static bool TextNormalizationRequiresSpecialHandling(CultureInfo c)
		{
			return SpeechRecognizerInfo.specialCulturesDictionary.ContainsKey(c.Name);
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x00065D28 File Offset: 0x00063F28
		public static IList<string> GetNormalizedForms(SpeechRecognitionEngine engine, string word)
		{
			UMTracer.DebugTrace("Entering SpeechRecognizerInfo.GetNormalizedForms - word='{0}'", new object[]
			{
				word
			});
			FaultInjectionUtils.FaultInjectException(2502307133U);
			NormalizedResult normalizedResult = engine.NormalizerCollection.Normalize(word);
			ReadOnlyCollection<NormalizedResultAlternate> alternates = normalizedResult.GetAlternates();
			List<string> list = new List<string>(5);
			foreach (NormalizedResultAlternate normalizedResultAlternate in alternates)
			{
				UMTracer.DebugTrace("SpeechRecognizerInfo.GetNormalizedForms - word='{0}', alternate='{1}'", new object[]
				{
					word,
					normalizedResultAlternate.NormalizedString
				});
				list.Add(normalizedResultAlternate.NormalizedString);
			}
			return list;
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x00065DDC File Offset: 0x00063FDC
		private static void InitializeInstalledRecognizers()
		{
			if (SpeechRecognizerInfo.installedSpeechRecognizers == null)
			{
				lock (SpeechRecognizerInfo.staticLock)
				{
					if (SpeechRecognizerInfo.installedSpeechRecognizers == null)
					{
						ICollection<RecognizerInfo> collection = SpeechRecognitionEngine.InstalledRecognizers();
						SpeechRecognizerInfo.installedSpeechRecognizers = new Dictionary<CultureInfo, string>(collection.Count);
						foreach (RecognizerInfo recognizerInfo in collection)
						{
							if (recognizerInfo.AdditionalInfo.Keys.Contains("Telephony") && !recognizerInfo.AdditionalInfo.Keys.Contains("Transcription"))
							{
								UMTracer.DebugTrace("SpeechRecognizerInfo - Adding recognizer culture='{0}', id='{1}'", new object[]
								{
									recognizerInfo.Culture,
									recognizerInfo.Id
								});
								SpeechRecognizerInfo.installedSpeechRecognizers.Add(recognizerInfo.Culture, recognizerInfo.Id);
							}
						}
					}
				}
			}
		}

		// Token: 0x04000ADD RID: 2781
		private static readonly Dictionary<string, bool> specialCulturesDictionary = new Dictionary<string, bool>(5, StringComparer.OrdinalIgnoreCase)
		{
			{
				"ca-ES",
				true
			},
			{
				"it-IT",
				true
			},
			{
				"nl-NL",
				true
			},
			{
				"pt-BR",
				true
			},
			{
				"pt-PT",
				true
			}
		};

		// Token: 0x04000ADE RID: 2782
		private static object staticLock = new object();

		// Token: 0x04000ADF RID: 2783
		private static Dictionary<CultureInfo, string> installedSpeechRecognizers;
	}
}
