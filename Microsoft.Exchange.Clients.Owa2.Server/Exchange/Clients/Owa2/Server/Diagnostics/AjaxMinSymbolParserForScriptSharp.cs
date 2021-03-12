using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x0200042C RID: 1068
	internal sealed class AjaxMinSymbolParserForScriptSharp : AjaxMinSymbolParser<AjaxMinSymbolForScriptSharp>
	{
		// Token: 0x06002473 RID: 9331 RVA: 0x000836F8 File Offset: 0x000818F8
		protected override AjaxMinSymbolForScriptSharp ParseSymbolData(string[] columns, ClientWatsonFunctionNamePool functionNamePool)
		{
			return new AjaxMinSymbolForScriptSharp
			{
				ScriptStartLine = int.Parse(columns[0]),
				ScriptStartColumn = int.Parse(columns[1]),
				ScriptEndLine = int.Parse(columns[2]),
				ScriptEndColumn = int.Parse(columns[3]),
				SourceStartPosition = int.Parse(columns[4]),
				SourceEndPosition = int.Parse(columns[5]),
				SourceFileId = uint.Parse(columns[10])
			};
		}
	}
}
