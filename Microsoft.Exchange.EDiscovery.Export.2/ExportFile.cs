using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000051 RID: 81
	public class ExportFile
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x0001774D File Offset: 0x0001594D
		// (set) Token: 0x06000660 RID: 1632 RVA: 0x00017755 File Offset: 0x00015955
		public string Name { get; internal set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x0001775E File Offset: 0x0001595E
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x00017766 File Offset: 0x00015966
		public string Path { get; internal set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x0001776F File Offset: 0x0001596F
		// (set) Token: 0x06000664 RID: 1636 RVA: 0x00017777 File Offset: 0x00015977
		public ulong Size { get; internal set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x00017780 File Offset: 0x00015980
		// (set) Token: 0x06000666 RID: 1638 RVA: 0x00017788 File Offset: 0x00015988
		public string Hash { get; internal set; }
	}
}
