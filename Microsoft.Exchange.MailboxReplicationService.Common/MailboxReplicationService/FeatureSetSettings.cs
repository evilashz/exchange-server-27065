using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200009A RID: 154
	[DataContract]
	internal sealed class FeatureSetSettings : ItemPropertiesBase
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0000AEC5 File Offset: 0x000090C5
		// (set) Token: 0x0600060E RID: 1550 RVA: 0x0000AECD File Offset: 0x000090CD
		[DataMember]
		public long MaxDailyMessages { get; set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0000AED6 File Offset: 0x000090D6
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x0000AEDE File Offset: 0x000090DE
		[DataMember]
		public bool ToolsAccount { get; set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0000AEE7 File Offset: 0x000090E7
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x0000AEEF File Offset: 0x000090EF
		[DataMember]
		public long MaxRecipients { get; set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0000AEF8 File Offset: 0x000090F8
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x0000AF00 File Offset: 0x00009100
		[DataMember]
		public bool HipChallengeApplicable { get; set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x0000AF09 File Offset: 0x00009109
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x0000AF11 File Offset: 0x00009111
		[DataMember]
		public int AccountTrustLevel { get; set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0000AF1A File Offset: 0x0000911A
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x0000AF22 File Offset: 0x00009122
		[DataMember]
		public bool HijackDetection { get; set; }
	}
}
