using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000062 RID: 98
	internal class FolderCopyPropsOperation : HierarchyBulkOperation
	{
		// Token: 0x0600027C RID: 636 RVA: 0x00011D3E File Offset: 0x0000FF3E
		public FolderCopyPropsOperation(MapiFolder sourceFolder, MapiFolder destinationFolder, StorePropTag[] propTags, bool replaceIfExists, int chunkSize) : base(sourceFolder, false, chunkSize)
		{
			this.destinationPrincipalFolder = destinationFolder;
			this.propTags = propTags;
			this.replaceIfExists = replaceIfExists;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00011D60 File Offset: 0x0000FF60
		public FolderCopyPropsOperation(MapiFolder sourceFolder, MapiFolder destinationFolder, StorePropTag[] propTags, bool replaceIfExists) : this(sourceFolder, destinationFolder, propTags, replaceIfExists, 100)
		{
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00011D6F File Offset: 0x0000FF6F
		public List<MapiPropertyProblem> PropertyProblems
		{
			get
			{
				return this.propertyProblems;
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00011D78 File Offset: 0x0000FF78
		protected override bool ProcessFolderStart(MapiContext context, HierarchyBulkOperation.FolderStackEntry currentEntry, out int progressCount, ref bool incomplete, ref ErrorCode error)
		{
			bool processSubfolders = true;
			bool processNormalMessages = true;
			bool processAssociatedMessages = true;
			if (currentEntry.IsPrincipal)
			{
				if (!this.destinationPrincipalFolder.CheckAlive(context))
				{
					progressCount = 0;
					error = ErrorCode.CreateObjectDeleted((LID)46488U);
					return false;
				}
				if (this.destinationPrincipalFolder.IsSearchFolder())
				{
					progressCount = 0;
					error = ErrorCode.CreateNotSupported((LID)62872U);
					return false;
				}
				if (currentEntry.Folder.Fid == this.destinationPrincipalFolder.Fid)
				{
					progressCount = 0;
					error = ErrorCode.CreateNoAccess((LID)38296U);
					return false;
				}
				List<StorePropTag> list = new List<StorePropTag>(this.propTags);
				processSubfolders = false;
				processNormalMessages = false;
				processAssociatedMessages = false;
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].PropId == PropTag.Folder.ContainerContents.PropId)
					{
						processNormalMessages = true;
						list.RemoveAt(i);
						i--;
					}
					else if (list[i].PropId == PropTag.Folder.ContainerHierarchy.PropId)
					{
						processSubfolders = true;
						list.RemoveAt(i);
						i--;
					}
					else if (list[i].PropId == PropTag.Folder.FolderAssociatedContents.PropId)
					{
						processAssociatedMessages = true;
						list.RemoveAt(i);
						i--;
					}
				}
				currentEntry.Folder.CopyProps(context, this.destinationPrincipalFolder, list, this.replaceIfExists, ref this.propertyProblems);
				currentEntry.DestinationFolder = this.destinationPrincipalFolder;
				currentEntry.DisposeDestinationFolder = false;
			}
			else
			{
				if (!currentEntry.DestinationParentFolder.CheckAlive(context))
				{
					progressCount = 0;
					error = ErrorCode.CreateObjectDeleted((LID)54680U);
					return false;
				}
				error = currentEntry.Folder.Copy(context, currentEntry.DestinationParentFolder, currentEntry.Folder.GetDisplayName(context), out currentEntry.DestinationFolder);
				currentEntry.DisposeDestinationFolder = true;
				if (error != ErrorCode.NoError)
				{
					progressCount = 0;
					return false;
				}
			}
			currentEntry.ProcessSubfolders = processSubfolders;
			if (!currentEntry.Folder.IsSearchFolder())
			{
				currentEntry.ProcessAssociatedMessages = processAssociatedMessages;
				currentEntry.ProcessNormalMessages = processNormalMessages;
			}
			else
			{
				currentEntry.ProcessAssociatedMessages = false;
				currentEntry.ProcessNormalMessages = false;
			}
			progressCount = 1;
			return true;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00011FC0 File Offset: 0x000101C0
		protected override bool ProcessMessages(MapiContext context, HierarchyBulkOperation.FolderStackEntry currentEntry, IList<ExchangeId> midsToProcess, out int progressCount, ref bool incomplete, ref ErrorCode error)
		{
			return BulkOperation.CopyMessages(context, currentEntry.Folder, currentEntry.DestinationFolder, midsToProcess, Properties.Empty, BulkErrorAction.Incomplete, BulkErrorAction.Error, null, null, out progressCount, ref incomplete, ref error);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00011FEF File Offset: 0x000101EF
		protected override bool ProcessFolderEnd(MapiContext context, HierarchyBulkOperation.FolderStackEntry currentEntry, out int progressCount, ref bool incomplete, ref ErrorCode error)
		{
			progressCount = 0;
			return true;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00011FF5 File Offset: 0x000101F5
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<FolderCopyPropsOperation>(this);
		}

		// Token: 0x040001BA RID: 442
		private MapiFolder destinationPrincipalFolder;

		// Token: 0x040001BB RID: 443
		private StorePropTag[] propTags;

		// Token: 0x040001BC RID: 444
		private bool replaceIfExists;

		// Token: 0x040001BD RID: 445
		private List<MapiPropertyProblem> propertyProblems;
	}
}
