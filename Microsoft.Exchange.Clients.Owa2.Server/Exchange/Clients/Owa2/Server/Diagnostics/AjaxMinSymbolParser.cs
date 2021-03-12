using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000429 RID: 1065
	internal abstract class AjaxMinSymbolParser<T> : ClientWatsonSymbolParser<T> where T : IJavaScriptSymbol
	{
		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x0600246D RID: 9325 RVA: 0x00083514 File Offset: 0x00081714
		private int AjaxMinFieldCount
		{
			get
			{
				if (this.ajaxMinFieldCount < 0)
				{
					this.ajaxMinFieldCount = Enum.GetNames(typeof(AjaxMinSymbolParser<T>.AjaxMinSymbolData)).Length;
				}
				return this.ajaxMinFieldCount;
			}
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x0008353C File Offset: 0x0008173C
		public override bool TryParseSymbolData(string value, ClientWatsonFunctionNamePool functionNamePool, out T javaScriptSymbol)
		{
			javaScriptSymbol = default(T);
			if (value == null)
			{
				return false;
			}
			string[] array = value.Split(new char[]
			{
				','
			}, StringSplitOptions.None);
			if (array.Length != this.AjaxMinFieldCount)
			{
				return false;
			}
			bool result;
			try
			{
				string item = array[11];
				if (this.symbolTypesToSkip.Contains(item))
				{
					result = false;
				}
				else
				{
					javaScriptSymbol = this.ParseSymbolData(array, functionNamePool);
					result = true;
				}
			}
			catch (ArgumentNullException)
			{
				result = false;
			}
			catch (OverflowException)
			{
				result = false;
			}
			catch (FormatException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600246F RID: 9327
		protected abstract T ParseSymbolData(string[] columns, ClientWatsonFunctionNamePool functionNamePool);

		// Token: 0x040013D8 RID: 5080
		public const string AjaxMinMapFileSuffix = "*_minify.xml";

		// Token: 0x040013D9 RID: 5081
		public const string AjaxMinMapFilePattern = "**_minify.xml";

		// Token: 0x040013DA RID: 5082
		private readonly HashSet<string> symbolTypesToSkip = new HashSet<string>
		{
			"ThisLiteral",
			"Member",
			"Lookup",
			"ConstantWrapper"
		};

		// Token: 0x040013DB RID: 5083
		private int ajaxMinFieldCount = -1;

		// Token: 0x0200042A RID: 1066
		protected enum AjaxMinSymbolData
		{
			// Token: 0x040013DD RID: 5085
			ScriptStartLine,
			// Token: 0x040013DE RID: 5086
			ScriptStartColumn,
			// Token: 0x040013DF RID: 5087
			ScriptEndLine,
			// Token: 0x040013E0 RID: 5088
			ScriptEndColumn,
			// Token: 0x040013E1 RID: 5089
			SourceStartPosition,
			// Token: 0x040013E2 RID: 5090
			SourceEndPosition,
			// Token: 0x040013E3 RID: 5091
			SourceStartLine,
			// Token: 0x040013E4 RID: 5092
			SourceStartColumn,
			// Token: 0x040013E5 RID: 5093
			SourceEndLine,
			// Token: 0x040013E6 RID: 5094
			SourceEndColumn,
			// Token: 0x040013E7 RID: 5095
			SourceFileId,
			// Token: 0x040013E8 RID: 5096
			SymbolType,
			// Token: 0x040013E9 RID: 5097
			ParentFunction
		}
	}
}
