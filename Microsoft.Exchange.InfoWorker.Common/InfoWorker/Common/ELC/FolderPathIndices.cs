using System;

namespace Microsoft.Exchange.InfoWorker.Common.ELC
{
	// Token: 0x0200019D RID: 413
	public struct FolderPathIndices
	{
		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002DFD3 File Offset: 0x0002C1D3
		internal FolderPathIndices(int displayNameIndex, int folderDepthIndex, int folderIdIndex, int parentIdIndex, int folderPathIndex)
		{
			this.displayNameIndex = displayNameIndex;
			this.folderDepthIndex = folderDepthIndex;
			this.folderIdIndex = folderIdIndex;
			this.parentIdIndex = parentIdIndex;
			this.folderPathIndex = folderPathIndex;
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x0002DFFA File Offset: 0x0002C1FA
		internal int DisplayNameIndex
		{
			get
			{
				return this.displayNameIndex;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x0002E002 File Offset: 0x0002C202
		internal int FolderDepthIndex
		{
			get
			{
				return this.folderDepthIndex;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x0002E00A File Offset: 0x0002C20A
		internal int FolderIdIndex
		{
			get
			{
				return this.folderIdIndex;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0002E012 File Offset: 0x0002C212
		internal int ParentIdIndex
		{
			get
			{
				return this.parentIdIndex;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x0002E01A File Offset: 0x0002C21A
		internal int FolderPathIndex
		{
			get
			{
				return this.folderPathIndex;
			}
		}

		// Token: 0x04000824 RID: 2084
		private int displayNameIndex;

		// Token: 0x04000825 RID: 2085
		private int folderDepthIndex;

		// Token: 0x04000826 RID: 2086
		private int folderIdIndex;

		// Token: 0x04000827 RID: 2087
		private int parentIdIndex;

		// Token: 0x04000828 RID: 2088
		private int folderPathIndex;
	}
}
