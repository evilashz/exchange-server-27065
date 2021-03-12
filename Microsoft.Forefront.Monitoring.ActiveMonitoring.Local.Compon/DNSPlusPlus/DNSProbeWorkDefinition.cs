using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.DNSPlusPlus
{
	// Token: 0x02000085 RID: 133
	internal class DNSProbeWorkDefinition
	{
		// Token: 0x06000399 RID: 921 RVA: 0x000155D2 File Offset: 0x000137D2
		public DNSProbeWorkDefinition(string workContextXml, TracingContext traceContext)
		{
			this.traceContext = traceContext;
			this.LoadWorkContext(workContextXml);
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600039A RID: 922 RVA: 0x000155E8 File Offset: 0x000137E8
		// (set) Token: 0x0600039B RID: 923 RVA: 0x000155F0 File Offset: 0x000137F0
		public DnsConfiguration Configuration { get; private set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600039C RID: 924 RVA: 0x000155F9 File Offset: 0x000137F9
		// (set) Token: 0x0600039D RID: 925 RVA: 0x00015601 File Offset: 0x00013801
		public List<DnsProbeOperation> Operations { get; private set; }

		// Token: 0x0600039E RID: 926 RVA: 0x0001560C File Offset: 0x0001380C
		private void LoadWorkContext(string workContextXml)
		{
			if (string.IsNullOrWhiteSpace(workContextXml))
			{
				throw new ArgumentException("Work Context XML is null");
			}
			WTFDiagnostics.TraceInformation(ExTraceGlobals.DNSTracer, this.traceContext, "DNSProbeWorkDefinition: Loading work context", null, "LoadWorkContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsProbeWorkDefinition.cs", 59);
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			safeXmlDocument.LoadXml(workContextXml);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.DNSTracer, this.traceContext, "DNSProbeWorkDefinition: Parsing //WorkContext", null, "LoadWorkContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsProbeWorkDefinition.cs", 63);
			XmlElement xmlElement = Utils.CheckXmlElement(safeXmlDocument.SelectSingleNode("//WorkContext"), "//WorkContext");
			XmlElement xmlElement2 = Utils.CheckXmlElement(xmlElement.SelectSingleNode("DnsLocation"), "WorkContext.DnsLocation");
			bool boolean = Utils.GetBoolean(xmlElement2.GetAttribute("IsLocal"), "DnsLocation.IsLocal");
			WTFDiagnostics.TraceInformation<bool>(ExTraceGlobals.DNSTracer, this.traceContext, "DNSProbeWorkDefinition: DnsLocation.IsLocal={0}", boolean, null, "LoadWorkContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsProbeWorkDefinition.cs", 68);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.DNSTracer, this.traceContext, "DNSProbeWorkDefinition: Parsing /WorkContext/DnsConfiguration", null, "LoadWorkContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsProbeWorkDefinition.cs", 70);
			XmlElement configNode = xmlElement.SelectSingleNode("DnsConfiguration") as XmlElement;
			this.Configuration = DnsConfiguration.CreateInstance(configNode, this.traceContext, boolean);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.DNSTracer, this.traceContext, "DNSProbeWorkDefinition: Parsing /WorkContext/DnsOperations", null, "LoadWorkContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsProbeWorkDefinition.cs", 75);
			XmlElement operationsNode = Utils.CheckXmlElement(xmlElement.SelectSingleNode("DnsOperations"), "DnsOperations");
			this.Operations = DnsProbeOperation.GetOperations(operationsNode, this.Configuration, this.traceContext);
		}

		// Token: 0x0400020D RID: 525
		private TracingContext traceContext;
	}
}
