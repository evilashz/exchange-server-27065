using System;

namespace Microsoft.Exchange.EdgeSync.Common
{
	// Token: 0x0200002E RID: 46
	public enum StatusResult
	{
		// Token: 0x040000C8 RID: 200
		InProgress,
		// Token: 0x040000C9 RID: 201
		Success,
		// Token: 0x040000CA RID: 202
		Aborted,
		// Token: 0x040000CB RID: 203
		CouldNotConnect,
		// Token: 0x040000CC RID: 204
		CouldNotLease,
		// Token: 0x040000CD RID: 205
		LostLease,
		// Token: 0x040000CE RID: 206
		Incomplete,
		// Token: 0x040000CF RID: 207
		NoSubscriptions,
		// Token: 0x040000D0 RID: 208
		SyncDisabled
	}
}
