using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000221 RID: 545
	[Serializable]
	public class TimeSlotXML
	{
		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x0003CF45 File Offset: 0x0003B145
		// (set) Token: 0x06001D8F RID: 7567 RVA: 0x0003CF4D File Offset: 0x0003B14D
		[XmlAttribute(AttributeName = "StartTime")]
		public string StartTime { get; set; }

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06001D90 RID: 7568 RVA: 0x0003CF56 File Offset: 0x0003B156
		// (set) Token: 0x06001D91 RID: 7569 RVA: 0x0003CF5E File Offset: 0x0003B15E
		[CLSCompliant(false)]
		[XmlAttribute(AttributeName = "Value")]
		public ulong Value { get; set; }
	}
}
