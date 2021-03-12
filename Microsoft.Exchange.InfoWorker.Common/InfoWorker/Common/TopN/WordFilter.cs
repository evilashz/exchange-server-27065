using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;

namespace Microsoft.Exchange.InfoWorker.Common.TopN
{
	// Token: 0x0200027D RID: 637
	internal class WordFilter
	{
		// Token: 0x06001246 RID: 4678 RVA: 0x00055254 File Offset: 0x00053454
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "WordFilter for locale id " + this.localeId + ". ";
			}
			return this.toString;
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x00055284 File Offset: 0x00053484
		internal int LocaleId
		{
			get
			{
				return this.localeId;
			}
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0005528C File Offset: 0x0005348C
		internal WordFilter(int localeId)
		{
			this.localeId = localeId;
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x0005529C File Offset: 0x0005349C
		internal List<string> Filter(List<string> wordList)
		{
			List<string> list = new List<string>(10);
			foreach (string text in wordList)
			{
				if (!string.IsNullOrEmpty(text) && text.Length <= 20)
				{
					bool flag = false;
					bool flag2 = false;
					foreach (char c in text)
					{
						if (!char.IsLetter(c))
						{
							flag = true;
							break;
						}
						if (!flag2 && (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u' || c == 'y'))
						{
							flag2 = true;
						}
					}
					if (!flag && flag2)
					{
						list.Add(text);
					}
				}
			}
			return list;
		}

		// Token: 0x04000BFD RID: 3069
		private int localeId;

		// Token: 0x04000BFE RID: 3070
		private string toString;

		// Token: 0x04000BFF RID: 3071
		protected static readonly Trace Tracer = ExTraceGlobals.TopNTracer;
	}
}
