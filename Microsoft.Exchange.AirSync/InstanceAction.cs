using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000087 RID: 135
	public struct InstanceAction
	{
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x00026A07 File Offset: 0x00024C07
		// (set) Token: 0x06000729 RID: 1833 RVA: 0x00026A0F File Offset: 0x00024C0F
		[XmlAttribute("Date")]
		public DateTime Time { get; set; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x00026A18 File Offset: 0x00024C18
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x00026A20 File Offset: 0x00024C20
		[XmlAttribute]
		public string Action { get; set; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x00026A29 File Offset: 0x00024C29
		// (set) Token: 0x0600072D RID: 1837 RVA: 0x00026A31 File Offset: 0x00024C31
		[XmlAttribute("TID")]
		public int ThreadId { get; set; }

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x00026A3A File Offset: 0x00024C3A
		// (set) Token: 0x0600072F RID: 1839 RVA: 0x00026A42 File Offset: 0x00024C42
		[XmlAttribute]
		public string XsoEventType { get; set; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x00026A4B File Offset: 0x00024C4B
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x00026A53 File Offset: 0x00024C53
		[XmlAttribute]
		public string XsoObjectType { get; set; }

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x00026A5C File Offset: 0x00024C5C
		// (set) Token: 0x06000733 RID: 1843 RVA: 0x00026A64 File Offset: 0x00024C64
		[XmlAttribute]
		public string XsoException { get; set; }
	}
}
