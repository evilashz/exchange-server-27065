using System;

namespace Microsoft.Exchange.Server.Storage.DirectoryServices
{
	// Token: 0x0200000A RID: 10
	[Flags]
	public enum GetMailboxInfoFlags
	{
		// Token: 0x04000007 RID: 7
		None = 0,
		// Token: 0x04000008 RID: 8
		IgnoreHomeMdb = 1,
		// Token: 0x04000009 RID: 9
		BypassSharedCache = 2
	}
}
