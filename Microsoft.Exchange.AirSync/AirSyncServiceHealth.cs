using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000078 RID: 120
	public class AirSyncServiceHealth
	{
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00025980 File Offset: 0x00023B80
		// (set) Token: 0x06000674 RID: 1652 RVA: 0x00025988 File Offset: 0x00023B88
		public string ServerName { get; set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x00025991 File Offset: 0x00023B91
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x00025999 File Offset: 0x00023B99
		public long TotalNumberOfRequests { get; set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x000259A2 File Offset: 0x00023BA2
		// (set) Token: 0x06000678 RID: 1656 RVA: 0x000259AA File Offset: 0x00023BAA
		public long NumberOfRequests { get; set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x000259B3 File Offset: 0x00023BB3
		// (set) Token: 0x0600067A RID: 1658 RVA: 0x000259BB File Offset: 0x00023BBB
		public int NumberOfDevices { get; set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x000259C4 File Offset: 0x00023BC4
		// (set) Token: 0x0600067C RID: 1660 RVA: 0x000259CC File Offset: 0x00023BCC
		public int NumberOfErroredRequests { get; set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x000259D5 File Offset: 0x00023BD5
		// (set) Token: 0x0600067E RID: 1662 RVA: 0x000259DD File Offset: 0x00023BDD
		public List<ErrorDetail> ErrorDetails
		{
			get
			{
				return this.errorDetails;
			}
			set
			{
				this.errorDetails = value;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x000259E6 File Offset: 0x00023BE6
		// (set) Token: 0x06000680 RID: 1664 RVA: 0x000259EE File Offset: 0x00023BEE
		public double RateOfEASRequests { get; set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x000259F7 File Offset: 0x00023BF7
		// (set) Token: 0x06000682 RID: 1666 RVA: 0x000259FF File Offset: 0x00023BFF
		public long ActiveRequests { get; set; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x00025A08 File Offset: 0x00023C08
		// (set) Token: 0x06000684 RID: 1668 RVA: 0x00025A10 File Offset: 0x00023C10
		public long AutoblockedDevices { get; set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x00025A19 File Offset: 0x00023C19
		// (set) Token: 0x06000686 RID: 1670 RVA: 0x00025A21 File Offset: 0x00023C21
		public int NewDevices { get; set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x00025A2A File Offset: 0x00023C2A
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x00025A32 File Offset: 0x00023C32
		public double Http200ResponseRatio { get; set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00025A3B File Offset: 0x00023C3B
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x00025A43 File Offset: 0x00023C43
		public long AverageRpcLatency { get; set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00025A4C File Offset: 0x00023C4C
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x00025A54 File Offset: 0x00023C54
		public long AverageLdapLatency { get; set; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x00025A5D File Offset: 0x00023C5D
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x00025A65 File Offset: 0x00023C65
		public long CurrentlyPendingSync { get; set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00025A6E File Offset: 0x00023C6E
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x00025A76 File Offset: 0x00023C76
		public long NumberOfDroppedSync { get; set; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x00025A7F File Offset: 0x00023C7F
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x00025A87 File Offset: 0x00023C87
		public long NumberOfDroppedPing { get; set; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00025A90 File Offset: 0x00023C90
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x00025A98 File Offset: 0x00023C98
		public long CurrentlyPendingPing { get; set; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x00025AA1 File Offset: 0x00023CA1
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x00025AA9 File Offset: 0x00023CA9
		public double AverageRequestTime { get; set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00025AB2 File Offset: 0x00023CB2
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x00025ABA File Offset: 0x00023CBA
		public float SyncICSFolderCheckPercent { get; set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00025AC3 File Offset: 0x00023CC3
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x00025ACB File Offset: 0x00023CCB
		public float PingICSFolderCheckPercent { get; set; }

		// Token: 0x0400048C RID: 1164
		private List<ErrorDetail> errorDetails = new List<ErrorDetail>();
	}
}
