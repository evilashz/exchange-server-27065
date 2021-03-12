using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000592 RID: 1426
	[XmlType(TypeName = "TransportServerConfigXml")]
	[Serializable]
	public sealed class ServerConfigXML : XMLSerializableBase
	{
		// Token: 0x0600425E RID: 16990 RVA: 0x000FAA7C File Offset: 0x000F8C7C
		public ServerConfigXML()
		{
			this.QueueLog = new LogConfigXML();
			this.WlmLog = new LogConfigXML();
			this.AgentLog = new LogConfigXML();
			this.FlowControlLog = new LogConfigXML();
			this.ResourceLog = new LogConfigXML();
			this.ProcessingSchedulerLog = new LogConfigXML();
			this.DnsLog = new LogConfigXML();
			this.JournalLog = new LogConfigXML();
			this.TransportMaintenanceLog = new LogConfigXML();
			this.MaximumPreferredActiveDatabases = null;
		}

		// Token: 0x170015A9 RID: 5545
		// (get) Token: 0x0600425F RID: 16991 RVA: 0x000FAB01 File Offset: 0x000F8D01
		// (set) Token: 0x06004260 RID: 16992 RVA: 0x000FAB09 File Offset: 0x000F8D09
		[XmlElement(ElementName = "QueueLog")]
		public LogConfigXML QueueLog { get; set; }

		// Token: 0x170015AA RID: 5546
		// (get) Token: 0x06004261 RID: 16993 RVA: 0x000FAB12 File Offset: 0x000F8D12
		// (set) Token: 0x06004262 RID: 16994 RVA: 0x000FAB1A File Offset: 0x000F8D1A
		[XmlElement(ElementName = "WlmLog")]
		public LogConfigXML WlmLog { get; set; }

		// Token: 0x170015AB RID: 5547
		// (get) Token: 0x06004263 RID: 16995 RVA: 0x000FAB23 File Offset: 0x000F8D23
		// (set) Token: 0x06004264 RID: 16996 RVA: 0x000FAB2B File Offset: 0x000F8D2B
		[XmlElement(ElementName = "AgentLog")]
		public LogConfigXML AgentLog { get; set; }

		// Token: 0x170015AC RID: 5548
		// (get) Token: 0x06004265 RID: 16997 RVA: 0x000FAB34 File Offset: 0x000F8D34
		// (set) Token: 0x06004266 RID: 16998 RVA: 0x000FAB3C File Offset: 0x000F8D3C
		[XmlElement(ElementName = "FlowControlLog")]
		public LogConfigXML FlowControlLog { get; set; }

		// Token: 0x170015AD RID: 5549
		// (get) Token: 0x06004267 RID: 16999 RVA: 0x000FAB45 File Offset: 0x000F8D45
		// (set) Token: 0x06004268 RID: 17000 RVA: 0x000FAB4D File Offset: 0x000F8D4D
		[XmlElement(ElementName = "ResourceLog")]
		public LogConfigXML ResourceLog { get; set; }

		// Token: 0x170015AE RID: 5550
		// (get) Token: 0x06004269 RID: 17001 RVA: 0x000FAB56 File Offset: 0x000F8D56
		// (set) Token: 0x0600426A RID: 17002 RVA: 0x000FAB5E File Offset: 0x000F8D5E
		[XmlElement(ElementName = "ProcessingSchedulerLog")]
		public LogConfigXML ProcessingSchedulerLog { get; set; }

		// Token: 0x170015AF RID: 5551
		// (get) Token: 0x0600426B RID: 17003 RVA: 0x000FAB67 File Offset: 0x000F8D67
		// (set) Token: 0x0600426C RID: 17004 RVA: 0x000FAB6F File Offset: 0x000F8D6F
		[XmlElement(ElementName = "DnsLog")]
		public LogConfigXML DnsLog { get; set; }

		// Token: 0x170015B0 RID: 5552
		// (get) Token: 0x0600426D RID: 17005 RVA: 0x000FAB78 File Offset: 0x000F8D78
		// (set) Token: 0x0600426E RID: 17006 RVA: 0x000FAB80 File Offset: 0x000F8D80
		[XmlElement(ElementName = "JournalLog")]
		public LogConfigXML JournalLog { get; set; }

		// Token: 0x170015B1 RID: 5553
		// (get) Token: 0x0600426F RID: 17007 RVA: 0x000FAB89 File Offset: 0x000F8D89
		// (set) Token: 0x06004270 RID: 17008 RVA: 0x000FAB91 File Offset: 0x000F8D91
		[XmlElement(ElementName = "MaintenanceLog")]
		public LogConfigXML TransportMaintenanceLog { get; set; }

		// Token: 0x170015B2 RID: 5554
		// (get) Token: 0x06004271 RID: 17009 RVA: 0x000FAB9A File Offset: 0x000F8D9A
		// (set) Token: 0x06004272 RID: 17010 RVA: 0x000FABA2 File Offset: 0x000F8DA2
		[XmlElement(ElementName = "MailboxProvisioningAttributes")]
		public MailboxProvisioningAttributes MailboxProvisioningAttributes { get; set; }

		// Token: 0x170015B3 RID: 5555
		// (get) Token: 0x06004273 RID: 17011 RVA: 0x000FABAB File Offset: 0x000F8DAB
		// (set) Token: 0x06004274 RID: 17012 RVA: 0x000FABB3 File Offset: 0x000F8DB3
		[XmlElement(ElementName = "MaxPrefActives")]
		public int? MaximumPreferredActiveDatabases { get; set; }
	}
}
