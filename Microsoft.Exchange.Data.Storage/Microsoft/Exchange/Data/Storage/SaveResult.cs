using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001E3 RID: 483
	internal enum SaveResult
	{
		// Token: 0x04000D79 RID: 3449
		Success,
		// Token: 0x04000D7A RID: 3450
		SuccessWithConflictResolution,
		// Token: 0x04000D7B RID: 3451
		IrresolvableConflict,
		// Token: 0x04000D7C RID: 3452
		SuccessWithoutSaving
	}
}
