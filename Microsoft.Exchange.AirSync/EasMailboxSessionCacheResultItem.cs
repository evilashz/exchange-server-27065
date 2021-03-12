using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200007F RID: 127
	[XmlType("CacheEntry")]
	public class EasMailboxSessionCacheResultItem
	{
		// Token: 0x060006C2 RID: 1730 RVA: 0x00025E2A File Offset: 0x0002402A
		public EasMailboxSessionCacheResultItem()
		{
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00025E32 File Offset: 0x00024032
		public EasMailboxSessionCacheResultItem(string id, TimeSpan timeToLive)
		{
			this.Identifier = id;
			this.TimeToLive = timeToLive.ToString();
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00025E54 File Offset: 0x00024054
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x00025E5C File Offset: 0x0002405C
		[XmlElement("Id")]
		public string Identifier { get; set; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00025E65 File Offset: 0x00024065
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x00025E6D File Offset: 0x0002406D
		public string TimeToLive { get; set; }
	}
}
