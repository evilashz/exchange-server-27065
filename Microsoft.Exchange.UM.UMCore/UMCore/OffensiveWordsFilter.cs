using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.Prompts.Config;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000195 RID: 405
	internal class OffensiveWordsFilter
	{
		// Token: 0x06000BF8 RID: 3064 RVA: 0x00033F84 File Offset: 0x00032184
		private OffensiveWordsFilter(CultureInfo transcriptionLanguage) : this(transcriptionLanguage, Strings.OffensiveWordsList.ToString(transcriptionLanguage))
		{
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00033F98 File Offset: 0x00032198
		public OffensiveWordsFilter(CultureInfo transcriptionLanguage, string offensiveWordsString)
		{
			this.offensiveWords = new HashSet<string>(StringComparer.Create(transcriptionLanguage, true));
			string[] array = offensiveWordsString.Split(new char[]
			{
				','
			});
			foreach (string item in array)
			{
				this.offensiveWords.Add(item);
			}
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00033FF7 File Offset: 0x000321F7
		public static bool TryGet(CultureInfo transcriptionLanguage, out OffensiveWordsFilter offensiveWordsFilter)
		{
			return OffensiveWordsFilter.instances.TryGetValue(transcriptionLanguage, out offensiveWordsFilter);
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x00034008 File Offset: 0x00032208
		public static void Init()
		{
			foreach (CultureInfo cultureInfo in Platform.Utilities.SupportedTranscriptionLanguages)
			{
				OffensiveWordsFilter.instances.Add(cultureInfo, new OffensiveWordsFilter(cultureInfo));
			}
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x00034064 File Offset: 0x00032264
		public List<KeyValuePair<string, int>> Filter(List<KeyValuePair<string, int>> rawList)
		{
			List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>(rawList.Count);
			foreach (KeyValuePair<string, int> item in rawList)
			{
				if (this.offensiveWords.Contains(item.Key))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "OffensiveWordsFilter::Filter filtering out offensive word '{0}'", new object[]
					{
						item.Key
					});
				}
				else
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x04000A07 RID: 2567
		private const char WordSeparator = ',';

		// Token: 0x04000A08 RID: 2568
		private static readonly Dictionary<CultureInfo, OffensiveWordsFilter> instances = new Dictionary<CultureInfo, OffensiveWordsFilter>();

		// Token: 0x04000A09 RID: 2569
		private readonly HashSet<string> offensiveWords;
	}
}
