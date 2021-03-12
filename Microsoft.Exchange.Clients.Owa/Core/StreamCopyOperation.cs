using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200021B RID: 539
	[Flags]
	internal enum StreamCopyOperation
	{
		// Token: 0x04000C72 RID: 3186
		SyncRead = 1,
		// Token: 0x04000C73 RID: 3187
		SyncWrite = 2,
		// Token: 0x04000C74 RID: 3188
		AsyncRead = 4,
		// Token: 0x04000C75 RID: 3189
		AsyncWrite = 8,
		// Token: 0x04000C76 RID: 3190
		Read = 5,
		// Token: 0x04000C77 RID: 3191
		Write = 10
	}
}
