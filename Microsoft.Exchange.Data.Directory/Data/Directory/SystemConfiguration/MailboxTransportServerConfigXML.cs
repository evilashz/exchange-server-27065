using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002DD RID: 733
	[XmlType(TypeName = "MailboxTransportServerConfigXml")]
	[Serializable]
	public sealed class MailboxTransportServerConfigXML : XMLSerializableBase
	{
		// Token: 0x06002281 RID: 8833 RVA: 0x00096F0A File Offset: 0x0009510A
		public MailboxTransportServerConfigXML()
		{
			this.MailboxSubmissionAgentLog = new LogConfigXML();
			this.MailboxDeliveryAgentLog = new LogConfigXML();
			this.MailboxDeliveryThrottlingLog = new LogConfigXML();
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06002282 RID: 8834 RVA: 0x00096F33 File Offset: 0x00095133
		// (set) Token: 0x06002283 RID: 8835 RVA: 0x00096F3B File Offset: 0x0009513B
		[XmlElement(ElementName = "MailboxSubmissionAgentLog")]
		public LogConfigXML MailboxSubmissionAgentLog { get; set; }

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x00096F44 File Offset: 0x00095144
		// (set) Token: 0x06002285 RID: 8837 RVA: 0x00096F4C File Offset: 0x0009514C
		[XmlElement(ElementName = "MailboxDeliveryAgentLog")]
		public LogConfigXML MailboxDeliveryAgentLog { get; set; }

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06002286 RID: 8838 RVA: 0x00096F55 File Offset: 0x00095155
		// (set) Token: 0x06002287 RID: 8839 RVA: 0x00096F5D File Offset: 0x0009515D
		[XmlElement(ElementName = "MailboxDeliveryThrottlingLog")]
		public LogConfigXML MailboxDeliveryThrottlingLog { get; set; }
	}
}
