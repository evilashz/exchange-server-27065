using System;
using System.Collections;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000023 RID: 35
	public class SupportTextComparerAdapter : IComparer
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x00007BFE File Offset: 0x00005DFE
		public SupportTextComparerAdapter(ISupportTextComparer supportTextComparer, ICustomFormatter customFormatter, IFormatProvider formatProvider, string formatString, string defaultEmptyText)
		{
			this.SupportTextComparer = supportTextComparer;
			this.CustomFormatter = customFormatter;
			this.FormatProvider = formatProvider;
			this.FormatString = formatString;
			this.DefaultEmptyText = defaultEmptyText;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00007C2B File Offset: 0x00005E2B
		public int Compare(object x, object y)
		{
			return this.SupportTextComparer.Compare(x, y, this.CustomFormatter, this.FormatProvider, this.FormatString, this.DefaultEmptyText);
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00007C52 File Offset: 0x00005E52
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00007C5A File Offset: 0x00005E5A
		public ISupportTextComparer SupportTextComparer { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00007C63 File Offset: 0x00005E63
		// (set) Token: 0x060001CB RID: 459 RVA: 0x00007C6B File Offset: 0x00005E6B
		public ICustomFormatter CustomFormatter { get; private set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00007C74 File Offset: 0x00005E74
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00007C7C File Offset: 0x00005E7C
		public IFormatProvider FormatProvider { get; private set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00007C85 File Offset: 0x00005E85
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00007C8D File Offset: 0x00005E8D
		public string FormatString { get; private set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00007C96 File Offset: 0x00005E96
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00007C9E File Offset: 0x00005E9E
		public string DefaultEmptyText { get; private set; }
	}
}
