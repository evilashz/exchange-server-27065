using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001F5 RID: 501
	[DataContract]
	[Serializable]
	public class FixedTimeSumSlot : XMLSerializableBase
	{
		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x0003010B File Offset: 0x0002E30B
		// (set) Token: 0x06001579 RID: 5497 RVA: 0x00030113 File Offset: 0x0002E313
		[XmlAttribute(AttributeName = "Value")]
		[DataMember(Name = "Value")]
		[CLSCompliant(false)]
		public uint Value { get; set; }

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x0003011C File Offset: 0x0002E31C
		// (set) Token: 0x0600157B RID: 5499 RVA: 0x00030124 File Offset: 0x0002E324
		[DataMember(Name = "Ticks")]
		[XmlAttribute(AttributeName = "Ticks")]
		public long StartTimeInTicks { get; set; }
	}
}
