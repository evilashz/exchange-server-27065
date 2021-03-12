using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TimeBasedAssistants
{
	// Token: 0x02000529 RID: 1321
	[DataContract(Namespace = "")]
	internal class WindowJob
	{
		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06002075 RID: 8309 RVA: 0x000C6816 File Offset: 0x000C4A16
		// (set) Token: 0x06002076 RID: 8310 RVA: 0x000C681E File Offset: 0x000C4A1E
		[DataMember(Order = 0)]
		public DateTime StartTime { get; set; }

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06002077 RID: 8311 RVA: 0x000C6827 File Offset: 0x000C4A27
		// (set) Token: 0x06002078 RID: 8312 RVA: 0x000C682F File Offset: 0x000C4A2F
		[DataMember(Order = 1)]
		public DateTime EndTime { get; set; }

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06002079 RID: 8313 RVA: 0x000C6838 File Offset: 0x000C4A38
		// (set) Token: 0x0600207A RID: 8314 RVA: 0x000C6840 File Offset: 0x000C4A40
		[DataMember(Order = 2)]
		public int TotalOnDatabaseMailboxCount { get; set; }

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x0600207B RID: 8315 RVA: 0x000C6849 File Offset: 0x000C4A49
		// (set) Token: 0x0600207C RID: 8316 RVA: 0x000C6851 File Offset: 0x000C4A51
		[DataMember(Order = 3)]
		public int InterestingMailboxCount { get; set; }

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x0600207D RID: 8317 RVA: 0x000C685A File Offset: 0x000C4A5A
		// (set) Token: 0x0600207E RID: 8318 RVA: 0x000C6862 File Offset: 0x000C4A62
		[DataMember(Order = 4)]
		public int NotInterestingMailboxCount { get; set; }

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x0600207F RID: 8319 RVA: 0x000C686B File Offset: 0x000C4A6B
		// (set) Token: 0x06002080 RID: 8320 RVA: 0x000C6873 File Offset: 0x000C4A73
		[DataMember(Order = 5)]
		public int FilteredMailboxCount { get; set; }

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06002081 RID: 8321 RVA: 0x000C687C File Offset: 0x000C4A7C
		// (set) Token: 0x06002082 RID: 8322 RVA: 0x000C6884 File Offset: 0x000C4A84
		[DataMember(Order = 6)]
		public int FailedFilteringMailboxCount { get; set; }

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06002083 RID: 8323 RVA: 0x000C688D File Offset: 0x000C4A8D
		// (set) Token: 0x06002084 RID: 8324 RVA: 0x000C6895 File Offset: 0x000C4A95
		[DataMember(Order = 7)]
		public int CompletedMailboxCount { get; set; }

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06002085 RID: 8325 RVA: 0x000C689E File Offset: 0x000C4A9E
		// (set) Token: 0x06002086 RID: 8326 RVA: 0x000C68A6 File Offset: 0x000C4AA6
		[DataMember(Order = 8)]
		public int MovedToOnDemandMailboxCount { get; set; }

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06002087 RID: 8327 RVA: 0x000C68AF File Offset: 0x000C4AAF
		// (set) Token: 0x06002088 RID: 8328 RVA: 0x000C68B7 File Offset: 0x000C4AB7
		[DataMember(Order = 9)]
		public int FailedMailboxCount { get; set; }

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06002089 RID: 8329 RVA: 0x000C68C0 File Offset: 0x000C4AC0
		// (set) Token: 0x0600208A RID: 8330 RVA: 0x000C68C8 File Offset: 0x000C4AC8
		[DataMember(Order = 10)]
		public int FailedToOpenStoreSessionCount { get; set; }

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x0600208B RID: 8331 RVA: 0x000C68D1 File Offset: 0x000C4AD1
		// (set) Token: 0x0600208C RID: 8332 RVA: 0x000C68D9 File Offset: 0x000C4AD9
		[DataMember(Order = 11)]
		public int RetriedMailboxCount { get; set; }
	}
}
