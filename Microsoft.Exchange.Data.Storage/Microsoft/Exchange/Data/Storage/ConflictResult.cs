using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000222 RID: 546
	internal enum ConflictResult
	{
		// Token: 0x04001009 RID: 4105
		AcceptClientChange = 1,
		// Token: 0x0400100A RID: 4106
		RejectClientChange,
		// Token: 0x0400100B RID: 4107
		TemporarilyUnavailable,
		// Token: 0x0400100C RID: 4108
		ObjectNotFound
	}
}
