using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E07 RID: 3591
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IFolderHierarchySyncState : ISyncState
	{
		// Token: 0x06007BE0 RID: 31712
		FolderHierarchySync GetFolderHierarchySync();

		// Token: 0x06007BE1 RID: 31713
		FolderHierarchySync GetFolderHierarchySync(ChangeTrackingDelegate changeTrackingDelegate);
	}
}
