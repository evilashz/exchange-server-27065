using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200004C RID: 76
	[Flags]
	internal enum FastTransferCopyMessagesFlag : byte
	{
		// Token: 0x040000EC RID: 236
		None = 0,
		// Token: 0x040000ED RID: 237
		Move = 1,
		// Token: 0x040000EE RID: 238
		BestBody = 16,
		// Token: 0x040000EF RID: 239
		SendEntryId = 32
	}
}
