using System;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000056 RID: 86
	[Flags]
	public enum BulkErrorAction
	{
		// Token: 0x04000181 RID: 385
		Skip = 0,
		// Token: 0x04000182 RID: 386
		Incomplete = 1,
		// Token: 0x04000183 RID: 387
		Error = 2,
		// Token: 0x04000184 RID: 388
		Exception = 3
	}
}
