using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000BC RID: 188
	[DataContract]
	internal class OlcLegacyMessageProperties
	{
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0000B936 File Offset: 0x00009B36
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x0000B93E File Offset: 0x00009B3E
		[DataMember]
		public byte AccountId { get; set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x0000B947 File Offset: 0x00009B47
		// (set) Token: 0x0600074C RID: 1868 RVA: 0x0000B94F File Offset: 0x00009B4F
		[DataMember]
		public string Location { get; set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x0000B958 File Offset: 0x00009B58
		// (set) Token: 0x0600074E RID: 1870 RVA: 0x0000B960 File Offset: 0x00009B60
		[DataMember]
		public byte ContentTypeInt { get; set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x0000B969 File Offset: 0x00009B69
		// (set) Token: 0x06000750 RID: 1872 RVA: 0x0000B971 File Offset: 0x00009B71
		[DataMember]
		public bool IsFromSomeoneInAddressBook { get; set; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x0000B97A File Offset: 0x00009B7A
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x0000B982 File Offset: 0x00009B82
		[DataMember]
		public bool IsToAddressInWhiteList { get; set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0000B98B File Offset: 0x00009B8B
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x0000B993 File Offset: 0x00009B93
		[DataMember]
		public int CreateSequenceNumber { get; set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x0000B99C File Offset: 0x00009B9C
		// (set) Token: 0x06000756 RID: 1878 RVA: 0x0000B9A4 File Offset: 0x00009BA4
		[DataMember]
		public int OriginationIP { get; set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x0000B9AD File Offset: 0x00009BAD
		// (set) Token: 0x06000758 RID: 1880 RVA: 0x0000B9B5 File Offset: 0x00009BB5
		[DataMember]
		public bool JunkedByBlockListMessageFilter { get; set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x0000B9BE File Offset: 0x00009BBE
		// (set) Token: 0x0600075A RID: 1882 RVA: 0x0000B9C6 File Offset: 0x00009BC6
		[DataMember]
		public byte EeScanVersion { get; set; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x0000B9CF File Offset: 0x00009BCF
		// (set) Token: 0x0600075C RID: 1884 RVA: 0x0000B9D7 File Offset: 0x00009BD7
		[DataMember]
		public bool LinksAndImages { get; set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x0000B9E0 File Offset: 0x00009BE0
		// (set) Token: 0x0600075E RID: 1886 RVA: 0x0000B9E8 File Offset: 0x00009BE8
		[DataMember]
		public bool PutAddressBookBlockList { get; set; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0000B9F1 File Offset: 0x00009BF1
		// (set) Token: 0x06000760 RID: 1888 RVA: 0x0000B9F9 File Offset: 0x00009BF9
		[DataMember]
		public bool? SendAsAddressInRcptsList { get; set; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x0000BA02 File Offset: 0x00009C02
		// (set) Token: 0x06000762 RID: 1890 RVA: 0x0000BA0A File Offset: 0x00009C0A
		[DataMember]
		public byte? CategorizationChangedBy { get; set; }
	}
}
