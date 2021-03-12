using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200005E RID: 94
	internal class DeleteFolderOperation : HierarchyBulkOperation
	{
		// Token: 0x06000263 RID: 611 RVA: 0x00011523 File Offset: 0x0000F723
		public DeleteFolderOperation(MapiFolder folder, bool deleteSubfolders, bool deleteMessages, bool processAssociatedDumpsterFolders, bool force, int chunkSize) : base(folder, processAssociatedDumpsterFolders, chunkSize)
		{
			this.deleteSubfolders = deleteSubfolders;
			this.deleteMessages = deleteMessages;
			this.force = force;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00011546 File Offset: 0x0000F746
		public DeleteFolderOperation(MapiFolder folder, bool deleteSubfolders, bool deleteMessages, bool processAssociatedDumpsterFolders, bool force) : this(folder, deleteSubfolders, deleteMessages, processAssociatedDumpsterFolders, force, 100)
		{
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00011558 File Offset: 0x0000F758
		protected override bool ProcessFolderStart(MapiContext context, HierarchyBulkOperation.FolderStackEntry currentEntry, out int progressCount, ref bool incomplete, ref ErrorCode error)
		{
			progressCount = 0;
			currentEntry.ProcessSubfolders = (this.deleteSubfolders && !currentEntry.IsAssociatedDumpster);
			if (currentEntry.Folder.IsSearchFolder())
			{
				currentEntry.ProcessAssociatedMessages = false;
				currentEntry.ProcessNormalMessages = false;
			}
			else
			{
				currentEntry.ProcessAssociatedMessages = (this.deleteMessages || !currentEntry.IsPrincipal);
				currentEntry.ProcessNormalMessages = (this.deleteMessages || !currentEntry.IsPrincipal);
			}
			if (currentEntry.ProcessNormalMessages || currentEntry.ProcessAssociatedMessages)
			{
				currentEntry.Folder.StoreFolder.InvalidateIndexes(context, currentEntry.ProcessNormalMessages, currentEntry.ProcessAssociatedMessages);
			}
			return true;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00011604 File Offset: 0x0000F804
		protected override bool ProcessMessages(MapiContext context, HierarchyBulkOperation.FolderStackEntry currentEntry, IList<ExchangeId> midsToProcess, out int progressCount, ref bool incomplete, ref ErrorCode error)
		{
			return BulkOperation.DeleteMessages(context, currentEntry.Folder, true, this.force, midsToProcess, BulkErrorAction.Skip, BulkErrorAction.Incomplete, out progressCount, ref incomplete, ref error);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00011630 File Offset: 0x0000F830
		protected override bool ProcessFolderEnd(MapiContext context, HierarchyBulkOperation.FolderStackEntry currentEntry, out int progressCount, ref bool incomplete, ref ErrorCode error)
		{
			if (currentEntry.IsAssociatedDumpster)
			{
				progressCount = 0;
				return true;
			}
			error = currentEntry.Folder.Delete(context);
			if (error != ErrorCode.NoError)
			{
				progressCount = 0;
				return BulkOperation.ContinueOnError(ref error, ref incomplete, BulkErrorAction.Skip, BulkErrorAction.Incomplete);
			}
			progressCount = 1;
			return true;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00011688 File Offset: 0x0000F888
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<DeleteFolderOperation>(this);
		}

		// Token: 0x040001AE RID: 430
		private bool deleteSubfolders;

		// Token: 0x040001AF RID: 431
		private bool deleteMessages;

		// Token: 0x040001B0 RID: 432
		private bool force;
	}
}
