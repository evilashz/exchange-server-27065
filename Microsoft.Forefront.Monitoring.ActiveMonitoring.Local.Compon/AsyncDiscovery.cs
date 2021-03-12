using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x020001F9 RID: 505
	public sealed class AsyncDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06000F6B RID: 3947 RVA: 0x00027A38 File Offset: 0x00025C38
		protected override void DoWork(CancellationToken cancellationToken)
		{
			GenericWorkItemHelper.CreateAllDefinitions(new string[]
			{
				"Async.xml"
			}, base.Broker, base.TraceContext, base.Result);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.SMTPTracer, AsyncDiscovery.traceContext, "[AsyncDiscovery.DoWork]: Async work item definitions created", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\RandomResult\\Probes\\AsyncDiscovery.cs", 37);
		}

		// Token: 0x0400075A RID: 1882
		private static TracingContext traceContext = new TracingContext();
	}
}
