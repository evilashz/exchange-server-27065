using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200004F RID: 79
	internal enum FastTransferState : ushort
	{
		// Token: 0x040000FB RID: 251
		Error,
		// Token: 0x040000FC RID: 252
		Partial,
		// Token: 0x040000FD RID: 253
		NoRoom,
		// Token: 0x040000FE RID: 254
		Done
	}
}
