using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x0200021F RID: 543
	public enum DeletedItemRetention
	{
		// Token: 0x04000C76 RID: 3190
		DatabaseDefault,
		// Token: 0x04000C77 RID: 3191
		RetainForCustomPeriod = 5,
		// Token: 0x04000C78 RID: 3192
		RetainUntilBackupOrCustomPeriod = 3
	}
}
