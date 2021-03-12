using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000243 RID: 579
	internal enum FastTransferState : ushort
	{
		// Token: 0x04001156 RID: 4438
		Error,
		// Token: 0x04001157 RID: 4439
		Partial,
		// Token: 0x04001158 RID: 4440
		NoRoom,
		// Token: 0x04001159 RID: 4441
		Done
	}
}
