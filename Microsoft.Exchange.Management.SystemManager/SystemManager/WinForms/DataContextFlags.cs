using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000120 RID: 288
	public class DataContextFlags
	{
		// Token: 0x06000B24 RID: 2852 RVA: 0x00027EAB File Offset: 0x000260AB
		public DataContextFlags()
		{
			this.pages = new ExchangePageCollection(this);
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x00027EC6 File Offset: 0x000260C6
		// (set) Token: 0x06000B26 RID: 2854 RVA: 0x00027ECE File Offset: 0x000260CE
		[DefaultValue(true)]
		public bool NeedToShowVersionWarning
		{
			get
			{
				return this.needToShowVersionWarning;
			}
			set
			{
				this.needToShowVersionWarning = value;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x00027ED7 File Offset: 0x000260D7
		public ExchangePageCollection Pages
		{
			get
			{
				return this.pages;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x00027EDF File Offset: 0x000260DF
		// (set) Token: 0x06000B29 RID: 2857 RVA: 0x00027EE7 File Offset: 0x000260E7
		public int SelectedObjectsCount { get; set; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x00027EF0 File Offset: 0x000260F0
		// (set) Token: 0x06000B2B RID: 2859 RVA: 0x00027EF8 File Offset: 0x000260F8
		public string SelectedObjectDetailsType { get; set; }

		// Token: 0x040004A9 RID: 1193
		private bool needToShowVersionWarning = true;

		// Token: 0x040004AA RID: 1194
		private ExchangePageCollection pages;
	}
}
