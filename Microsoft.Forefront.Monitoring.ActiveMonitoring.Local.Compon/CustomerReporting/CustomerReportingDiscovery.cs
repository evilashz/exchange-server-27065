using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.CustomerReporting
{
	// Token: 0x0200006F RID: 111
	public sealed class CustomerReportingDiscovery : MaintenanceWorkItem
	{
		// Token: 0x060002E3 RID: 739 RVA: 0x00011118 File Offset: 0x0000F318
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (!FfoLocalEndpointManager.IsBackgroundRoleInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.DataminingTracer, CustomerReportingDiscovery.traceContext, "[CustomerReportingDiscovery.DoWork]: Background role is not installed on this server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Datamining\\CustomerReportingDiscovery.cs", 34);
				base.Result.StateAttribute1 = "CustomerReporting: Background role is not installed on this server.";
				return;
			}
			GenericWorkItemHelper.CreateAllDefinitions(new List<string>
			{
				"CustomerReporting.xml"
			}, base.Broker, CustomerReportingDiscovery.traceContext, base.Result);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.DataminingTracer, CustomerReportingDiscovery.traceContext, "CustomerReporting:  work item definitions created", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Datamining\\CustomerReportingDiscovery.cs", 53);
		}

		// Token: 0x040001A8 RID: 424
		private static TracingContext traceContext = new TracingContext();
	}
}
