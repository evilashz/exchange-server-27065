using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000984 RID: 2436
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FolderPropertyRestriction : PropertyRestriction
	{
		// Token: 0x060059F7 RID: 23031 RVA: 0x00174F90 File Offset: 0x00173190
		public FolderPropertyRestriction()
		{
			this.BlockBeforeLink.Add(FolderSchema.LinkedId);
			this.BlockBeforeLink.Add(FolderSchema.LinkedUrl);
			this.BlockBeforeLink.Add(FolderSchema.LinkedSiteUrl);
			this.BlockBeforeLink.Add(FolderSchema.LinkedListId);
			this.BlockBeforeLink.Add(FolderSchema.SharePointChangeToken);
			this.BlockBeforeLink.Add(FolderSchema.IsDocumentLibraryFolder);
			this.BlockBeforeLink.Add(FolderSchema.LinkedSiteAuthorityUrl);
			this.BlockAfterLink.Add(FolderSchema.LinkedId);
			this.BlockAfterLink.Add(FolderSchema.LinkedUrl);
			this.BlockAfterLink.Add(FolderSchema.LinkedSiteUrl);
			this.BlockAfterLink.Add(FolderSchema.LinkedListId);
			this.BlockAfterLink.Add(FolderSchema.SharePointChangeToken);
			this.BlockAfterLink.Add(FolderSchema.IsDocumentLibraryFolder);
			this.BlockAfterLink.Add(FolderSchema.DisplayName);
			this.BlockAfterLink.Add(StoreObjectSchema.ContainerClass);
			this.BlockAfterLink.Add(FolderSchema.IsHidden);
			this.BlockAfterLink.Add(FolderSchema.LinkedSiteAuthorityUrl);
		}

		// Token: 0x04003177 RID: 12663
		public static FolderPropertyRestriction Instance = new FolderPropertyRestriction();
	}
}
