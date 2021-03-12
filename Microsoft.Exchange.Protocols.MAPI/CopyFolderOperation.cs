using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000060 RID: 96
	internal class CopyFolderOperation : HierarchyBulkOperation
	{
		// Token: 0x0600026F RID: 623 RVA: 0x00011809 File Offset: 0x0000FA09
		public CopyFolderOperation(MapiFolder sourceFolder, MapiFolder destinationParentFolder, string newFolderName, bool recurse, int chunkSize) : base(sourceFolder, false, chunkSize)
		{
			this.recurse = recurse;
			this.destinationPrincipalFolder = destinationParentFolder;
			this.newFolderName = newFolderName;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0001182B File Offset: 0x0000FA2B
		public CopyFolderOperation(MapiFolder sourceFolder, MapiFolder destinationParentFolder, string newFolderName, bool recurse) : this(sourceFolder, destinationParentFolder, newFolderName, recurse, 100)
		{
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0001183C File Offset: 0x0000FA3C
		protected override bool ProcessFolderStart(MapiContext context, HierarchyBulkOperation.FolderStackEntry currentEntry, out int progressCount, ref bool incomplete, ref ErrorCode error)
		{
			if (currentEntry.IsPrincipal)
			{
				if (!this.destinationPrincipalFolder.CheckAlive(context))
				{
					progressCount = 0;
					error = ErrorCode.CreateObjectDeleted((LID)64920U);
					return false;
				}
				error = currentEntry.Folder.Copy(context, this.destinationPrincipalFolder, this.newFolderName, out currentEntry.DestinationFolder);
				currentEntry.DisposeDestinationFolder = true;
			}
			else
			{
				if (!currentEntry.DestinationParentFolder.CheckAlive(context))
				{
					progressCount = 0;
					error = ErrorCode.CreateObjectDeleted((LID)40344U);
					return false;
				}
				error = currentEntry.Folder.Copy(context, currentEntry.DestinationParentFolder, currentEntry.Folder.GetDisplayName(context), out currentEntry.DestinationFolder);
				currentEntry.DisposeDestinationFolder = true;
			}
			if (error != ErrorCode.NoError)
			{
				progressCount = 0;
				return false;
			}
			currentEntry.ProcessSubfolders = this.recurse;
			if (!currentEntry.Folder.IsSearchFolder())
			{
				currentEntry.ProcessAssociatedMessages = true;
				currentEntry.ProcessNormalMessages = true;
			}
			else
			{
				currentEntry.ProcessAssociatedMessages = false;
				currentEntry.ProcessNormalMessages = false;
			}
			progressCount = 1;
			return true;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00011958 File Offset: 0x0000FB58
		protected override bool ProcessMessages(MapiContext context, HierarchyBulkOperation.FolderStackEntry currentEntry, IList<ExchangeId> midsToProcess, out int progressCount, ref bool incomplete, ref ErrorCode error)
		{
			return BulkOperation.CopyMessages(context, currentEntry.Folder, currentEntry.DestinationFolder, midsToProcess, Properties.Empty, BulkErrorAction.Incomplete, BulkErrorAction.Error, null, null, out progressCount, ref incomplete, ref error);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00011987 File Offset: 0x0000FB87
		protected override bool ProcessFolderEnd(MapiContext context, HierarchyBulkOperation.FolderStackEntry currentEntry, out int progressCount, ref bool incomplete, ref ErrorCode error)
		{
			progressCount = 0;
			return true;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0001198D File Offset: 0x0000FB8D
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CopyFolderOperation>(this);
		}

		// Token: 0x040001B3 RID: 435
		private MapiFolder destinationPrincipalFolder;

		// Token: 0x040001B4 RID: 436
		private string newFolderName;

		// Token: 0x040001B5 RID: 437
		private bool recurse;
	}
}
