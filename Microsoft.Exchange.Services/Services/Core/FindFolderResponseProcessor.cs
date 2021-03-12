using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020000A4 RID: 164
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FindFolderResponseProcessor
	{
		// Token: 0x060003CA RID: 970 RVA: 0x000130B5 File Offset: 0x000112B5
		internal static BaseFolderType[] ShiftDefaultFoldersToTop(BaseFolderType[] folderCollection, DistinguishedFolderIdName[] foldersToMoveToTop)
		{
			return FolderTreeProcessor.ShiftDefaultFoldersToTop<BaseFolderType>(folderCollection, new Func<BaseFolderType, DistinguishedFolderIdName>(FindFolderResponseProcessor.DistinguishedFolderIdNameExtractor), new Func<BaseFolderType, string>(FindFolderResponseProcessor.ParentFolderIdExtractor), foldersToMoveToTop);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000130D6 File Offset: 0x000112D6
		private static string ParentFolderIdExtractor(BaseFolderType folder)
		{
			if (folder != null && folder.ParentFolderId != null)
			{
				return folder.ParentFolderId.Id;
			}
			return null;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000130F0 File Offset: 0x000112F0
		private static DistinguishedFolderIdName DistinguishedFolderIdNameExtractor(BaseFolderType folder)
		{
			return EnumUtilities.Parse<DistinguishedFolderIdName>(folder.DistinguishedFolderId);
		}
	}
}
