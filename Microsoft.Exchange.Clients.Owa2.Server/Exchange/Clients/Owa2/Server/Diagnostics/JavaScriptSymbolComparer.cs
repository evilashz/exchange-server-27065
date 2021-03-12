using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000447 RID: 1095
	public sealed class JavaScriptSymbolComparer<T> : IComparer<T> where T : IJavaScriptSymbol
	{
		// Token: 0x060024F3 RID: 9459 RVA: 0x00085D73 File Offset: 0x00083F73
		private JavaScriptSymbolComparer()
		{
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x060024F4 RID: 9460 RVA: 0x00085D7B File Offset: 0x00083F7B
		public static JavaScriptSymbolComparer<T> Instance
		{
			get
			{
				return JavaScriptSymbolComparer<T>.instance;
			}
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x00085D84 File Offset: 0x00083F84
		public int Compare(T x, T y)
		{
			if (x.ScriptEndLine > y.ScriptEndLine)
			{
				return 1;
			}
			if (x.ScriptEndLine < y.ScriptEndLine)
			{
				return -1;
			}
			if (x.ScriptEndColumn > y.ScriptEndColumn)
			{
				return 1;
			}
			if (x.ScriptEndColumn < y.ScriptEndColumn)
			{
				return -1;
			}
			if (x.ScriptStartLine > y.ScriptStartLine)
			{
				return -1;
			}
			if (x.ScriptStartLine < y.ScriptStartLine)
			{
				return 1;
			}
			if (x.ScriptStartColumn > y.ScriptStartColumn)
			{
				return -1;
			}
			if (x.ScriptStartColumn < y.ScriptStartColumn)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x040014DE RID: 5342
		private const int Less = -1;

		// Token: 0x040014DF RID: 5343
		private const int Greater = 1;

		// Token: 0x040014E0 RID: 5344
		private const int Equal = 0;

		// Token: 0x040014E1 RID: 5345
		private static readonly JavaScriptSymbolComparer<T> instance = new JavaScriptSymbolComparer<T>();
	}
}
