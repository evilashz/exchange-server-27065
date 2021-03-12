using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200005F RID: 95
	internal class EmptyFolderOperation : HierarchyBulkOperation
	{
		// Token: 0x06000269 RID: 617 RVA: 0x00011690 File Offset: 0x0000F890
		public EmptyFolderOperation(MapiFolder folder, bool deleteAssociatedMessages, bool processAssociatedDumpsterFolders, bool force, int chunkSize) : base(folder, processAssociatedDumpsterFolders, chunkSize)
		{
			this.deleteAssociatedMessages = deleteAssociatedMessages;
			this.force = force;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000116AB File Offset: 0x0000F8AB
		public EmptyFolderOperation(MapiFolder folder, bool deleteAssociatedMessages, bool processAssociatedDumpsterFolders, bool force) : this(folder, deleteAssociatedMessages, processAssociatedDumpsterFolders, force, 100)
		{
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000116BC File Offset: 0x0000F8BC
		protected override bool ProcessFolderStart(MapiContext context, HierarchyBulkOperation.FolderStackEntry currentEntry, out int progressCount, ref bool incomplete, ref ErrorCode error)
		{
			currentEntry.ProcessSubfolders = !currentEntry.IsAssociatedDumpster;
			if (currentEntry.IsPrincipal && base.PrincipalFolder.IsSearchFolder())
			{
				progressCount = 0;
				error = ErrorCode.CreateNotSupported((LID)48536U);
				return false;
			}
			if (currentEntry.Folder.IsSearchFolder())
			{
				currentEntry.ProcessAssociatedMessages = false;
				currentEntry.ProcessNormalMessages = false;
			}
			else
			{
				currentEntry.ProcessAssociatedMessages = (this.deleteAssociatedMessages || !currentEntry.IsPrincipal);
				currentEntry.ProcessNormalMessages = true;
			}
			if (currentEntry.ProcessNormalMessages || currentEntry.ProcessAssociatedMessages)
			{
				currentEntry.Folder.StoreFolder.InvalidateIndexes(context, currentEntry.ProcessNormalMessages, currentEntry.ProcessAssociatedMessages);
			}
			progressCount = 0;
			return true;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00011778 File Offset: 0x0000F978
		protected override bool ProcessMessages(MapiContext context, HierarchyBulkOperation.FolderStackEntry currentEntry, IList<ExchangeId> midsToProcess, out int progressCount, ref bool incomplete, ref ErrorCode error)
		{
			return BulkOperation.DeleteMessages(context, currentEntry.Folder, true, this.force, midsToProcess, BulkErrorAction.Skip, BulkErrorAction.Incomplete, out progressCount, ref incomplete, ref error);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000117A4 File Offset: 0x0000F9A4
		protected override bool ProcessFolderEnd(MapiContext context, HierarchyBulkOperation.FolderStackEntry currentEntry, out int progressCount, ref bool incomplete, ref ErrorCode error)
		{
			progressCount = 0;
			if (currentEntry.IsAssociatedDumpster)
			{
				return true;
			}
			if (!currentEntry.IsPrincipal)
			{
				error = currentEntry.Folder.Delete(context);
				if (error != ErrorCode.NoError)
				{
					return BulkOperation.ContinueOnError(ref error, ref incomplete, BulkErrorAction.Skip, BulkErrorAction.Incomplete);
				}
				progressCount = 1;
			}
			return true;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00011801 File Offset: 0x0000FA01
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<EmptyFolderOperation>(this);
		}

		// Token: 0x040001B1 RID: 433
		private bool deleteAssociatedMessages;

		// Token: 0x040001B2 RID: 434
		private bool force;
	}
}
