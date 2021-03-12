using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Exchange.TextProcessing;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000013 RID: 19
	internal class MultiMatcher : IMatch
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00002F7C File Offset: 0x0000117C
		internal MultiMatcher()
		{
			this.matchers = new List<IMatch>();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002F8F File Offset: 0x0000118F
		internal void Add(IMatch matcher)
		{
			this.matchers.Add(matcher);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002F9D File Offset: 0x0000119D
		internal void Clear()
		{
			this.matchers.Clear();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002FC0 File Offset: 0x000011C0
		public bool IsMatch(TextScanContext data)
		{
			return this.matchers.Any((IMatch matcher) => matcher.IsMatch(data));
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002FF4 File Offset: 0x000011F4
		public bool IsMatch(string text, string textId, RulesEvaluationContext rulesEvaluationContext)
		{
			TextScanContext cachedScanSession = MultiMatcher.GetCachedScanSession(text, textId, rulesEvaluationContext);
			return this.IsMatch(cachedScanSession);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003014 File Offset: 0x00001214
		public bool IsMatch(TextReader reader, string streamId, int maxStreamLength, RulesEvaluationContext rulesEvaluationContext)
		{
			string text;
			if (maxStreamLength > 0)
			{
				char[] array = new char[maxStreamLength];
				int length = reader.Read(array, 0, array.Length);
				text = new string(array, 0, length);
			}
			else
			{
				text = reader.ReadToEnd();
			}
			TextScanContext cachedScanSession = MultiMatcher.GetCachedScanSession(text, streamId, rulesEvaluationContext);
			return this.IsMatch(cachedScanSession);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000305C File Offset: 0x0000125C
		private static TextScanContext GetCachedScanSession(string text, string textId, RulesEvaluationContext rulesEvaluationContext)
		{
			TextScanContext textScanContext = null;
			bool flag = rulesEvaluationContext != null && !string.IsNullOrEmpty(textId);
			if (flag)
			{
				rulesEvaluationContext.RegexMatcherCache.TryGetValue(textId, out textScanContext);
			}
			if (textScanContext == null)
			{
				textScanContext = new TextScanContext(text);
				if (flag)
				{
					rulesEvaluationContext.AddTextProcessingContext(textId, textScanContext);
				}
			}
			return textScanContext;
		}

		// Token: 0x04000023 RID: 35
		private List<IMatch> matchers;
	}
}
