using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Storage.FolderTask
{
	// Token: 0x02000963 RID: 2403
	public sealed class HierarchyFolderNode
	{
		// Token: 0x060058FD RID: 22781 RVA: 0x0016DEC4 File Offset: 0x0016C0C4
		public HierarchyFolderNode(string path)
		{
			this.Path = path;
			this.ChildNodes = new Dictionary<string, HierarchyFolderNode>();
		}

		// Token: 0x170018A1 RID: 6305
		// (get) Token: 0x060058FE RID: 22782 RVA: 0x0016DEDE File Offset: 0x0016C0DE
		// (set) Token: 0x060058FF RID: 22783 RVA: 0x0016DEE6 File Offset: 0x0016C0E6
		public string Path { get; set; }

		// Token: 0x170018A2 RID: 6306
		// (get) Token: 0x06005900 RID: 22784 RVA: 0x0016DEEF File Offset: 0x0016C0EF
		// (set) Token: 0x06005901 RID: 22785 RVA: 0x0016DEF7 File Offset: 0x0016C0F7
		public ulong TotalItemSize { get; set; }

		// Token: 0x170018A3 RID: 6307
		// (get) Token: 0x06005902 RID: 22786 RVA: 0x0016DF00 File Offset: 0x0016C100
		// (set) Token: 0x06005903 RID: 22787 RVA: 0x0016DF08 File Offset: 0x0016C108
		public ulong AggregateTotalItemSize { get; set; }

		// Token: 0x170018A4 RID: 6308
		// (get) Token: 0x06005904 RID: 22788 RVA: 0x0016DF11 File Offset: 0x0016C111
		// (set) Token: 0x06005905 RID: 22789 RVA: 0x0016DF19 File Offset: 0x0016C119
		public HierarchyFolderNode Parent { get; set; }

		// Token: 0x170018A5 RID: 6309
		// (get) Token: 0x06005906 RID: 22790 RVA: 0x0016DF22 File Offset: 0x0016C122
		// (set) Token: 0x06005907 RID: 22791 RVA: 0x0016DF2A File Offset: 0x0016C12A
		public Dictionary<string, HierarchyFolderNode> ChildNodes { get; set; }
	}
}
