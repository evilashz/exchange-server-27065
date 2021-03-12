using System;
using Microsoft.Exchange.Data.Reporting;

namespace Microsoft.Exchange.Management.TransportProcessingQuota
{
	// Token: 0x020000B4 RID: 180
	[Serializable]
	public class TransportProcessingQuotaDigest
	{
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0001A89B File Offset: 0x00018A9B
		// (set) Token: 0x0600066F RID: 1647 RVA: 0x0001A8A3 File Offset: 0x00018AA3
		public Guid ExternalDirectoryOrganizationId { get; set; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x0001A8AC File Offset: 0x00018AAC
		// (set) Token: 0x06000671 RID: 1649 RVA: 0x0001A8B4 File Offset: 0x00018AB4
		public bool Throttled { get; set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x0001A8BD File Offset: 0x00018ABD
		// (set) Token: 0x06000673 RID: 1651 RVA: 0x0001A8C5 File Offset: 0x00018AC5
		public ThrottlingSource Source { get; set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x0001A8CE File Offset: 0x00018ACE
		// (set) Token: 0x06000675 RID: 1653 RVA: 0x0001A8D6 File Offset: 0x00018AD6
		public double Cost { get; set; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x0001A8DF File Offset: 0x00018ADF
		// (set) Token: 0x06000677 RID: 1655 RVA: 0x0001A8E7 File Offset: 0x00018AE7
		public int MessageCount { get; set; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0001A8F0 File Offset: 0x00018AF0
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x0001A8F8 File Offset: 0x00018AF8
		public double MessageAverageSizeKb { get; set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0001A901 File Offset: 0x00018B01
		// (set) Token: 0x0600067B RID: 1659 RVA: 0x0001A909 File Offset: 0x00018B09
		public double StandardDeviation { get; set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0001A912 File Offset: 0x00018B12
		// (set) Token: 0x0600067D RID: 1661 RVA: 0x0001A91A File Offset: 0x00018B1A
		public double ThrottlingFactor { get; set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0001A923 File Offset: 0x00018B23
		// (set) Token: 0x0600067F RID: 1663 RVA: 0x0001A92B File Offset: 0x00018B2B
		public double PartitionCost { get; set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x0001A934 File Offset: 0x00018B34
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x0001A93C File Offset: 0x00018B3C
		public int PartitionMessageCount { get; set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x0001A945 File Offset: 0x00018B45
		// (set) Token: 0x06000683 RID: 1667 RVA: 0x0001A94D File Offset: 0x00018B4D
		public double PartitionMessageAverageSizeKb { get; set; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0001A956 File Offset: 0x00018B56
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x0001A95E File Offset: 0x00018B5E
		public int PartitionTenantCount { get; set; }

		// Token: 0x06000686 RID: 1670 RVA: 0x0001A968 File Offset: 0x00018B68
		internal static TransportProcessingQuotaDigest Create(TenantThrottleInfo tenantThrottleInfo)
		{
			return new TransportProcessingQuotaDigest
			{
				ExternalDirectoryOrganizationId = tenantThrottleInfo.TenantId,
				Throttled = tenantThrottleInfo.IsThrottled,
				Source = ((tenantThrottleInfo.ThrottleState == TenantThrottleState.Auto) ? ThrottlingSource.Calculated : ThrottlingSource.Override),
				Cost = tenantThrottleInfo.AverageMessageCostMs,
				MessageCount = tenantThrottleInfo.MessageCount,
				MessageAverageSizeKb = tenantThrottleInfo.AverageMessageSizeKb,
				StandardDeviation = tenantThrottleInfo.StandardDeviation,
				ThrottlingFactor = tenantThrottleInfo.ThrottlingFactor,
				PartitionCost = tenantThrottleInfo.PartitionAverageMessageCostMs,
				PartitionMessageCount = tenantThrottleInfo.PartitionMessageCount,
				PartitionMessageAverageSizeKb = tenantThrottleInfo.PartitionAverageMessageSizeKb,
				PartitionTenantCount = tenantThrottleInfo.PartitionTenantCount
			};
		}
	}
}
