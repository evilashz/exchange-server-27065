using System;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x02000293 RID: 659
	[Serializable]
	public class Slot
	{
		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001BDC RID: 7132 RVA: 0x000796A2 File Offset: 0x000778A2
		// (set) Token: 0x06001BDD RID: 7133 RVA: 0x000796AA File Offset: 0x000778AA
		public string Key { get; set; }

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001BDE RID: 7134 RVA: 0x000796B3 File Offset: 0x000778B3
		// (set) Token: 0x06001BDF RID: 7135 RVA: 0x000796BB File Offset: 0x000778BB
		public string Version { get; set; }

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x000796C4 File Offset: 0x000778C4
		// (set) Token: 0x06001BE1 RID: 7137 RVA: 0x000796CC File Offset: 0x000778CC
		public bool Removed { get; set; }
	}
}
