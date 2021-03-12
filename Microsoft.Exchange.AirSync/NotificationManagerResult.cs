using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000085 RID: 133
	public class NotificationManagerResult
	{
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x0002681B File Offset: 0x00024A1B
		// (set) Token: 0x060006EF RID: 1775 RVA: 0x00026823 File Offset: 0x00024A23
		public int CreatesPerMinute { get; set; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0002682C File Offset: 0x00024A2C
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x00026834 File Offset: 0x00024A34
		public int HitsPerMinute { get; set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0002683D File Offset: 0x00024A3D
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x00026845 File Offset: 0x00024A45
		public int ContentionsPerMinute { get; set; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0002684E File Offset: 0x00024A4E
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x00026856 File Offset: 0x00024A56
		public int StealsPerMinute { get; set; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0002685F File Offset: 0x00024A5F
		// (set) Token: 0x060006F7 RID: 1783 RVA: 0x00026867 File Offset: 0x00024A67
		public int CacheCount { get; set; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x00026870 File Offset: 0x00024A70
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x00026878 File Offset: 0x00024A78
		public int ActiveCount { get; set; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00026881 File Offset: 0x00024A81
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x00026889 File Offset: 0x00024A89
		public int RemovedCount { get; set; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x00026892 File Offset: 0x00024A92
		// (set) Token: 0x060006FD RID: 1789 RVA: 0x0002689A File Offset: 0x00024A9A
		public int InactiveCount { get; set; }

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x000268A3 File Offset: 0x00024AA3
		// (set) Token: 0x060006FF RID: 1791 RVA: 0x000268AB File Offset: 0x00024AAB
		[XmlArrayItem("Instance")]
		public List<NotificationManagerResultItem> ActiveInstances { get; set; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x000268B4 File Offset: 0x00024AB4
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x000268BC File Offset: 0x00024ABC
		[XmlArrayItem("Instance")]
		public List<NotificationManagerResultItem> InactiveInstances { get; set; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x000268C5 File Offset: 0x00024AC5
		// (set) Token: 0x06000703 RID: 1795 RVA: 0x000268CD File Offset: 0x00024ACD
		[XmlArrayItem("Instance")]
		public List<NotificationManagerResultItem> RemovedInstances { get; set; }
	}
}
