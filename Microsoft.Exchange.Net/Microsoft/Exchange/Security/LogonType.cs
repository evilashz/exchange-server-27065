using System;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C74 RID: 3188
	internal enum LogonType
	{
		// Token: 0x04003B22 RID: 15138
		Unknown,
		// Token: 0x04003B23 RID: 15139
		Interactive = 2,
		// Token: 0x04003B24 RID: 15140
		Network,
		// Token: 0x04003B25 RID: 15141
		Batch,
		// Token: 0x04003B26 RID: 15142
		Service,
		// Token: 0x04003B27 RID: 15143
		Unlock = 7,
		// Token: 0x04003B28 RID: 15144
		NetworkCleartext,
		// Token: 0x04003B29 RID: 15145
		NewCredentials
	}
}
