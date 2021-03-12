using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020000B2 RID: 178
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SyncFolderHierarchyResponseProcessor
	{
		// Token: 0x06000484 RID: 1156 RVA: 0x00017579 File Offset: 0x00015779
		internal static SyncFolderHierarchyChangeBase[] ShiftChangesToDefaultFoldersToTop(SyncFolderHierarchyChangeBase[] folderChanges, DistinguishedFolderIdName[] foldersToMoveToTop)
		{
			return FolderTreeProcessor.ShiftDefaultFoldersToTop<SyncFolderHierarchyChangeBase>(folderChanges, new Func<SyncFolderHierarchyChangeBase, DistinguishedFolderIdName>(SyncFolderHierarchyResponseProcessor.DistinguishedFolderIdNameExtractor), new Func<SyncFolderHierarchyChangeBase, string>(SyncFolderHierarchyResponseProcessor.ParentFolderIdExtractor), foldersToMoveToTop);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0001759C File Offset: 0x0001579C
		private static string ParentFolderIdExtractor(SyncFolderHierarchyChangeBase folderChange)
		{
			if (folderChange is SyncFolderHierarchyCreateOrUpdateType)
			{
				SyncFolderHierarchyCreateOrUpdateType syncFolderHierarchyCreateOrUpdateType = (SyncFolderHierarchyCreateOrUpdateType)folderChange;
				if (syncFolderHierarchyCreateOrUpdateType != null && syncFolderHierarchyCreateOrUpdateType.Folder != null && syncFolderHierarchyCreateOrUpdateType.Folder.ParentFolderId != null)
				{
					return syncFolderHierarchyCreateOrUpdateType.Folder.ParentFolderId.Id;
				}
			}
			return null;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x000175E4 File Offset: 0x000157E4
		private static DistinguishedFolderIdName DistinguishedFolderIdNameExtractor(SyncFolderHierarchyChangeBase folderChange)
		{
			if (folderChange is SyncFolderHierarchyCreateOrUpdateType)
			{
				SyncFolderHierarchyCreateOrUpdateType syncFolderHierarchyCreateOrUpdateType = (SyncFolderHierarchyCreateOrUpdateType)folderChange;
				if (syncFolderHierarchyCreateOrUpdateType != null && syncFolderHierarchyCreateOrUpdateType.Folder != null)
				{
					return EnumUtilities.Parse<DistinguishedFolderIdName>(syncFolderHierarchyCreateOrUpdateType.Folder.DistinguishedFolderId);
				}
			}
			return EnumUtilities.Parse<DistinguishedFolderIdName>(null);
		}
	}
}
