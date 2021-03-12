using System;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000049 RID: 73
	internal enum SessionRundownReason
	{
		// Token: 0x04000126 RID: 294
		ProtocolFault,
		// Token: 0x04000127 RID: 295
		ClientRundown,
		// Token: 0x04000128 RID: 296
		ClientRecreate,
		// Token: 0x04000129 RID: 297
		ContextHandleCleared,
		// Token: 0x0400012A RID: 298
		Expired
	}
}
