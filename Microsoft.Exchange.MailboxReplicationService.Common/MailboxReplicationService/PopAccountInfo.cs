using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200009E RID: 158
	[DataContract]
	internal sealed class PopAccountInfo
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0000B039 File Offset: 0x00009239
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x0000B041 File Offset: 0x00009241
		[DataMember]
		public uint AccountId { get; set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0000B04A File Offset: 0x0000924A
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x0000B052 File Offset: 0x00009252
		[DataMember]
		public byte TypeByte { get; set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0000B05B File Offset: 0x0000925B
		// (set) Token: 0x0600063E RID: 1598 RVA: 0x0000B063 File Offset: 0x00009263
		[DataMember]
		public ushort AccountTypeUShort { get; set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0000B06C File Offset: 0x0000926C
		// (set) Token: 0x06000640 RID: 1600 RVA: 0x0000B074 File Offset: 0x00009274
		[DataMember]
		public string DisplayName { get; set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x0000B07D File Offset: 0x0000927D
		// (set) Token: 0x06000642 RID: 1602 RVA: 0x0000B085 File Offset: 0x00009285
		[DataMember]
		public string UserName { get; set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x0000B08E File Offset: 0x0000928E
		// (set) Token: 0x06000644 RID: 1604 RVA: 0x0000B096 File Offset: 0x00009296
		[DataMember]
		public bool DownloadNewMessagesOnly { get; set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0000B09F File Offset: 0x0000929F
		// (set) Token: 0x06000646 RID: 1606 RVA: 0x0000B0A7 File Offset: 0x000092A7
		[DataMember]
		public bool LeaveMessagesOnServer { get; set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x0000B0B0 File Offset: 0x000092B0
		// (set) Token: 0x06000648 RID: 1608 RVA: 0x0000B0B8 File Offset: 0x000092B8
		[DataMember]
		public int NewMailIndicator { get; set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x0000B0C1 File Offset: 0x000092C1
		// (set) Token: 0x0600064A RID: 1610 RVA: 0x0000B0C9 File Offset: 0x000092C9
		[DataMember]
		public string Password { get; set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0000B0D2 File Offset: 0x000092D2
		// (set) Token: 0x0600064C RID: 1612 RVA: 0x0000B0DA File Offset: 0x000092DA
		[DataMember]
		public string ServerName { get; set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x0000B0E3 File Offset: 0x000092E3
		// (set) Token: 0x0600064E RID: 1614 RVA: 0x0000B0EB File Offset: 0x000092EB
		[DataMember]
		public uint ServerPort { get; set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x0000B0F4 File Offset: 0x000092F4
		// (set) Token: 0x06000650 RID: 1616 RVA: 0x0000B0FC File Offset: 0x000092FC
		[DataMember]
		public ushort ServerTimeout { get; set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x0000B105 File Offset: 0x00009305
		// (set) Token: 0x06000652 RID: 1618 RVA: 0x0000B10D File Offset: 0x0000930D
		[DataMember]
		public DateTime LastWrite { get; set; }
	}
}
