using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200021C RID: 540
	[Flags]
	public enum StreamCopyMode
	{
		// Token: 0x04000C79 RID: 3193
		SyncReadSyncWrite = 3,
		// Token: 0x04000C7A RID: 3194
		SyncReadAsyncWrite = 9,
		// Token: 0x04000C7B RID: 3195
		AsyncReadSyncWrite = 6,
		// Token: 0x04000C7C RID: 3196
		AsyncReadAsyncWrite = 12
	}
}
