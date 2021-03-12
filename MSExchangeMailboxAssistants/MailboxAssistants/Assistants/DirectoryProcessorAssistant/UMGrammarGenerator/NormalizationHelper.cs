using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Speech.Recognition.SrgsGrammar;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.UMGrammarGenerator
{
	// Token: 0x020001BF RID: 447
	internal class NormalizationHelper
	{
		// Token: 0x06001159 RID: 4441 RVA: 0x00065A68 File Offset: 0x00063C68
		public static List<string> GetNormalizedNames(List<string> inputNames, NameNormalizer nameNormalizer, RecipientType recipientType, GrammarGenerationLog generationLog)
		{
			UMTracer.DebugTrace("NormalizationHelper.GetNormalizedNames", new object[0]);
			List<string> list = new List<string>();
			int i = 0;
			while (i < inputNames.Count)
			{
				string text = inputNames[i];
				UMTracer.DebugTrace("NormalizationHelper.GetNormalizedName inputName='{0}', recipientType={1}", new object[]
				{
					text,
					recipientType
				});
				string input = GrammarRecipientHelper.CharacterMapReplaceString(text);
				Dictionary<string, bool> dictionary = GrammarRecipientHelper.ApplyExclusionList(input, recipientType);
				if (dictionary.Count != 0)
				{
					using (Dictionary<string, bool>.KeyCollection.Enumerator enumerator = dictionary.Keys.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text2 = enumerator.Current;
							if (!string.IsNullOrEmpty(text2) && NormalizationHelper.IsGrammarEntryFormatValid(text2))
							{
								bool flag = false;
								string text3 = SpeechUtils.SrgsEncode(text2);
								if (dictionary[text2])
								{
									if (nameNormalizer.IsNameAcceptable(text3))
									{
										flag = true;
									}
									else
									{
										generationLog.WriteLine(Strings.NormalizationFailedError(text));
									}
								}
								else
								{
									flag = true;
								}
								if (flag && !string.IsNullOrEmpty(text3))
								{
									UMTracer.DebugTrace("Adding name '{0}'", new object[]
									{
										text3
									});
									list.Add(text3);
								}
							}
						}
						goto IL_12D;
					}
					goto IL_11C;
				}
				goto IL_11C;
				IL_12D:
				i++;
				continue;
				IL_11C:
				generationLog.WriteLine(Strings.EntryExcludedFromGrammar(text));
				goto IL_12D;
			}
			if (list.Count != 0)
			{
				return list;
			}
			return null;
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x00065BD0 File Offset: 0x00063DD0
		private static bool IsGrammarEntryFormatValid(string wordToCheck)
		{
			bool result;
			try
			{
				new SrgsText(wordToCheck);
				result = true;
			}
			catch (FormatException)
			{
				result = false;
			}
			return result;
		}
	}
}
