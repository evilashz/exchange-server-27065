using System;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Transport.Probes
{
	// Token: 0x0200027A RID: 634
	internal class Tracer : ITracer
	{
		// Token: 0x060014E1 RID: 5345 RVA: 0x0003F0AC File Offset: 0x0003D2AC
		public void TraceDebug(string debugInfo)
		{
			WTFDiagnostics.TraceDebug(ExTraceGlobals.MapiSubmitLAMTracer, new TracingContext(), debugInfo, null, "TraceDebug", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Transport\\MailboxTransport\\Tracer.cs", 56);
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0003F0CB File Offset: 0x0003D2CB
		public void TraceError(string errorInfo)
		{
			WTFDiagnostics.TraceError(ExTraceGlobals.MapiSubmitLAMTracer, new TracingContext(), errorInfo, null, "TraceError", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Transport\\MailboxTransport\\Tracer.cs", 65);
		}
	}
}
