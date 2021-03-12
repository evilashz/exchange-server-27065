using System;
using System.Collections;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x020000CD RID: 205
	public class ResultsColumnProfile
	{
		// Token: 0x0600072E RID: 1838 RVA: 0x00018975 File Offset: 0x00016B75
		public ResultsColumnProfile(string name, bool isDefault, string text)
		{
			this.name = name;
			this.isDefault = isDefault;
			this.text = text;
			this.SortMode = SortMode.NotSpecified;
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x00018999 File Offset: 0x00016B99
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x000189A1 File Offset: 0x00016BA1
		public string Name
		{
			get
			{
				return this.name;
			}
			internal set
			{
				this.name = value;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x000189AA File Offset: 0x00016BAA
		public bool IsDefault
		{
			get
			{
				return this.isDefault;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x000189B2 File Offset: 0x00016BB2
		public string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x000189BA File Offset: 0x00016BBA
		// (set) Token: 0x06000734 RID: 1844 RVA: 0x000189C2 File Offset: 0x00016BC2
		public SortMode SortMode { get; set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x000189CB File Offset: 0x00016BCB
		// (set) Token: 0x06000736 RID: 1846 RVA: 0x000189D3 File Offset: 0x00016BD3
		public IComparer CustomComparer { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x000189DC File Offset: 0x00016BDC
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x000189E4 File Offset: 0x00016BE4
		public ICustomFormatter CustomFormatter { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x000189ED File Offset: 0x00016BED
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x000189F5 File Offset: 0x00016BF5
		public IFormatProvider FormatProvider { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x000189FE File Offset: 0x00016BFE
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x00018A06 File Offset: 0x00016C06
		public string FormatString { get; set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x00018A0F File Offset: 0x00016C0F
		// (set) Token: 0x0600073E RID: 1854 RVA: 0x00018A17 File Offset: 0x00016C17
		public string DefaultEmptyText { get; set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x00018A20 File Offset: 0x00016C20
		// (set) Token: 0x06000740 RID: 1856 RVA: 0x00018A28 File Offset: 0x00016C28
		public IToColorFormatter ColorFormatter { get; set; }

		// Token: 0x04000369 RID: 873
		private string name;

		// Token: 0x0400036A RID: 874
		private bool isDefault;

		// Token: 0x0400036B RID: 875
		private string text;
	}
}
