using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000061 RID: 97
	internal class FolderCopyToOperation : HierarchyBulkOperation
	{
		// Token: 0x06000275 RID: 629 RVA: 0x00011995 File Offset: 0x0000FB95
		public FolderCopyToOperation(MapiFolder sourceFolder, MapiFolder destinationFolder, StorePropTag[] propTagsExclude, CopyToFlags flags, int chunkSize) : base(sourceFolder, false, chunkSize)
		{
			this.destinationPrincipalFolder = destinationFolder;
			this.propTagsExclude = propTagsExclude;
			this.flags = flags;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000119B7 File Offset: 0x0000FBB7
		public FolderCopyToOperation(MapiFolder sourceFolder, MapiFolder destinationFolder, StorePropTag[] propTagsExclude, CopyToFlags flags) : this(sourceFolder, destinationFolder, propTagsExclude, flags, 100)
		{
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000277 RID: 631 RVA: 0x000119C6 File Offset: 0x0000FBC6
		public List<MapiPropertyProblem> PropertyProblems
		{
			get
			{
				return this.propertyProblems;
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000119D0 File Offset: 0x0000FBD0
		protected override bool ProcessFolderStart(MapiContext context, HierarchyBulkOperation.FolderStackEntry currentEntry, out int progressCount, ref bool incomplete, ref ErrorCode error)
		{
			bool processSubfolders = true;
			bool flag = true;
			bool flag2 = true;
			if (currentEntry.IsPrincipal)
			{
				if (!this.destinationPrincipalFolder.CheckAlive(context))
				{
					progressCount = 0;
					error = ErrorCode.CreateObjectDeleted((LID)56728U);
					return false;
				}
				if (this.destinationPrincipalFolder.IsSearchFolder())
				{
					progressCount = 0;
					error = ErrorCode.CreateNotSupported((LID)44440U);
					return false;
				}
				if (currentEntry.Folder.Fid == this.destinationPrincipalFolder.Fid)
				{
					progressCount = 0;
					error = ErrorCode.CreateNoAccess((LID)60824U);
					return false;
				}
				if ((this.flags & CopyToFlags.MoveProperties) != CopyToFlags.None && currentEntry.Folder.GetParentFid(context).IsNullOrZero)
				{
					progressCount = 0;
					error = ErrorCode.CreateRootFolder((LID)36248U);
					return false;
				}
				processSubfolders = (CopyToFlags.None != (this.flags & CopyToFlags.CopyHierarchy));
				flag = (CopyToFlags.None != (this.flags & CopyToFlags.CopyContent));
				flag2 = (CopyToFlags.None != (this.flags & CopyToFlags.CopyHiddenItems));
				if (this.propTagsExclude != null && this.propTagsExclude.Length != 0)
				{
					for (int i = 0; i < this.propTagsExclude.Length; i++)
					{
						if (this.propTagsExclude[i].PropId == PropTag.Folder.ContainerContents.PropId)
						{
							flag = false;
						}
						else if (this.propTagsExclude[i].PropId == PropTag.Folder.ContainerHierarchy.PropId)
						{
							processSubfolders = false;
						}
						else if (this.propTagsExclude[i].PropId == PropTag.Folder.FolderAssociatedContents.PropId)
						{
							flag2 = false;
						}
					}
				}
				if ((this.flags & CopyToFlags.MoveProperties) != CopyToFlags.None && (flag || flag2))
				{
					currentEntry.Folder.StoreFolder.InvalidateIndexes(context, flag, flag2);
				}
				currentEntry.Folder.CopyTo(context, this.destinationPrincipalFolder, this.propTagsExclude, this.flags, ref this.propertyProblems);
				currentEntry.DestinationFolder = this.destinationPrincipalFolder;
				currentEntry.DisposeDestinationFolder = false;
			}
			else
			{
				if (!currentEntry.DestinationParentFolder.CheckAlive(context))
				{
					progressCount = 0;
					error = ErrorCode.CreateObjectDeleted((LID)52632U);
					return false;
				}
				if ((this.flags & CopyToFlags.MoveProperties) != CopyToFlags.None)
				{
					error = currentEntry.Folder.Move(context, currentEntry.DestinationParentFolder, currentEntry.Folder.GetDisplayName(context), false);
					if (error != ErrorCode.NoError)
					{
						progressCount = 0;
						return false;
					}
					processSubfolders = false;
					flag = false;
					flag2 = false;
				}
				else
				{
					error = currentEntry.Folder.Copy(context, currentEntry.DestinationParentFolder, currentEntry.Folder.GetDisplayName(context), out currentEntry.DestinationFolder);
					currentEntry.DisposeDestinationFolder = true;
					if (error != ErrorCode.NoError)
					{
						progressCount = 0;
						return false;
					}
				}
			}
			currentEntry.ProcessSubfolders = processSubfolders;
			if (!currentEntry.Folder.IsSearchFolder())
			{
				currentEntry.ProcessAssociatedMessages = flag2;
				currentEntry.ProcessNormalMessages = flag;
			}
			else
			{
				currentEntry.ProcessAssociatedMessages = false;
				currentEntry.ProcessNormalMessages = false;
			}
			progressCount = 1;
			return true;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00011CD4 File Offset: 0x0000FED4
		protected override bool ProcessMessages(MapiContext context, HierarchyBulkOperation.FolderStackEntry currentEntry, IList<ExchangeId> midsToProcess, out int progressCount, ref bool incomplete, ref ErrorCode error)
		{
			if ((this.flags & CopyToFlags.MoveProperties) != CopyToFlags.None)
			{
				return BulkOperation.MoveMessages(context, currentEntry.Folder, currentEntry.DestinationFolder, midsToProcess, Properties.Empty, BulkErrorAction.Incomplete, BulkErrorAction.Error, null, null, out progressCount, ref incomplete, ref error);
			}
			return BulkOperation.CopyMessages(context, currentEntry.Folder, currentEntry.DestinationFolder, midsToProcess, Properties.Empty, BulkErrorAction.Incomplete, BulkErrorAction.Error, null, null, out progressCount, ref incomplete, ref error);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00011D30 File Offset: 0x0000FF30
		protected override bool ProcessFolderEnd(MapiContext context, HierarchyBulkOperation.FolderStackEntry currentEntry, out int progressCount, ref bool incomplete, ref ErrorCode error)
		{
			progressCount = 0;
			return true;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00011D36 File Offset: 0x0000FF36
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<FolderCopyToOperation>(this);
		}

		// Token: 0x040001B6 RID: 438
		private MapiFolder destinationPrincipalFolder;

		// Token: 0x040001B7 RID: 439
		private StorePropTag[] propTagsExclude;

		// Token: 0x040001B8 RID: 440
		private CopyToFlags flags;

		// Token: 0x040001B9 RID: 441
		private List<MapiPropertyProblem> propertyProblems;
	}
}
