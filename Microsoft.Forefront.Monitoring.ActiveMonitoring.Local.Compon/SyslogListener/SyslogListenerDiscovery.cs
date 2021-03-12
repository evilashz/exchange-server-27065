using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.FfoSelfRecoveryFx;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.SyslogListener
{
	// Token: 0x0200025B RID: 603
	public sealed class SyslogListenerDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06001441 RID: 5185 RVA: 0x0003B92C File Offset: 0x00039B2C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (!FfoLocalEndpointManager.IsSyslogListenerRoleInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.RAAServiceTracer, SyslogListenerDiscovery.traceContext, "[SyslogListenerDiscovery.DoWork]: SyslogListener role is not installed on this server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\SyslogListener\\SyslogListenerDiscovery.cs", 38);
				base.Result.StateAttribute1 = "SyslogListenerDiscovery: SyslogListener role is not installed on this server.";
				return;
			}
			GenericWorkItemHelper.CreateAllDefinitions(new List<string>
			{
				"SyslogListener.xml"
			}, base.Broker, base.TraceContext, base.Result);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.RAAServiceTracer, SyslogListenerDiscovery.traceContext, "[SyslogListenerDiscovery.DoWork]: SyslogListenerDiscovery work item definitions created.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\SyslogListener\\SyslogListenerDiscovery.cs", 57);
		}

		// Token: 0x040009C1 RID: 2497
		private static TracingContext traceContext = new TracingContext();
	}
}
