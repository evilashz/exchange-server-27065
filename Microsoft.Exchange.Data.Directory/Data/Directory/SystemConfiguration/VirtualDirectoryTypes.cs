using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200036B RID: 875
	public enum VirtualDirectoryTypes
	{
		// Token: 0x040018FA RID: 6394
		[LocDescription(DirectoryStrings.IDs.NotSpecified)]
		NotSpecified,
		// Token: 0x040018FB RID: 6395
		[LocDescription(DirectoryStrings.IDs.Mailboxes)]
		Mailboxes,
		// Token: 0x040018FC RID: 6396
		[LocDescription(DirectoryStrings.IDs.PublicFolders)]
		PublicFolders,
		// Token: 0x040018FD RID: 6397
		[LocDescription(DirectoryStrings.IDs.Exchweb)]
		Exchweb,
		// Token: 0x040018FE RID: 6398
		[LocDescription(DirectoryStrings.IDs.Exadmin)]
		Exadmin
	}
}
