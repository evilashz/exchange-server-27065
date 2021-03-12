using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001FC RID: 508
	internal class TopNWordsCustomGrammar : CustomGrammarBase
	{
		// Token: 0x06000ED7 RID: 3799 RVA: 0x00042CD8 File Offset: 0x00040ED8
		internal TopNWordsCustomGrammar(CultureInfo transcriptionLanguage, List<KeyValuePair<string, int>> filteredWords) : this(transcriptionLanguage, filteredWords, AppConfig.Instance.Service.TopNGrammarThreshold)
		{
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x00042CF1 File Offset: 0x00040EF1
		internal TopNWordsCustomGrammar(CultureInfo transcriptionLanguage, List<KeyValuePair<string, int>> filteredWords, int threshold) : base(transcriptionLanguage)
		{
			this.filteredWords = filteredWords;
			this.threshold = threshold;
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x00042D08 File Offset: 0x00040F08
		internal override string FileName
		{
			get
			{
				return "ExtTopN.grxml";
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x00042D0F File Offset: 0x00040F0F
		internal override string Rule
		{
			get
			{
				return "ExtTopN";
			}
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x00042D18 File Offset: 0x00040F18
		protected override List<GrammarItemBase> GetItems()
		{
			int num = 0;
			List<GrammarItemBase> list = new List<GrammarItemBase>(this.filteredWords.Count);
			foreach (KeyValuePair<string, int> keyValuePair in this.filteredWords)
			{
				if (keyValuePair.Value >= this.threshold)
				{
					num += keyValuePair.Value;
				}
			}
			foreach (KeyValuePair<string, int> keyValuePair2 in this.filteredWords)
			{
				if (keyValuePair2.Value >= this.threshold)
				{
					GrammarItem item = new GrammarItem(keyValuePair2.Key, TopNWordsCustomGrammar.topNWordTrueTag, base.TranscriptionLanguage, (float)keyValuePair2.Value / (float)num);
					list.Add(item);
				}
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "TopN grammar adding {0} words that exceeded threshold {1}", new object[]
			{
				list.Count,
				this.threshold
			});
			return list;
		}

		// Token: 0x04000B1E RID: 2846
		private static readonly string topNWordTrueTag = "out.topNWords=true;";

		// Token: 0x04000B1F RID: 2847
		private readonly List<KeyValuePair<string, int>> filteredWords;

		// Token: 0x04000B20 RID: 2848
		private readonly int threshold;
	}
}
