using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.PublicFolder
{
	// Token: 0x02000947 RID: 2375
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PublicFolderMailboxSynchronizerReference : DisposeTrackableBase
	{
		// Token: 0x06005870 RID: 22640 RVA: 0x0016BD68 File Offset: 0x00169F68
		public PublicFolderMailboxSynchronizerReference(PublicFolderMailboxSynchronizer publicFolderMailboxSynchronizer, Action<PublicFolderMailboxSynchronizer> onDispose)
		{
			this.publicFolderMailboxSynchronizer = publicFolderMailboxSynchronizer;
			this.onDispose = onDispose;
		}

		// Token: 0x17001885 RID: 6277
		// (get) Token: 0x06005871 RID: 22641 RVA: 0x0016BD7E File Offset: 0x00169F7E
		public PublicFolderSyncJobState SyncJobState
		{
			get
			{
				return this.publicFolderMailboxSynchronizer.SyncJobState;
			}
		}

		// Token: 0x06005872 RID: 22642 RVA: 0x0016BD8B File Offset: 0x00169F8B
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PublicFolderMailboxSynchronizerReference>(this);
		}

		// Token: 0x06005873 RID: 22643 RVA: 0x0016BD93 File Offset: 0x00169F93
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.onDispose(this.publicFolderMailboxSynchronizer);
				this.publicFolderMailboxSynchronizer = null;
			}
		}

		// Token: 0x04003038 RID: 12344
		private PublicFolderMailboxSynchronizer publicFolderMailboxSynchronizer;

		// Token: 0x04003039 RID: 12345
		private Action<PublicFolderMailboxSynchronizer> onDispose;
	}
}
