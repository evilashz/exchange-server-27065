using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x0200042B RID: 1067
	internal sealed class AjaxMinSymbolParserForJavaScript : AjaxMinSymbolParser<AjaxMinSymbolForJavaScript>
	{
		// Token: 0x06002471 RID: 9329 RVA: 0x00083630 File Offset: 0x00081830
		protected override AjaxMinSymbolForJavaScript ParseSymbolData(string[] columns, ClientWatsonFunctionNamePool functionNamePool)
		{
			string text = columns[12];
			if (string.IsNullOrEmpty(text))
			{
				text = "GLOBAL";
			}
			return new AjaxMinSymbolForJavaScript
			{
				ScriptStartLine = int.Parse(columns[0]),
				ScriptStartColumn = int.Parse(columns[1]),
				ScriptEndLine = int.Parse(columns[2]),
				ScriptEndColumn = int.Parse(columns[3]),
				SourceStartLine = int.Parse(columns[6]),
				SourceStartColumn = int.Parse(columns[7]),
				SourceEndLine = int.Parse(columns[8]),
				SourceEndColumn = int.Parse(columns[9]),
				SourceFileId = uint.Parse(columns[10]),
				FunctionNameIndex = functionNamePool.GetOrAddFunctionNameIndex(text)
			};
		}
	}
}
