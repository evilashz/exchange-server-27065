using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002DC RID: 732
	[XmlType(TypeName = "FrontendTransportServerConfigXml")]
	[Serializable]
	public sealed class FrontendTransportServerConfigXML : XMLSerializableBase
	{
		// Token: 0x06002276 RID: 8822 RVA: 0x00096E81 File Offset: 0x00095081
		public FrontendTransportServerConfigXML()
		{
			this.AgentLog = new LogConfigXML();
			this.AttributionLog = new LogConfigXML();
			this.DnsLog = new LogConfigXML();
			this.ResourceLog = new LogConfigXML();
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06002277 RID: 8823 RVA: 0x00096EB5 File Offset: 0x000950B5
		// (set) Token: 0x06002278 RID: 8824 RVA: 0x00096EBD File Offset: 0x000950BD
		[XmlElement(ElementName = "AgentLog")]
		public LogConfigXML AgentLog { get; set; }

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06002279 RID: 8825 RVA: 0x00096EC6 File Offset: 0x000950C6
		// (set) Token: 0x0600227A RID: 8826 RVA: 0x00096ECE File Offset: 0x000950CE
		[XmlElement(ElementName = "DnsLog")]
		public LogConfigXML DnsLog { get; set; }

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x0600227B RID: 8827 RVA: 0x00096ED7 File Offset: 0x000950D7
		// (set) Token: 0x0600227C RID: 8828 RVA: 0x00096EDF File Offset: 0x000950DF
		[XmlElement(ElementName = "ResourceLog")]
		public LogConfigXML ResourceLog { get; set; }

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x0600227D RID: 8829 RVA: 0x00096EE8 File Offset: 0x000950E8
		// (set) Token: 0x0600227E RID: 8830 RVA: 0x00096EF0 File Offset: 0x000950F0
		[XmlElement(ElementName = "AttributionLog")]
		public LogConfigXML AttributionLog { get; set; }

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x0600227F RID: 8831 RVA: 0x00096EF9 File Offset: 0x000950F9
		// (set) Token: 0x06002280 RID: 8832 RVA: 0x00096F01 File Offset: 0x00095101
		[XmlElement(ElementName = "MaxReceiveTlsRatePerMinute")]
		public int MaxReceiveTlsRatePerMinute { get; set; }
	}
}
