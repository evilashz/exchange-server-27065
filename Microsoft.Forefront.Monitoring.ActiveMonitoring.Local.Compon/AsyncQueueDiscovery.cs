using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200000F RID: 15
	public sealed class AsyncQueueDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00003E2C File Offset: 0x0000202C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (!FfoLocalEndpointManager.IsBackgroundRoleInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.AsyncEngineTracer, AsyncQueueDiscovery.traceContext, "[AsyncQueueDiscovery.DoWork]: Background role is not installed on this server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\AsyncQueue\\AsyncQueueDiscovery.cs", 35);
				base.Result.StateAttribute1 = "AsyncQueueDiscovery: Background role is not installed on this server.";
				return;
			}
			GenericWorkItemHelper.CreateAllDefinitions(new List<string>
			{
				"AsyncQueue.xml",
				"AsyncQueueDaemon.xml"
			}, base.Broker, AsyncQueueDiscovery.traceContext, base.Result);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.AsyncEngineTracer, AsyncQueueDiscovery.traceContext, "AsyncQueueDiscovery:  work item definitions created", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\AsyncQueue\\AsyncQueueDiscovery.cs", 55);
		}

		// Token: 0x04000049 RID: 73
		private static TracingContext traceContext = new TracingContext();
	}
}
