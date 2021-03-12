using System;
using Microsoft.Exchange.Clients.Owa2.Server.Core;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000433 RID: 1075
	internal interface IClientWatsonParameters
	{
		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x060024AC RID: 9388
		IConsolidationSymbolsMap ConsolidationSymbolsMap { get; }

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x060024AD RID: 9389
		IJavaScriptSymbolsMap<AjaxMinSymbolForJavaScript> MinificationSymbolsMapForJavaScript { get; }

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x060024AE RID: 9390
		IJavaScriptSymbolsMap<AjaxMinSymbolForScriptSharp> MinificationSymbolsMapForScriptSharp { get; }

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x060024AF RID: 9391
		IJavaScriptSymbolsMap<ScriptSharpSymbolWrapper> ObfuscationSymbolsMap { get; }

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x060024B0 RID: 9392
		SendClientWatsonReportAction ReportAction { get; }

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x060024B1 RID: 9393
		string OwaVersion { get; }

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x060024B2 RID: 9394
		string ExchangeSourcesPath { get; }

		// Token: 0x060024B3 RID: 9395
		bool IsErrorOverReportQuota(int hashCode);
	}
}
