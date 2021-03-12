using System;
using Microsoft.Exchange.HttpProxy.Routing;

namespace Microsoft.Exchange.HttpProxy.RouteSelector
{
	// Token: 0x02000004 RID: 4
	internal interface IRouteSelectorModuleDiagnostics : IRouteSelectorDiagnostics, IRoutingDiagnostics
	{
		// Token: 0x06000008 RID: 8
		void SetTargetServer(string value);

		// Token: 0x06000009 RID: 9
		void SetTargetServerVersion(string value);

		// Token: 0x0600000A RID: 10
		void SaveRoutingLatency(Action operationToTrack);

		// Token: 0x0600000B RID: 11
		void LogLatencies();

		// Token: 0x0600000C RID: 12
		void ProcessLatencyPerfCounters();
	}
}
