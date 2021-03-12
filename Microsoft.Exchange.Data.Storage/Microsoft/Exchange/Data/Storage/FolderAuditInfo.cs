using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000627 RID: 1575
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FolderAuditInfo
	{
		// Token: 0x17001344 RID: 4932
		// (get) Token: 0x060040F3 RID: 16627 RVA: 0x001111BF File Offset: 0x0010F3BF
		// (set) Token: 0x060040F4 RID: 16628 RVA: 0x001111C7 File Offset: 0x0010F3C7
		public StoreObjectId Id { get; private set; }

		// Token: 0x17001345 RID: 4933
		// (get) Token: 0x060040F5 RID: 16629 RVA: 0x001111D0 File Offset: 0x0010F3D0
		// (set) Token: 0x060040F6 RID: 16630 RVA: 0x001111D8 File Offset: 0x0010F3D8
		public string PathName { get; private set; }

		// Token: 0x060040F7 RID: 16631 RVA: 0x001111E1 File Offset: 0x0010F3E1
		public FolderAuditInfo(StoreObjectId folderId, string pathName)
		{
			Util.ThrowOnNullArgument(folderId, "folderId");
			this.Id = folderId;
			this.PathName = pathName;
		}
	}
}
