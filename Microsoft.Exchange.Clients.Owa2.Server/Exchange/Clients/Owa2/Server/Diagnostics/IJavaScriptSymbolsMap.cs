using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000446 RID: 1094
	public interface IJavaScriptSymbolsMap<T> where T : IJavaScriptSymbol
	{
		// Token: 0x060024EF RID: 9455
		bool Search(string scriptName, T javaScriptSymbol, out T symbolFound);

		// Token: 0x060024F0 RID: 9456
		string GetSourceFilePathFromId(uint id);

		// Token: 0x060024F1 RID: 9457
		string GetFunctionName(int index);

		// Token: 0x060024F2 RID: 9458
		bool HasSymbolsLoadedForScript(string scriptName);
	}
}
