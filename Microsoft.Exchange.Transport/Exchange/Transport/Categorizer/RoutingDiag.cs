using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000259 RID: 601
	internal static class RoutingDiag
	{
		// Token: 0x04000C69 RID: 3177
		public static readonly ExEventLog EventLogger = new ExEventLog(ExTraceGlobals.RoutingTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x04000C6A RID: 3178
		public static readonly Trace Tracer = ExTraceGlobals.RoutingTracer;

		// Token: 0x04000C6B RID: 3179
		public static readonly SystemProbeTrace SystemProbeTracer = new SystemProbeTrace(ExTraceGlobals.RoutingTracer, "Routing");
	}
}
