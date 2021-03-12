using System;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Diagnostics.Components.FfoSelfRecoveryFx;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.RaaService
{
	// Token: 0x020001F8 RID: 504
	public sealed class RaaServiceDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06000F68 RID: 3944 RVA: 0x0002798C File Offset: 0x00025B8C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (!FfoLocalEndpointManager.IsCentralAdminRoleInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.RAAServiceTracer, RaaServiceDiscovery.traceContext, "[RaaServiceDiscovery.DoWork]: RaaService role is not installed on this server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\RaaService\\RaaServiceDiscovery.cs", 38);
				base.Result.StateAttribute1 = "RaaServiceDiscovery: RaaService role is not installed on this server.";
				return;
			}
			XmlNode definitionNode = GenericWorkItemHelper.GetDefinitionNode("RaaService.xml", RaaServiceDiscovery.traceContext);
			GenericWorkItemHelper.CreateCustomDefinitions(definitionNode, base.Broker, RaaServiceDiscovery.traceContext, base.Result);
			GenericWorkItemHelper.CompleteDiscovery(RaaServiceDiscovery.traceContext);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.RAAServiceTracer, RaaServiceDiscovery.traceContext, "[RaaServiceDiscovery.DoWork]: work item definitions created", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\RaaService\\RaaServiceDiscovery.cs", 57);
		}

		// Token: 0x04000759 RID: 1881
		private static TracingContext traceContext = new TracingContext();
	}
}
