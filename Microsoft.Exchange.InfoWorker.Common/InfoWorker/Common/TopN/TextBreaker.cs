using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;

namespace Microsoft.Exchange.InfoWorker.Common.TopN
{
	// Token: 0x0200027A RID: 634
	internal class TextBreaker
	{
		// Token: 0x06001224 RID: 4644 RVA: 0x000548A9 File Offset: 0x00052AA9
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "TextBreaker for locale id " + this.localeId + ". ";
			}
			return this.toString;
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x000548D9 File Offset: 0x00052AD9
		internal int LocaleId
		{
			get
			{
				return this.localeId;
			}
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x000548E1 File Offset: 0x00052AE1
		internal TextBreaker(int localeId)
		{
			this.localeId = localeId;
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x000548F0 File Offset: 0x00052AF0
		internal List<string> BreakText(string text)
		{
			List<string> list = new List<string>(10);
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			int num = 0;
			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];
				bool flag = this.IsSeparatorChar(c);
				bool flag2 = i == text.Length - 1;
				if (flag || flag2)
				{
					int num2 = 0;
					if (flag)
					{
						num2 = i - num;
					}
					else if (i != 8191)
					{
						num2 = i - num + 1;
					}
					if (num2 > 0)
					{
						string text2 = text.Substring(num, num2);
						list.Add(text2.ToLower());
					}
					num = i + 1;
				}
			}
			return list;
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00054987 File Offset: 0x00052B87
		internal bool IsSeparatorChar(char c)
		{
			return char.IsSeparator(c) || char.IsSymbol(c) || char.IsPunctuation(c) || char.IsControl(c) || char.IsWhiteSpace(c);
		}

		// Token: 0x04000BE4 RID: 3044
		internal const int MaxTextSize = 8192;

		// Token: 0x04000BE5 RID: 3045
		private int localeId;

		// Token: 0x04000BE6 RID: 3046
		private string toString;

		// Token: 0x04000BE7 RID: 3047
		protected static readonly Trace Tracer = ExTraceGlobals.TopNTracer;
	}
}
