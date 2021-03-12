using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200057F RID: 1407
	public enum ServerStatus
	{
		// Token: 0x04002C6B RID: 11371
		[LocDescription(DirectoryStrings.IDs.Enabled)]
		Enabled,
		// Token: 0x04002C6C RID: 11372
		[LocDescription(DirectoryStrings.IDs.Disabled)]
		Disabled,
		// Token: 0x04002C6D RID: 11373
		[LocDescription(DirectoryStrings.IDs.NoNewCalls)]
		NoNewCalls
	}
}
