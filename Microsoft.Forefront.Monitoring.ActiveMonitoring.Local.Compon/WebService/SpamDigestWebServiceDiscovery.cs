using System;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.WebService
{
	// Token: 0x02000292 RID: 658
	public sealed class SpamDigestWebServiceDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06001620 RID: 5664 RVA: 0x00046C98 File Offset: 0x00044E98
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (!FfoLocalEndpointManager.IsWebServiceInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.WebServiceTracer, SpamDigestWebServiceDiscovery.traceContext, "[WebServiceDiscovery.DoWork]: FfoWebService role is not installed on this server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\WebService\\SpamDigestWebServiceDiscovery.cs", 44);
				base.Result.StateAttribute1 = "WebServiceDiscovery: FfoWebService role is not installed on this server.";
				return;
			}
			XmlNode definitionNode = GenericWorkItemHelper.GetDefinitionNode("SpamDigestWebService.xml", SpamDigestWebServiceDiscovery.traceContext);
			GenericWorkItemHelper.CreatePerfCounterDefinitions(definitionNode, base.Broker, SpamDigestWebServiceDiscovery.traceContext, base.Result);
			GenericWorkItemHelper.CreateCustomDefinitions(definitionNode, base.Broker, SpamDigestWebServiceDiscovery.traceContext, base.Result);
			GenericWorkItemHelper.CompleteDiscovery(SpamDigestWebServiceDiscovery.traceContext);
		}

		// Token: 0x04000ABC RID: 2748
		private const string EscalationTeam = "FFO Web Service";

		// Token: 0x04000ABD RID: 2749
		private static TracingContext traceContext = new TracingContext();
	}
}
