using System;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000076 RID: 118
	internal class OBDUploaderProbeDefinition
	{
		// Token: 0x06000300 RID: 768 RVA: 0x000120F4 File Offset: 0x000102F4
		public OBDUploaderProbeDefinition(string extensionXml, TracingContext traceContext)
		{
			this.traceContext = traceContext;
			this.LoadWorkContext(extensionXml);
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0001210A File Offset: 0x0001030A
		// (set) Token: 0x06000302 RID: 770 RVA: 0x00012112 File Offset: 0x00010312
		public string ProgressFileFolder { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0001211B File Offset: 0x0001031B
		// (set) Token: 0x06000304 RID: 772 RVA: 0x00012123 File Offset: 0x00010323
		public string RawLogFileFolder { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0001212C File Offset: 0x0001032C
		// (set) Token: 0x06000306 RID: 774 RVA: 0x00012134 File Offset: 0x00010334
		public string ProgressFileNamePattern { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0001213D File Offset: 0x0001033D
		// (set) Token: 0x06000308 RID: 776 RVA: 0x00012145 File Offset: 0x00010345
		public string RawLogFileNamePattern { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0001214E File Offset: 0x0001034E
		// (set) Token: 0x0600030A RID: 778 RVA: 0x00012156 File Offset: 0x00010356
		public int SLAThresholdInHours { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0001215F File Offset: 0x0001035F
		// (set) Token: 0x0600030C RID: 780 RVA: 0x00012167 File Offset: 0x00010367
		public int LiveTrafficCheckThresholdInHours { get; set; }

		// Token: 0x0600030D RID: 781 RVA: 0x00012170 File Offset: 0x00010370
		private void LoadWorkContext(string workContextXml)
		{
			if (string.IsNullOrWhiteSpace(workContextXml))
			{
				throw new ArgumentException("Work Context XML is null");
			}
			WTFDiagnostics.TraceInformation(ExTraceGlobals.DataminingTracer, this.traceContext, "OBDUploaderProbeDefinition: Loading work context", null, "LoadWorkContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Datamining\\Probes\\OBDUploaderProbeDefinition.cs", 80);
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			safeXmlDocument.LoadXml(workContextXml);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.DataminingTracer, this.traceContext, "OBDUploaderProbeDefinition: Parsing //WorkContext", null, "LoadWorkContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Datamining\\Probes\\OBDUploaderProbeDefinition.cs", 84);
			XmlElement xmlElement = Utils.CheckXmlElement(safeXmlDocument.SelectSingleNode("//WorkContext"), "//WorkContext");
			XmlElement xmlElement2 = Utils.CheckXmlElement(xmlElement.SelectSingleNode("OBDUploaderConfiguration"), "WorkContext.OBDUploaderConfiguration");
			this.ProgressFileFolder = xmlElement2.GetAttribute("ProgressFileFolder");
			this.RawLogFileFolder = xmlElement2.GetAttribute("RawLogFileFolder");
			this.ProgressFileNamePattern = xmlElement2.GetAttribute("ProgressFileNamePattern");
			this.RawLogFileNamePattern = xmlElement2.GetAttribute("RawLogFileNamePattern");
			this.SLAThresholdInHours = Utils.GetInteger(xmlElement2.GetAttribute("SLAThresholdInHours"), "OBDUploaderConfiguration.SLAThresholdInHours", 6, 1);
			this.LiveTrafficCheckThresholdInHours = Utils.GetInteger(xmlElement2.GetAttribute("LiveTrafficCheckThresholdInHours"), "OBDUploaderConfiguration.LiveTrafficCheckThresholdInHours", 2, 1);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.DataminingTracer, this.traceContext, string.Format("OBDUploaderProbeDefinition: the result is {0},{1},{2},{3},{4},{5}", new object[]
			{
				this.ProgressFileFolder,
				this.RawLogFileFolder,
				this.ProgressFileNamePattern,
				this.RawLogFileNamePattern,
				this.SLAThresholdInHours,
				this.LiveTrafficCheckThresholdInHours
			}), null, "LoadWorkContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Datamining\\Probes\\OBDUploaderProbeDefinition.cs", 94);
		}

		// Token: 0x040001BD RID: 445
		private TracingContext traceContext;
	}
}
