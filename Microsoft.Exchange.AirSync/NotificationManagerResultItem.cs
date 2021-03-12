using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000086 RID: 134
	public class NotificationManagerResultItem
	{
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x000268DE File Offset: 0x00024ADE
		// (set) Token: 0x06000706 RID: 1798 RVA: 0x000268E6 File Offset: 0x00024AE6
		public string UniqueId { get; set; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x000268EF File Offset: 0x00024AEF
		// (set) Token: 0x06000708 RID: 1800 RVA: 0x000268F7 File Offset: 0x00024AF7
		public string Command { get; set; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x00026900 File Offset: 0x00024B00
		// (set) Token: 0x0600070A RID: 1802 RVA: 0x00026908 File Offset: 0x00024B08
		public string EmailAddress { get; set; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x00026911 File Offset: 0x00024B11
		// (set) Token: 0x0600070C RID: 1804 RVA: 0x00026919 File Offset: 0x00024B19
		public string DeviceId { get; set; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x00026922 File Offset: 0x00024B22
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x0002692A File Offset: 0x00024B2A
		public long PolicyKey { get; set; }

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x00026933 File Offset: 0x00024B33
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x0002693B File Offset: 0x00024B3B
		public string LiveTime { get; set; }

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x00026944 File Offset: 0x00024B44
		// (set) Token: 0x06000712 RID: 1810 RVA: 0x0002694C File Offset: 0x00024B4C
		public int QueueCount { get; set; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x00026955 File Offset: 0x00024B55
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x0002695D File Offset: 0x00024B5D
		public int TotalAcquires { get; set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x00026966 File Offset: 0x00024B66
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x0002696E File Offset: 0x00024B6E
		public int TotalKills { get; set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x00026977 File Offset: 0x00024B77
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x0002697F File Offset: 0x00024B7F
		public int TotalReleases { get; set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00026988 File Offset: 0x00024B88
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x00026990 File Offset: 0x00024B90
		public int TotalTimeouts { get; set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x00026999 File Offset: 0x00024B99
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x000269A1 File Offset: 0x00024BA1
		public int TotalXsoEvents { get; set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x000269AA File Offset: 0x00024BAA
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x000269B2 File Offset: 0x00024BB2
		public int TotalXsoExceptions { get; set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x000269BB File Offset: 0x00024BBB
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x000269C3 File Offset: 0x00024BC3
		public bool IsExecuting { get; set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x000269CC File Offset: 0x00024BCC
		// (set) Token: 0x06000722 RID: 1826 RVA: 0x000269D4 File Offset: 0x00024BD4
		public string RequestedWaitTime { get; set; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x000269DD File Offset: 0x00024BDD
		// (set) Token: 0x06000724 RID: 1828 RVA: 0x000269E5 File Offset: 0x00024BE5
		[XmlArrayItem("Action")]
		public List<InstanceAction> Actions { get; set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x000269EE File Offset: 0x00024BEE
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x000269F6 File Offset: 0x00024BF6
		[XmlArrayItem("Event")]
		public List<string> QueuedEvents { get; set; }
	}
}
