using System;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020000A6 RID: 166
	internal class FolderTreeNode
	{
		// Token: 0x060003D6 RID: 982 RVA: 0x00013325 File Offset: 0x00011525
		public FolderTreeNode(int index, int count)
		{
			this.Index = index;
			this.DescendantCount = count;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0001333B File Offset: 0x0001153B
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x00013343 File Offset: 0x00011543
		public int Index { get; private set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0001334C File Offset: 0x0001154C
		// (set) Token: 0x060003DA RID: 986 RVA: 0x00013354 File Offset: 0x00011554
		public int DescendantCount { get; private set; }
	}
}
