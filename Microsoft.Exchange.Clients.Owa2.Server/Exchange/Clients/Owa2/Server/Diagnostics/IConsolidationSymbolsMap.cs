using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000437 RID: 1079
	internal interface IConsolidationSymbolsMap
	{
		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x060024C8 RID: 9416
		// (set) Token: 0x060024C9 RID: 9417
		bool SkipChecksumValidation { get; set; }

		// Token: 0x060024CA RID: 9418
		bool Search(string scriptName, int line, int column, out string sourceFile, out Tuple<int, int> preConsolidationPosition);

		// Token: 0x060024CB RID: 9419
		bool HasSymbolsLoadedForScript(string scriptName);
	}
}
