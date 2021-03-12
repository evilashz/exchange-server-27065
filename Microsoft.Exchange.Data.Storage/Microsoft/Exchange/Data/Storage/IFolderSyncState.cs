using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E19 RID: 3609
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IFolderSyncState : ISyncState
	{
		// Token: 0x17002167 RID: 8551
		// (get) Token: 0x06007CCE RID: 31950
		// (set) Token: 0x06007CCF RID: 31951
		ISyncWatermark Watermark { get; set; }

		// Token: 0x06007CD0 RID: 31952
		FolderSync GetFolderSync();

		// Token: 0x06007CD1 RID: 31953
		FolderSync GetFolderSync(ConflictResolutionPolicy policy);

		// Token: 0x06007CD2 RID: 31954
		void OnCommitStateModifications(FolderSyncStateUtil.CommitStateModificationsDelegate ommitStateModificationsDelegate);
	}
}
