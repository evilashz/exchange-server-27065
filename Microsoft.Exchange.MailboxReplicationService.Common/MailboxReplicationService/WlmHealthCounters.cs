using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000288 RID: 648
	[Serializable]
	public sealed class WlmHealthCounters : XMLSerializableBase
	{
		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x06001FE0 RID: 8160 RVA: 0x00043ACE File Offset: 0x00041CCE
		// (set) Token: 0x06001FE1 RID: 8161 RVA: 0x00043AD6 File Offset: 0x00041CD6
		[XmlAttribute(AttributeName = "UnderloadedCounter")]
		public uint UnderloadedCounter { get; set; }

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06001FE2 RID: 8162 RVA: 0x00043ADF File Offset: 0x00041CDF
		// (set) Token: 0x06001FE3 RID: 8163 RVA: 0x00043AE7 File Offset: 0x00041CE7
		[XmlAttribute(AttributeName = "FullCounter")]
		public uint FullCounter { get; set; }

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06001FE4 RID: 8164 RVA: 0x00043AF0 File Offset: 0x00041CF0
		// (set) Token: 0x06001FE5 RID: 8165 RVA: 0x00043AF8 File Offset: 0x00041CF8
		[XmlAttribute(AttributeName = "OverloadedCounter")]
		public uint OverloadedCounter { get; set; }

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06001FE6 RID: 8166 RVA: 0x00043B01 File Offset: 0x00041D01
		// (set) Token: 0x06001FE7 RID: 8167 RVA: 0x00043B09 File Offset: 0x00041D09
		[XmlAttribute(AttributeName = "CriticalCounter")]
		public uint CriticalCounter { get; set; }

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06001FE8 RID: 8168 RVA: 0x00043B12 File Offset: 0x00041D12
		// (set) Token: 0x06001FE9 RID: 8169 RVA: 0x00043B1A File Offset: 0x00041D1A
		[XmlAttribute(AttributeName = "UnknownCounter")]
		public uint UnknownCounter { get; set; }
	}
}
