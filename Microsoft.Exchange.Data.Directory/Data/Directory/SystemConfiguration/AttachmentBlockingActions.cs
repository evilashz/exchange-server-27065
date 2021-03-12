using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200036D RID: 877
	public enum AttachmentBlockingActions
	{
		// Token: 0x04001903 RID: 6403
		[LocDescription(DirectoryStrings.IDs.Allow)]
		Allow,
		// Token: 0x04001904 RID: 6404
		[LocDescription(DirectoryStrings.IDs.ForceSave)]
		ForceSave,
		// Token: 0x04001905 RID: 6405
		[LocDescription(DirectoryStrings.IDs.Block)]
		Block
	}
}
