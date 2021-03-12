using System;
using Microsoft.Exchange.Data;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x020000CC RID: 204
	public class FilterColumnProfile
	{
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x000188C3 File Offset: 0x00016AC3
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x000188CB File Offset: 0x00016ACB
		public string PickerProfile { get; set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x000188D4 File Offset: 0x00016AD4
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x000188DC File Offset: 0x00016ADC
		public string DisplayMember { get; set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x000188E5 File Offset: 0x00016AE5
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x000188ED File Offset: 0x00016AED
		public string ValueMember { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x000188F6 File Offset: 0x00016AF6
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x000188FE File Offset: 0x00016AFE
		internal ProviderPropertyDefinition PropertyDefinition { get; set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x00018907 File Offset: 0x00016B07
		// (set) Token: 0x06000722 RID: 1826 RVA: 0x0001890F File Offset: 0x00016B0F
		public string Name { get; set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x00018918 File Offset: 0x00016B18
		// (set) Token: 0x06000724 RID: 1828 RVA: 0x00018920 File Offset: 0x00016B20
		public PropertyFilterOperator[] Operators { get; set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x00018929 File Offset: 0x00016B29
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x00018931 File Offset: 0x00016B31
		public DisplayFormatMode FormatMode { get; set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x0001893A File Offset: 0x00016B3A
		// (set) Token: 0x06000728 RID: 1832 RVA: 0x00018942 File Offset: 0x00016B42
		public Type ColumnType { get; set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0001894B File Offset: 0x00016B4B
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x00018953 File Offset: 0x00016B53
		public ObjectListSource FilterableListSource { get; set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0001895C File Offset: 0x00016B5C
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x00018964 File Offset: 0x00016B64
		public string RefDisplayedColumn { get; set; }
	}
}
