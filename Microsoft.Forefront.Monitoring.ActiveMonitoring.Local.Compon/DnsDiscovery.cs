using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200007E RID: 126
	public sealed class DnsDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06000333 RID: 819 RVA: 0x00013428 File Offset: 0x00011628
		protected override void DoWork(CancellationToken cancellationToken)
		{
			XmlNode definitionNode = GenericWorkItemHelper.GetDefinitionNode("Dns.xml", DnsDiscovery.traceContext);
			GenericWorkItemHelper.CreateCustomDefinitions(definitionNode, base.Broker, DnsDiscovery.traceContext, base.Result);
			if (!FfoLocalEndpointManager.IsDomainNameServerRoleInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.DNSTracer, DnsDiscovery.traceContext, "[DnsDiscovery.DoWork]: DomainNameServer role is not installed on this server, skipping FFODNS_DNSServer.xml.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\DnsDiscovery.cs", 40);
				base.Result.StateAttribute1 = "DnsDiscovery: DomainNameServer role is not installed on this server, skipping FFODNS_DNSServer.xml.";
				GenericWorkItemHelper.CompleteDiscovery(DnsDiscovery.traceContext);
				return;
			}
			GenericWorkItemHelper.CreateAllDefinitions(new List<string>
			{
				"FFODNS_DNSServer.xml"
			}, base.Broker, DnsDiscovery.traceContext, base.Result);
		}

		// Token: 0x040001D7 RID: 471
		private static TracingContext traceContext = new TracingContext();
	}
}
