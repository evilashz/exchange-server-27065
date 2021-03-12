using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200008A RID: 138
	public sealed class EmailManagementDiscovery : MaintenanceWorkItem
	{
		// Token: 0x060003C3 RID: 963 RVA: 0x0001651C File Offset: 0x0001471C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (!FfoLocalEndpointManager.IsBackgroundRoleInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.DataminingTracer, EmailManagementDiscovery.traceContext, "[EmailManagementDiscovery.DoWork]: Background role is not installed on this server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\EmailManagement\\EmailManagementDiscovery.cs", 34);
				base.Result.StateAttribute1 = "EmailManagement: Background role is not installed on this server.";
				return;
			}
			GenericWorkItemHelper.CreateAllDefinitions(new List<string>
			{
				"EmailManagement.xml"
			}, base.Broker, EmailManagementDiscovery.traceContext, base.Result);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.DataminingTracer, EmailManagementDiscovery.traceContext, "EmailManagement:  work item definitions created", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\EmailManagement\\EmailManagementDiscovery.cs", 53);
		}

		// Token: 0x04000233 RID: 563
		private static TracingContext traceContext = new TracingContext();
	}
}
